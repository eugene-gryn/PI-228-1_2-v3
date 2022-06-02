using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{

    private readonly OrderService _orderService;
    private readonly UserService _userService;
    private readonly StatisticsService _statisticsS;

    
    public OrdersController(OrderService orderService, UserService userService, StatisticsService statisticsS)
    {
        _orderService = orderService;
        _userService = userService;
        _statisticsS = statisticsS;
    }


    [HttpGet("getUserOrders/userId-{userId}")]
    public async Task<ActionResult<OrderDTO>> GetUserOrders(int userId)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        if (isAdminOrModerator == null)
        {
            return BadRequest("User not found");
        }
        if (isAdminOrModerator.Value)
        {
            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }
        else
        {
            var user = Utils.GetUserIDFromJWT(User);

            if (user.HasValue && user.Value == userId)
            {
                var orders = await _orderService.GetUserOrders(userId);
                return Ok(orders);

            }
        }

        return BadRequest("User can edit only own cart");
    }


    [HttpPost("createOrder/userCartID-{userCartID}")]
    public async Task<ActionResult<OrderDTO>> CreateOrder(int userCartID, OrderDTO order)
    {
        var orderCreated = await _orderService.Create(order);
        if (orderCreated != null)
        {
            var products = await _orderService.GetCartProducts(userCartID);

            if (products != null)
                foreach (var (key, value) in products)
                {
                    if (key != null) await _statisticsS.AddBought(key.ID);
                }

            var orderDto = await _orderService.MoveProductsFromCartToOrder(userCartID, orderCreated.ID);
            return Ok(orderDto);
        }

        return BadRequest("Error to make order");
    }

    [HttpGet("getProdAmounts/orderId-{orderId}")]
    public async Task<ActionResult<Dictionary<ProductDTO, int>>> GetProdAmounts(int orderId)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        if (isAdminOrModerator == null)
        {
            return BadRequest("User not found");
        }
        if (isAdminOrModerator.Value)
        {
            var response = await _orderService.GetOrderProducts(orderId);

            return Ok(response);
        }
        else
        {
            var user = Utils.GetUserIDFromJWT(User);
            var orders = await _orderService.GetMainData(orderId);

            if (orders != null && user.HasValue && user.Value == orders.UserID)
            {
                var response = await _orderService.GetOrderProducts(orderId);

                return Ok(response);
            }

            return BadRequest("User can get only own order list");
        }
    }


    [HttpPost("addProductToCart/userId-{userID:int}&productId-{productID}&amount-{amount}")]
    public async Task<ActionResult<bool>> AddProductToCart(int userID, int productID, int amount)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        if (isAdminOrModerator == null)
        {
            return BadRequest("User not found");
        }
        if (isAdminOrModerator.Value)
        {
            var response = await _orderService.AddProductToCart(userID, productID, amount);

            if (response) return Ok(response);
            
            return BadRequest("Error is happen");
        }
        else
        {
            var user = Utils.GetUserIDFromJWT(User);
            if (user.HasValue && user.Value == userID)
            {
                var response = await _orderService.AddProductToCart(userID, productID, amount);

                if (response) return Ok(response);

                return BadRequest("Error is happen");
            }

            return BadRequest("User can edit only own cart");
        }
    }

    [HttpGet("getCart/userId-{userID:int}")]
    public async Task<ActionResult<Dictionary<ProductDTO, int>>> GetCartProdAmounts(int userID)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        if (isAdminOrModerator == null)
        {
            return BadRequest("User not found");
        }
        if (isAdminOrModerator.Value)
        {
            return Ok(await _orderService.GetCartProducts(userID));
        }
        else
        {
            var user = Utils.GetUserIDFromJWT(User);
            if (user.HasValue && user.Value == userID)
            {
                return Ok(await _orderService.GetCartProducts(userID));
            }

            return BadRequest("User can get only own cart");
        }
    }



    [HttpGet("orderData/orderId-{orderId:int}")]
    public async Task<ActionResult<OrderDTO>> GetOrderData(int orderId)
    {
        var orderData = await _orderService.GetMainData(orderId);
        if (orderData == null)
        {
            return BadRequest("Bad order id.");
        }

        if (orderData.UserID != Utils.GetUserIDFromJWT(User))
        {
            return BadRequest("Forbidden.");
        }

        return Ok(orderData);
    }
    
    
    [HttpDelete("deleteOrder/orderId-{orderId:int}")]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId) 
    {
        var orderData = await _orderService.GetMainData(orderId);
        if (orderData == null)
        {
            return BadRequest("Bad order id.");
        }

        if (orderData.UserID != Utils.GetUserIDFromJWT(User))
        {
            return BadRequest("Forbidden.");
        }
        
        

        var success = await _orderService.DeleteOrder(orderId);

        return Ok(success);
    }


    [HttpPost("markProcessed/orderId-{orderId:int}")]
    public async Task<ActionResult<bool>> MarkOrderAsProcessed(int orderId)
    {
        var user = await UserController.IsUserAdminOrModerator(User, _userService);

            
        if (user == null) return BadRequest("User ID error.");
        if (!user.Value) return BadRequest("Forbidden!");
        
        var result = await _orderService.MarkOrderAsProcessed(orderId);
        return Ok(result);
    }

}
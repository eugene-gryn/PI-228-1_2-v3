using BLL.DTOs;
using BLL.DTOs.Product;
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
    private readonly StatisticsService _statisticsS;
    private readonly UserService _userService;


    public OrdersController(OrderService orderService, UserService userService, StatisticsService statisticsS)
    {
        _orderService = orderService;
        _userService = userService;
        _statisticsS = statisticsS;
    }

    [HttpGet("getCart/userId-{userID:int}")]
    public async Task<ActionResult<List<ProductAmountDTO>>> GetCartProdAmounts(int userID)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);
        var user = Utils.GetUserIDFromJWT(User);


        if (!(isAdminOrModerator || user.HasValue && user.Value == userID))
            return BadRequest("User can edit only own cart");

        var dict = await _orderService.GetCartProducts(userID);
        return Ok(dict);
    }

    [HttpGet("getUserOrders/userId-{userId}")]
    public async Task<ActionResult<OrderDTO>> GetUserOrders(int userId)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);
        var user = Utils.GetUserIDFromJWT(User);

        if (!(isAdminOrModerator || user.HasValue && user.Value == userId))
            return BadRequest("User can edit only own cart");

        var orders = await _orderService.GetUserOrders(userId);
        return Ok(orders);
    }

    [HttpGet("getProdAmounts/orderId-{orderId}")]
    public async Task<ActionResult<List<ProductAmountDTO>>> GetProdAmounts(int orderId)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);
        var orders = await _orderService.GetMainData(orderId);
        var user = Utils.GetUserIDFromJWT(User);


        if (!(orders != null && (isAdminOrModerator || user.HasValue && user.Value == orders.ID)))
            return BadRequest("User can edit only own cart");

        var response = await _orderService.GetOrderProducts(orderId);

        return Ok(response);
    }

    [HttpGet("orderData/orderId-{orderId:int}")]
    public async Task<ActionResult<OrderDTO>> GetOrderData(int orderId)
    {
        var orderData = await _orderService.GetMainData(orderId);
        if (orderData == null) return BadRequest("Bad order id.");

        if (orderData.UserID != Utils.GetUserIDFromJWT(User)) return BadRequest("Forbidden.");

        return Ok(orderData);
    }

    [HttpPut("addProductToCart/userId-{userID:int}&productId-{productID}&amount-{amount}")]
    public async Task<ActionResult<bool>> AddProductToCart(int userID, int productID, int amount)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        var user = Utils.GetUserIDFromJWT(User);


        if (!(isAdminOrModerator || user.HasValue && user.Value == userID))
            return BadRequest("User can edit only own cart");

        var response = await _orderService.AddProductToCart(userID, productID, amount);

        if (!response) return BadRequest("Error is happen");

        return Ok(response);
    }

    [HttpDelete("removeProductFromCart/userId-{userID:int}&productId-{productID}")]
    public async Task<ActionResult<bool>> RemoveProductFromCart(int userID, int productID)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        var user = Utils.GetUserIDFromJWT(User);


        if (!(isAdminOrModerator || user.HasValue && user.Value == userID))
            return BadRequest("User can edit only own cart");


        var response = await _orderService.DeleteProductFromCart(userID, productID);

        if (!response) return BadRequest("Error is happen");

        return Ok(response);
    }

    [HttpPut("createOrder/userId-{userId:int}&deliveryInfo-{deliveryInfo}")]
    public async Task<ActionResult<OrderDTO>> CreateOrder(int userId, string deliveryInfo)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);

        var user = Utils.GetUserIDFromJWT(User);


        if (!(isAdminOrModerator || user.HasValue && user.Value == userId))
            return BadRequest("User can edit only own cart");


        var orderDto = await _orderService.Create(new OrderDTO
        {
            DeliveryInfo = deliveryInfo,
            UserID = userId
        });
        var response = await _orderService.MoveProductsFromCartToOrder(userId, orderDto.ID);

        if (!response) return BadRequest("Error is happen");

        return Ok(response);
    }


    [HttpPost("markProcessed/orderId-{orderId:int}")]
    public async Task<ActionResult<bool>> MarkOrderAsProcessed(int orderId)
    {
        var isUserAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);


        if (!isUserAdminOrModerator) return BadRequest("Forbidden!");

        var result = await _orderService.MarkOrderAsProcessed(orderId);
        return Ok(result);
    }

    [HttpDelete("deleteOrder/orderId-{orderId:int}")]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        var isAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userService);
        var userId = Utils.GetUserIDFromJWT(User);
        var orderData = await _orderService.GetMainData(orderId);

        if (orderData == null) return BadRequest("Bad order id.");

        if (orderData.UserID != userId && !isAdminOrModerator) return BadRequest("Forbidden.");


        var success = await _orderService.DeleteOrder(orderId);

        return Ok(success);
    }
}
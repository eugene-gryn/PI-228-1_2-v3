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

    
    public OrdersController(OrderService orderService, UserService userService)
    {
        _orderService = orderService;
        _userService = userService;
    }

    /*
     * GetProdAmounts (orderId)
     * 
     * CreateOrder (userCartID, orderData)
     * GetOrderData (orderId)
     * DeleteOrder (if not done)
     * ChangeOrderStatus [admin]
     *
     * ===CART===
     * GetCartProdAmounts (userID)
     * AddProductToCart(userID, productID, amount)
     * 
     */



    [HttpGet("orderData/{id:int}")]
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
    
    
    [HttpDelete("deleteOrder/{id:int}")]
    public async Task<ActionResult<OrderDTO>> DeleteOrder(int orderId) 
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
        
        

        //_orderService.Delete();

        return Ok(orderData);
    }
    
}
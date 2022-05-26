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
    /// <summary>
    /// TODO: При офомленні заказу повідомляти статистику про ще одну закупівлю товару
    /// </summary>
    private readonly StatisticsService _statisticsS;

    
    public OrdersController(OrderService orderService, UserService userService, StatisticsService statisticsS)
    {
        _orderService = orderService;
        _userService = userService;
        _statisticsS = statisticsS;
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



    [HttpGet("orderData/{orderId:int}")]
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
    
    
    [HttpDelete("deleteOrder/{orderId:int}")]
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


    [HttpPost("markProcessed/{orderId:int}")]
    public async Task<ActionResult<bool>> MarkOrderAsProcessed(int orderId)
    {
        var userID = Utils.GetUserIDFromJWT(User);
        if (userID == null) return BadRequest("User ID error.");

        var user = await _userService.GetMainData((int)userID);
        if (user == null || !user.IsModerator && !user.IsAdmin)
        {
            return BadRequest("Forbidden!");
        }

        var result = await _orderService.MarkOrderAsProcessed(orderId);
        return Ok(result);
    }

}
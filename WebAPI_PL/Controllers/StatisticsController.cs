using BLL.DTOs.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;

/// <summary>
/// 
/// </summary>

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;

    private readonly StatisticsService _statisticsS;
    private readonly UserService _userS;
    private readonly OrderService _orderS;

    public StatisticsController(ILogger<StatisticsController> logger, StatisticsService statisticsService, UserService userS, OrderService orderS)
    {
        _logger = logger;
        _statisticsS = statisticsService;
        _userS = userS;
        _orderS = orderS;
    }

    [HttpPost("viewProduct/{id}"), ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct(int id)
    {
        // TODO: View Product
        throw new NotImplementedException();
    }

    [HttpPost("broughtProduct/productId={productId}&orderId={orderId}"), ValidateAntiForgeryToken]
    public async Task<ActionResult<UserMainDataDTO>> BroughtProduct(int productId, int orderId)
    {
        if (Utils.GetUserIDFromJWT(User) == null)
        {
            return BadRequest("Only Authorized users can brought products.");
        }

        _logger.LogInformation("INFO");

        var dto = await _orderS.GetMainData(orderId);


        //TODO: Validate brought
        throw new NotImplementedException();
    }

    [HttpGet("topProductsVisited/{count}"), ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> ViewProductsTop(int count)
    {
        // TODO: return List<Products> { Size = count }
        throw new NotImplementedException();
    }

    [HttpGet("topProductsVisited"), ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> ViewProductsALL(int id)
    {
        // TODO: retrun full List<Products>
        throw new NotImplementedException();
    }


    [HttpPost("viewTopSells/{count}"), ValidateAntiForgeryToken]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct(int count)
    {
        // TODO: retrun List<Products> { Size = count }
        throw new NotImplementedException();
    }

    [HttpPost("viewTopSells"), ValidateAntiForgeryToken]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct()
    {
        // TODO: retrun full List<Products>
        throw new NotImplementedException();
    }

}
using BLL.DTOs;
using BLL.DTOs.User;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;

    private readonly StatisticsService _statisticsS;
    private readonly UserService _userS;
    private readonly OrderService _orderS;
    private readonly ProductService _productS;

    public StatisticsController(ILogger<StatisticsController> logger, StatisticsService statisticsService, UserService userS, OrderService orderS, ProductService productS)
    {
        _logger = logger;
        _statisticsS = statisticsService;
        _userS = userS;
        _orderS = orderS;
        _productS = productS;
    }

    [HttpGet("topProductsVisited/{count}"), ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<ActionResult<List<ProductDTO>>> ViewProductsTop(uint count)
    {
        var list = await _statisticsS.GetMostPurchasedTop(count);

        return Ok(list);
    }

    [HttpGet("topProductsVisited"), ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> ViewProductsAll()
    {
        var list = await _statisticsS.GetMostPurchased();

        return Ok(list);
    }


    [HttpPost("viewTopSells/{count}"), ValidateAntiForgeryToken]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct(uint count)
    {
        var list = await _statisticsS.GetMostPurchasedTop(count);

        return Ok(list);



    }

    [HttpPost("viewTopSells"), ValidateAntiForgeryToken]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct()
    {
        // TODO: retrun full List<Products>
        throw new NotImplementedException();
    }

}
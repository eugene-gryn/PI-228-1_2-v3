using BLL.DTOs;
using BLL.DTOs.User;
using BLL.Services;
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


    public StatisticsController(ILogger<StatisticsController> logger, StatisticsService statisticsService,
        UserService userS)
    {
        _logger = logger;
        _statisticsS = statisticsService;
        _userS = userS;
    }

    [HttpGet("topProductsVisited/{count:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<ProductDTO>>> ViewProductsTop(uint count)
    {
        var list = await _statisticsS.GetMostViewedTop(count);

        _logger.LogInformation($"Top viewed {count} products loaded");

        return Ok(list);
    }

    [HttpGet("topProductsVisited")]
    [AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> ViewProductsAll()
    {
        var list = await _statisticsS.GetMostViewed();

        _logger.LogInformation("Top viewed products loaded");

        return Ok(list);
    }


    [HttpPost("viewTopSells/{count:int}")]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct(uint count)
    {
        var resAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userS);

        if (resAdminOrModerator)
        {
            var list = await _statisticsS.GetMostPurchasedTop(count);

            _logger.LogInformation($"Top {count} purchased products loaded");


            return Ok(list);
        }

        return Forbid("User must be admin or moderator!");
    }

    [HttpPost("viewTopSells")]
    public async Task<ActionResult<UserMainDataDTO>> ViewProduct()
    {
        var resAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userS);

        if (resAdminOrModerator)
        {
            var list = await _statisticsS.GetMostPurchased();

            _logger.LogInformation("Top purchased products loaded");

            return Ok(list);
        }


        return Forbid("User must be admin or moderator!");
    }
}
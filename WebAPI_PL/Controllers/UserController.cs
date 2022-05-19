using BLL.DTOs.User;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly UserService _userS;
        
    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userS = userService;
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserMainDataDTO>> GetMainData(int id)
    {
        _logger.LogInformation("[GetMainData] call");
        var dto = _userS.GetMainData(id);

        if (dto == null)
        {
            return BadRequest("Bad user id.");
        }
        return Ok(dto);
    }
    
}
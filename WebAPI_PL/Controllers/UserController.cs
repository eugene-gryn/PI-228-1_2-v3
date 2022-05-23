using BLL.DTOs.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly UserService _userS;
        
    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userS = userService;
    }


    /*[HttpPost("register"), AllowAnonymous]
    public async Task<ActionResult<UserMainDataDTO>> Register(UserRegisterDTO registerDto)
    {
        if (await _userS.GetMainData(registerDto.Email)!=null)
        {
            return Problem("User with such email already exists!");
        }
        var updatedDTO = await _userS.Create(registerDto);
        
        return Ok(updatedDTO);
    }*/


    [HttpGet("findByID/{id:int}")]
    public async Task<ActionResult<UserMainDataDTO>> GetMainData(int id)
    {
        if (Utils.GetIDFromJWT(Request) != id)
        {
            return BadRequest("User can get only his own data.");
        }
        
        _logger.LogInformation("[GetMainData] call");
        var dto = await _userS.GetMainData(id);

        if (dto == null)
        {
            return BadRequest("Bad user id.");
        }
        return Ok(dto);
    }

    [HttpGet("findByEmail/{email}")]
    public async Task<ActionResult<UserMainDataDTO>> GetMainData(string email)
    {
        _logger.LogInformation("[GetMainData] call");
        var dto = await _userS.GetMainData(email);

        if (dto == null)
        {
            return BadRequest("Bad user email.");
        }

        return Ok(dto);
    }

}
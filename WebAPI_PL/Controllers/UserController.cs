using System.Security.Claims;
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

    public static async Task<bool?> IsUserAdminOrModerator(ClaimsPrincipal User, UserService service)
    {
        var userId = Utils.GetUserIDFromJWT(User);

        if (userId != null)
        {
            var user = await service.GetMainData(userId.Value);

            if (user != null) return user.IsModerator || user.IsAdmin;
        }

        return null;
    }

    private async Task<bool?> IsUserAdmin()
    {
        var userId = Utils.GetUserIDFromJWT(User);

        if (userId != null)
        {
            var user = await _userS.GetMainData(userId.Value);

            if (user != null) return user.IsAdmin;
        }

        return null;
    }


    [HttpGet("findByID/{id:int}")]
    public async Task<ActionResult<UserMainDataDTO>> GetMainData(int id)
    {
        if (Utils.GetUserIDFromJWT(User) != id) return BadRequest("User can get only his own data.");

        _logger.LogInformation("[GetMainData] call");
        var dto = await _userS.GetMainData(id);

        if (dto == null) return BadRequest("Bad user id.");
        return Ok(dto);
    }

    [HttpGet("getAuthorizedUser")]
    public async Task<ActionResult<UserMainDataDTO>> GetUserData()
    {
        _logger.LogInformation("[GetUserData] call");

        var userId = Utils.GetUserIDFromJWT(User);
        if (userId != null)
        {
            var user = await _userS.GetMainData(userId.Value);
            return Ok(user);
        }
        return BadRequest("Bad logon.");
    }



    [HttpPost("changePassword/id-{id:int}&password-{password}")]
    public async Task<ActionResult> ChangeUserPassword(int id, string password)
    {
        _logger.LogInformation("[ChangeUserPassword] call");

        var isAdmin = await IsUserAdmin();
        if (isAdmin != null && isAdmin.Value)
        {
            var user = await _userS.GetMainData(id);

            if (user != null)
            {
                AuthController.CreatePasswordHash(password, out var passwordHash, out var userPasswordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = userPasswordSalt;

                await _userS.Update(user);

                return Ok();
            }

            return BadRequest("User with this id not found!");
        }
        return BadRequest("Only authorized admins can change password");
    }

    [HttpPost("changeUserStatus/id-{id:int}&isAdmin-{isAdmin}&isModer-{isModer}")]
    public async Task<ActionResult<UserMainDataDTO>> ChangeUserStatus(int id, bool isAdmin, bool isModer)
    {
        _logger.LogInformation("[ChangeUserStatus] call");

        var isAdminRequest = await IsUserAdmin();
        if (isAdminRequest != null && isAdminRequest.Value)
        {
            var user = await _userS.GetMainData(id);

            if (user != null)
            {
                user.IsAdmin = isAdmin;
                user.IsModerator = isModer;

                await _userS.Update(user);

                return Ok(user);

            }

            return BadRequest("User with this id not found!");
        }
        return BadRequest("Only authorized admins can change status");
    }

    [HttpDelete("removeUser/id-{id:int}")]
    public async Task<ActionResult<UserMainDataDTO>> RemoveUser(int id)
    {
        _logger.LogInformation("[ChangeUserStatus] call");

        var isAdminRequest = await IsUserAdmin();
        if (isAdminRequest != null && isAdminRequest.Value)
        {
            try
            {
                // TODO ADD CASCADE DELETING FROM EXCEPTION
                await _userS.Remove(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        return BadRequest("Only authorized admins can change status");
    }





    [HttpGet("findByEmail/{email}")]
    public async Task<ActionResult<UserMainDataDTO>> GetMainData(string email)
    {
        _logger.LogInformation("[GetMainData] call");
        var dto = await _userS.GetMainData(email);

        if (dto == null) return BadRequest("Bad user email.");

        return Ok(dto);
    }
}
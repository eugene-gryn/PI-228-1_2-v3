using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BLL.DTOs.User;
using BLL.Services;

namespace WebAPI_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public AuthController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        #region API methods

        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult<UserMainDataDTO>> Register(UserRegisterDTO registerDto)
        {
            if (await _userService.GetMainData(registerDto.Email)!=null)
            {
                return Problem("User with such email already exists!");
            }
            
            CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userMainDTO = new UserMainDataDTO()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            
            var updatedDTO = await _userService.Create(userMainDTO);
        
            return Ok(updatedDTO);
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO request)
        {
            var u = await _userService.GetMainData(request.Email);
            if (u == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, u.PasswordHash, u.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(u);
            
            SetRefreshToken(u);
            await _userService.Update(u);
            

            return Ok(token);
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            
            
            var userID = Utils.GetUserIDFromJWT(User);
            if (userID == null) return Unauthorized("Could not get your ID :(");

            var u = await _userService.GetMainData((int) userID);
            if (u == null) return NotFound("Can not refresh the token - user with needed ID not found!");

            if (!u.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            if(u.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(u);
            SetRefreshToken(u);
            await _userService.Update(u);
            

            return Ok(token);
        }

        #endregion
        

        #region Assist methods

        private void SetRefreshToken(UserMainDataDTO userMainDataDto)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var created = DateTime.Now.AddDays(7);
            var expires = DateTime.Now;
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);

            userMainDataDto.RefreshToken = token;
            userMainDataDto.TokenCreated = created;
            userMainDataDto.TokenExpires = expires;
        }

        
        private string CreateToken(UserMainDataDTO userMainDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userMainDTO.ID.ToString()),
                new Claim(ClaimTypes.Name, userMainDTO.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        #endregion

        
    }
}
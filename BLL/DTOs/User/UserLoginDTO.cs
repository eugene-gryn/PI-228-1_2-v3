using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.User;

public class UserLoginDTO
{
    [Required][EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
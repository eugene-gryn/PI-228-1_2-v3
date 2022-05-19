using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.User;

public class UserRegisterDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required][EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; }
}
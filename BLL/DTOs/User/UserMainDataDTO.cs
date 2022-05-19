using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.User;

public class UserMainDataDTO
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; }

    public bool IsAdmin;
    public bool IsModer;
}
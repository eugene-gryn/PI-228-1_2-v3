using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.User;

public class UserMainDataDTO
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required][EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
    
    
    [Required]
    public byte[] PasswordHash { get; set; }
    [Required]
    public byte[] PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }

    public bool IsAdmin;
    public bool IsModerator;
}
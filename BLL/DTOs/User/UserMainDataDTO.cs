using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.User;

public class UserMainDataDTO
{
    [Required] public byte[] PasswordHash;

    [Required] public byte[] PasswordSalt;

    public int ID { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Phone] public string Phone { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool IsModerator { get; set; }


    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
}
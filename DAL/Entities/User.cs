using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class User
{

    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    [Required]
    public byte[] PasswordHash { get; set; }
    [Required]
    public byte[] PasswordSalt { get; set; }
    
    
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    

    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductAmount> Cart { get; set; }
    
    public bool IsAdmin;
    public bool IsModer;
}
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
    public string Password { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductAmount> Cart { get; set; }
    
    public bool IsAdmin;
    public bool IsModer;
}
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class User
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Phone { get; set; }
    [Required]
    public string Password { get; set; }

    public List<Order> Orders { get; set; }
    public List<Product> Cart { get; set; }
    
    public bool IsAdmin;
    public bool IsModer;
}
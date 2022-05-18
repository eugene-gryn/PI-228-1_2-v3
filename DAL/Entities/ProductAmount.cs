using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class ProductAmount
{
    [Key]
    public int ID { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
    
    public ICollection<Order> OrdersThatHaveThisPM { get; set; }
    public ICollection<User> UsersThatHaveThisPMInCart { get; set; }
}
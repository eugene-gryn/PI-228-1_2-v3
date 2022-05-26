using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Order
{
    [Key]
    public int ID { get; set; }
    [Required]
    public int UserID { get; set; }
    public string DeliveryInfo { get; set; } = string.Empty;
    public bool Processed { get; set; }
    
    public ICollection<ProductAmount> ProductAmounts { get; set; }
}
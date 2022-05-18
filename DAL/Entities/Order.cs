using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Order
{
    [Key]
    public int ID { get; set; }
    [Required]
    public int UserID { get; set; }

    public Dictionary<Product, long> Products { get; set; } = new Dictionary<Product, long>();//TODO int
    public string DeliveryInfo { get; set; } = string.Empty;
    public bool Processed { get; set; }
}
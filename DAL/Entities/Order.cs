using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Order
{
    public int ID { get; set; }
    [Required]
    public int UserID { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public string DeliveryInfo { get; set; } = string.Empty;
    public bool Processed { get; set; }
}
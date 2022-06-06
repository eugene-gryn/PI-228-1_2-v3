using System.ComponentModel.DataAnnotations;
using BLL.DTOs.Product;

namespace BLL.DTOs;

public class OrderDTO
{
    public int ID { get; set; }
    [Required]
    public int UserID { get; set; }
    [Required]
    public string DeliveryInfo { get; set; } = string.Empty;
    
    public bool Processed;

    public List<ProductAmountDTO> ProductAmounts { get; set; }
}
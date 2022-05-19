namespace BLL.DTOs.User;

public class UserOrdersDTO
{
    public ICollection<OrderDTO> Orders { get; set; }
}
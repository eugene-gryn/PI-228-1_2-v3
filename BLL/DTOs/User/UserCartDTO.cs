namespace BLL.DTOs.User;

public class UserCartDTO
{
    public Dictionary<ProductDTO, int> Cart { get; set; }
}
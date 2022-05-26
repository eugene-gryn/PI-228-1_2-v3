using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    //Add product
    //change product
    //filtration / sort product
    // page with mo info
    private readonly ProductService _productS;
    private readonly UserService _userS;

    /// <summary>
    /// TODO: При отриманні детальної інформції про продукт повідомлять про перегляд
    /// </summary>
    private readonly StatisticsService _statisticsS;

    public ProductsController(ProductService productService, UserService userService, StatisticsService statisticsS)
    {
        _productS = productService;
        _userS = userService;
        _statisticsS = statisticsS;
    }


    [HttpGet("productData/{productID:int}")]
    public async Task<ActionResult<ProductDTO>> GetProductData(int productID)
    {
        var productData = await _productS.GetMainData(productID);

        if (productData == null)
        {
            return NotFound();
        }
        
        return Ok(productData);
    }
    

    [HttpDelete("deleteProduct/{productID:int}")]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(int productID)
    {
        var productData = await _productS.GetMainData(productID);
        if (productData == null)
        {
            return NotFound();
        }

        var thisUserID = Utils.GetUserIDFromJWT(User);
        if (thisUserID == null) return BadRequest("User ID not found.");

        var thisUser = await _userS.GetMainData((int)thisUserID);

        if(thisUser.IsModer || thisUser.IsAdmin)
        {
            await _productS.DeleteProduct(productID);
            return Ok(productData);
        }

        return Forbid("You are not moderator.");
    }
    [HttpGet("productData/{productID:int}")]
    //public async Task<ActionResult<ProductDTO>> GetView(int productID)
    //{

    //}
}

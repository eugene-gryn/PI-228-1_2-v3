using BLL.DTOs;
using BLL.DTOs.Product;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    //Add product
    //change product
    //filtration / sort product
    // page with mo info
    private readonly ProductService _productS;
    private readonly StatisticsService _statisticsS;
    private readonly UserService _userS;

    public ProductsController(ProductService productService, UserService userService, StatisticsService statisticsS)
    {
        _productS = productService;
        _userS = userService;
        _statisticsS = statisticsS;
    }


    [HttpGet("productsPreview"), AllowAnonymous]
    public async Task<ActionResult<ProductShortDTO>> GetProducts()
    {
        var list = await _productS.GetProductShortDTOs(null);

        return Ok(list);
    }

    [HttpGet("productsPreview/{count:int}"), AllowAnonymous]
    public async Task<ActionResult<ProductShortDTO>> GetProducts(uint count)
    {
        var list = await _productS.GetProductShortDTOs(count);

        return Ok(list);
    }

    [HttpGet("productData/{productID:int}"), AllowAnonymous]
    public async Task<ActionResult<ProductDTO>> GetProductData(int productID)
    {
        var res = await _statisticsS.AddView(productID);

        var productData = await _productS.GetMainData(productID);

        if (productData == null && !res) return NotFound();


        return Ok(productData);
    }




    [HttpPut("createProduct")]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductCreateDTO product)
    {
        var resAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userS);

        if (resAdminOrModerator == null) return new NotFoundResult();
        if (resAdminOrModerator.Value)
        {
            var result = await _productS.Create(product);

            if (result == null) return NotFound();

            return Ok(product);
        }


        return Forbid("User must be admin or moderator!");
    }

    [HttpPost("updateProduct")]
    public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO product)
    {
        var resAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userS);

        if (resAdminOrModerator == null) return new NotFoundResult();
        if (resAdminOrModerator.Value)
        {
            var res = await _productS.Update(product);

            if (res == null) return NotFound();

            return Ok(res);
        }


        return Forbid("User must be admin or moderator!");
    }

    [HttpDelete("deleteProduct/{productID:int}")]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(int productID)
    {
        var productData = await _productS.GetMainData(productID);


        var resAdminOrModerator = await UserController.IsUserAdminOrModerator(User, _userS);

        if (resAdminOrModerator == null) return new NotFoundResult();
        if (resAdminOrModerator.Value)
        {
            await _productS.DeleteProduct(productID);
            return Ok(productData);
        }


        return Forbid("User must be admin or moderator!");
    }

    [HttpGet("searchProduct/{query}"), AllowAnonymous]
    public async Task<ActionResult<ProductDTO>> SearchProduct(string query)
    {
        var list = await _productS.Search(query);

        return Ok(list);
    }

}
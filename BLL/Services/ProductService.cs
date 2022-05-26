using System.Data.Entity;
using BLL.DTOs;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services;

public class ProductService : AService
{
    public ProductService(IUnitOfWork uow) : base(uow) { }
    

    public async Task<ProductDTO?> Create(ProductDTO productDTO)
    {
        var product = Mapper.Map<Product>(productDTO);

        await Database.Products.Create(product);
        Database.Save();

        return await GetMainData(product.ID);
    }

    
    public async Task<ProductDTO?> GetMainData(int productID)
    {
        var product = await Database.Products.Read().FirstOrDefaultAsync(prod => prod.ID == productID);
        if (product == null) return null;
        return Mapper.Map<ProductDTO?>(product);
    }


    public async Task<bool> DeleteProduct(int productID)
    {
        return await Database.Products.Delete(productID);
    }


    public async Task<ProductDTO> ProductViews(int productID)
    {
        var product = await Database.Products.Read().FirstOrDefaultAsync(prod => prod.ID == productID);
        if(product == null) return null;
        product.Views++;
        return await GetMainData(productID);
    }
}
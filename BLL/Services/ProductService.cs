using System.Data.Entity;
using BLL.DTOs;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services;

public class ProductService : AService
{
    public ProductService(IUnitOfWork uow) : base(uow)
    {
    }

    public async Task<ProductDTO?> Create(ProductDTO productDTO)
    {
        var product = Mapper.Map<Product>(productDTO);

        Database.Products.Create(product);
        Database.Save();

        return await GetMainData(product.ID);
    }

    public async Task<ProductDTO?> GetMainData(int productID)
    {
        var product = await Database.Products.Read().FirstOrDefaultAsync(prod => prod.ID == productID);
        if (product == null) return null;
        return Mapper.Map<ProductDTO?>(product);
    }
}
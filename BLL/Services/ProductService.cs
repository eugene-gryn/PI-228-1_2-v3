using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using BLL.DTOs;
using BLL.DTOs.Product;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services;

public class ProductService : AService
{
    public ProductService(IUnitOfWork uow) : base(uow) { }

    private async Task<List<Product>> GetProducts(uint? count)
    {
        if (count.HasValue && Database.Products.Read().Count() >= count)
        {
            return await Database.Products.Read().Take((int)count.Value).AsNoTracking().ToListAsync();
        }
        return await Database.Products.Read().AsNoTracking().ToListAsync();
    }

    public async Task<List<ProductShortDTO>> GetProductShortDTOs(uint? count)
    {
        var list = await GetProducts(count);

        return list.Select(product => Mapper.Map<ProductShortDTO>(product)).ToList();
    }

    public async Task<ProductDTO?> GetMainData(int productID)
    {
        var product = await Database.Products.Read().FirstOrDefaultAsync(prod => prod.ID == productID);
        if (product == null) return null;
        return Mapper.Map<ProductDTO?>(product);
    }

    public async Task<ProductDTO?> Create(ProductCreateDTO productDTO)
    {
        var product = Mapper.Map<Product>(productDTO);

        await Database.Products.Create(product);
        Database.Save();

        return await GetMainData(product.ID);
    }

    public async Task<ProductDTO?> Update(ProductDTO productDTO)
    {
        var product = Mapper.Map<Product>(productDTO);

        await Database.Products.Update(product);
        Database.Save();

        return await GetMainData(product.ID);
    }

    public async Task<List<ProductShortDTO>?> Search(string query)
    {
        var list = await Database.Products.Read().Where(product => product.Name.ToLower().Contains(query.ToLower()) || product.Description.ToLower().Contains(query.ToLower()))
            .AsNoTracking().Select(product => Mapper.Map<ProductShortDTO>(product)).ToListAsync();


        return list;
    }


    public async Task<bool> DeleteProduct(int productID)
    {
        var result = await Database.Products.Delete(productID);

        Database.Save();

        return result;
    }



}
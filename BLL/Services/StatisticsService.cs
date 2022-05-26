using Microsoft.EntityFrameworkCore;
using BLL.DTOs;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services;

public class StatisticsService : AService
{
    public StatisticsService(IUnitOfWork uow) : base(uow)
    {
    }

    private async Task<Product?> GetProductData(int id)
    {
        return await Database.Products.Read()
            .AsNoTracking().FirstOrDefaultAsync(product => product.ID == id);
    }

    private async Task<bool> ActionWithProduct(int productId, Action<Product> action)
    {
        var product = await GetProductData(productId);

        if (product != null)
        {
            action(product);

            var result = await Database.Products.Update(product);

            if (result)
            {
                Database.Save();
                return result;
            }
        }

        return false;
    }

    public async Task<bool> AddView(int productId)
    {
        return await ActionWithProduct(productId, product => product.Views++);
    }

    public async Task<bool> AddBought(int productId)
    {
        return await ActionWithProduct(productId, product => product.Purchase++);
    }

    private async Task<List<Product>> GetProducts(uint? count)
    {
        if (count.HasValue && Database.Products.Read().Count().CompareTo((int) count) == 0)
        {
            return await Database.Products.Read().Take((int) count.Value).AsNoTracking().ToListAsync();
        }
        return await Database.Products.Read().AsNoTracking().ToListAsync();
    }

    private async Task<List<ProductDTO>> GetProductsSpecialSorted(uint? count, Comparison<ProductDTO> comparer)
    {
        var list = await GetProducts(count);

        var productDtos = list.Select(product => Mapper.Map<ProductDTO>(product)).ToList();

        productDtos.Sort(comparer);

        return productDtos;
    }

    public async Task<List<ProductDTO>> GetMostViewed()
    {
        return await GetProductsSpecialSorted(null, (dto, productDto) => dto.Views.CompareTo(productDto.Views));
    }
    public async Task<List<ProductDTO>> GetMostViewedTop(uint count)
    {
        return await GetProductsSpecialSorted(count, (dto, productDto) => dto.Views.CompareTo(productDto.Views));
    }

    public async Task<List<ProductDTO>> GetMostPurchased()
    {
        return await GetProductsSpecialSorted(null, (dto, productDto) => dto.Purchase.CompareTo(productDto.Purchase));
    }
    public async Task<List<ProductDTO>> GetMostPurchasedTop(uint count)
    {
        return await GetProductsSpecialSorted(count, (dto, productDto) => dto.Purchase.CompareTo(productDto.Purchase));
    }
}
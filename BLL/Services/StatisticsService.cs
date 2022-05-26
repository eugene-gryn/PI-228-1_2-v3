using System.Data.Entity;
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

    public async Task<bool> AddView(int productId)
    {
        return await ActionWithProduct(productId, product => product.Views++);
    }
    public async Task<bool> AddBought(int productId)
    {
        return await ActionWithProduct(productId, product => product.Purchase++);
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
}
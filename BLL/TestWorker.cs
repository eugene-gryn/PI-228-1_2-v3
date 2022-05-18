using DAL.EF;
using DAL.Entities;

namespace BLL;

public class TestWorker
{
    public TestWorker()
    {
        using (var context = new MainContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            context.Products.Add(new Product() {Name = "Computer"});
            context.Products.Add(new Product() {Name = "Laptop"});
            context.SaveChanges();
        }
    }
}
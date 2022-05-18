using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL;

public class TestWorker
{
    public TestWorker()
    {
        using (var context = new MainContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            
            var p1 = new Product() {Name = "Computer"};
            var p2 = new Product() {Name = "Laptop"};
            context.Products.Add(p1);
            context.Products.Add(p2);
            context.SaveChanges();

            
            var u1 = new User()
            {
                Name = "TestUser",
                Email = "test@email.com",
                Password = "12345",
                Cart = new List<ProductAmount>() {new ProductAmount() {Product = p1, Amount = 22}}
            };
            context.Users.Add(u1);
            context.SaveChanges();


            var o1 = new Order() { UserID = u1.ID, ProductAmounts = new List<ProductAmount>(){new ProductAmount(){Product = p2, Amount = 33}}};
            context.Orders.Add(o1);
            context.SaveChanges();
        }
        
        
        
        using (var context = new MainContext())
        {
            var user = context.Users.Include(u => u.Cart).First();
            Console.WriteLine($"Name: {user.Name}");
            
            var cart = user.Cart;
            

            Console.WriteLine($"Count in cart: {cart.Count}");
            

            /*foreach (var p in cart)
            {
                Console.WriteLine($"{p.Product.Name}: {p.Amount} шт.");
            }*/

        }
        
    }
}
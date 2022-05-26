using System.Security.Cryptography;
using DAL.EF;
using DAL.Entities;
using DelegateDecompiler;

namespace BLL;

public class TestWorker
{
    public static KeyValuePair<byte[], byte[]> GeneratePassword(string password)
    {
        byte[] passwordHash, passwordSalt;


        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }


        return new KeyValuePair<byte[], byte[]>(key:passwordHash, value:passwordSalt);
    }

    public TestWorker()
    {
        using (var context = new MainContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var products = new List<Product>
            {
                new()
                {
                    Name = "Computer",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Laptop",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "Portable PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Phone",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "hand PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Joker",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "Card from minecraft",
                    RemainingStock = Random.Shared.Next(100)
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();


            var defaultU = GeneratePassword("default");
            var moderatorU = GeneratePassword("moderator");
            var adminU = GeneratePassword("admin");

            var users = new List<User>
            {
                new()
                {
                    Name = "default",
                    Email = "default@email.com",
                    PasswordHash = defaultU.Key,
                    PasswordSalt = defaultU.Value,
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}}
                },

                new()
                {
                    Name = "moderator",
                    Email = "moderator@email.com",
                    PasswordHash = moderatorU.Key,
                    PasswordSalt = moderatorU.Value,
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}},
                    IsModerator = true
                },

                new()
                {
                    Name = "admin",
                    Email = "admin@email.com",
                    PasswordHash = adminU.Key,
                    PasswordSalt = adminU.Value,
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}},
                    IsAdmin = true
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();


            var orders = new List<Order>
            {
                new()
                {
                    UserID = users[0].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[0], Amount = 33}}
                },
                new()
                {
                    UserID = users[1].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[1], Amount = 33}}
                },
                new()
                {
                    UserID = users[2].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[2], Amount = 33}}
                }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }


        /*using (var context = new MainContext())
        {
            var user = context.Users.Include(u => u.Cart).ThenInclude(pa => pa.Product).First();
            Console.WriteLine($"Name: {user.Name}");

            var cart = user.Cart;


            Console.WriteLine($"Count in cart: {cart.Count}");


            foreach (var pa in cart) Console.WriteLine($"    {pa.Product.Name}: {pa.Amount} шт.");
        }*/
    }
}
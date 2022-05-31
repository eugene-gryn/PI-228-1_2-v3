using BLL.Services;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
   
    public class ServicesTest
    {
        private static DbContextOptions<MainContext> dbContextOptions = new DbContextOptionsBuilder<MainContext>()
            .UseInMemoryDatabase(databaseName: "DbTest")
            .Options;

        MainContext context;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new MainContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    ID = 1,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),
                },
                new Order()
                {
                    ID = 2,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),

                },
                new Order()
                {
                    ID = 3,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),
                },
            };
            context.Orders.AddRange(orders);

            var products = new List<Product>()
            {
                new Product()
                {
                    ID = 1,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                },
                new Product()
                {
                    ID = 2,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                },
                new Product()
                {
                    ID = 3,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                }
            };
            context.Products.AddRange(products);

            var users = new List<User>()
            {
                new User()
                {
                    ID = 1,
                    Name = "test1",
                    Email = "test1@gmail.com",
                    Phone = "+380970000000",
                    IsAdmin = false,
                    IsModerator = false,
                },
                new User()
                {
                    ID = 2,
                    Name = "test2",
                    Email = "test2@gmail.com",
                    Phone = "+380970000000",
                    IsAdmin = false,
                    IsModerator = false,
                },
                new User()
                {
                    ID = 3,
                    Name = "test3",
                    Email = "test3@gmail.com",
                    Phone = "+380970000000",
                    IsAdmin = false,
                    IsModerator = false,
                }
            };
            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}

using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany<Order>(user => user.Orders)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany<ProductAmount>(user => user.Cart)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Order>()
            .HasMany<ProductAmount>(order => order.ProductAmounts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

}
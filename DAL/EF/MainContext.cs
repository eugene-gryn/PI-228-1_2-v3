using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }


    //TODO: Cascade delete
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder
    //        .Entity<Blog>()
    //        .HasOne(e => e.Owner)
    //        .WithOne(e => e.OwnedBlog)
    //     !! .OnDelete(DeleteBehavior.ClientCascade);
    //}
}
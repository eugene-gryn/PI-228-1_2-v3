using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class MainContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    private string _dbPath { get; }
    
    public MainContext()
    {
        var folderPath = Environment.CurrentDirectory; //TODO bind to appsettings.json
        _dbPath = System.IO.Path.Join(folderPath, "shop.db");
    }

    

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}"); //TODO change to MS SQL

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
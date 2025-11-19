using Foodezon.Core.Models;
using Microsoft.EntityFrameworkCore;

public class FoodezonDbContext : DbContext
{
    public FoodezonDbContext(DbContextOptions<FoodezonDbContext> options) : base(options)
    {
    }
    public DbSet<Dish> Dishes{ get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts{ get; set; }
    public DbSet<Order> Orders{ get; set; }
    public DbSet<OrderItem> OrderItems{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(d => d.DishId);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Details).HasMaxLength(600);
            entity.Property(d => d.Price).HasColumnType("decimal(10,2)").IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.CategoryId);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Details).HasMaxLength(200);
        });
    }
}
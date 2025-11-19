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

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(d => d.DiscountId);
            entity.Property(d => d.DiscountCode).IsRequired().HasMaxLength(30);
            entity.Property(d => d.Percentage).IsRequired();
            entity.HasOne<Dish>().WithMany().HasForeignKey(d => d.DishId).OnDelete(deleteBehavior : DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(100);
            entity.Property(o => o.OrderStatus).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.OrderItemId);
            entity.Property(oi => oi.Quantity).IsRequired();
            entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
            entity.HasOne<Order>().WithMany().HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Dish>().WithMany().HasForeignKey(oi => oi.DishId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
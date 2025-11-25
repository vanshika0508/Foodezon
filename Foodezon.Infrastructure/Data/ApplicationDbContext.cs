
using Microsoft.EntityFrameworkCore;
using Foodezon.Core.Models;

namespace Foodezon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            });

         
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Description).HasMaxLength(200);
            });

          
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);
                entity.Property(d => d.Price).HasColumnType("decimal(18,2)");
                
                entity.HasOne(d => d.Category)
                      .WithMany(c => c.Dishes)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

      
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(c => c.User)
                      .WithOne(u => u.Cart)
                      .HasForeignKey<Cart>(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

           
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(ci => ci.Cart)
                      .WithMany(c => c.CartItems)
                      .HasForeignKey(ci => ci.CartId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(ci => ci.Dish)
                      .WithMany(d => d.CartItems)
                      .HasForeignKey(ci => ci.DishId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(o => o.OrderNumber).IsUnique();
                entity.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(o => o.Discountamount).HasColumnType("decimal(18,2)");
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(o => o.DeliveryAddress).HasMaxLength(500);
                
                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(o => o.Discount)
                      .WithMany(d => d.Orders)
                      .HasForeignKey(o => o.DiscountId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
                
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(oi => oi.Dish)
                      .WithMany(d => d.OrderItems)
                      .HasForeignKey(oi => oi.DishId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

         
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasIndex(d => d.code).IsUnique();
                entity.Property(d => d.code).IsRequired().HasMaxLength(20);
                entity.Property(d => d.Description).HasMaxLength(100);
                entity.Property(d => d.Percentage).HasColumnType("decimal(5,2)");
            });
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Beverages",
                    Description = "Hot and Cold drinks"
                },
                 new Category
                {
                    Id = 2,
                    Name = "Appetizer",
                    Description = "Starters and small bites"
                },
                new Category
                {
                    Id = 3,
                    Name = "Mains",
                    Description = "Main course dishes"
                },
               
                new Category
                {
                    Id = 4,
                    Name = "Desserts",
                    Description = "Sweet dishes"
                },
                new Category
                {
                    Id = 5,
                    Name = "Breads",
                    Description = "Naan,roti and other breads"
                }
            );
            modelBuilder.Entity<Dish>().HasData(
                // Beverages (CategoryId = 1)
                new Dish { Id = 1, Name = "Tea", Description = "Masala chai made with Indian spices and milk.", Price = 2.49m, ImageUrl = "", IsAvailable = true, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 2, Name = "Coffee", Description = "Freshly brewed hot coffee.", Price = 2.99m, ImageUrl = "", IsAvailable = true, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 3, Name = "Ice Tea", Description = "Chilled lemon iced tea.", Price = 3.49m, ImageUrl = "", IsAvailable = true, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 4, Name = "Coca Cola", Description = "Classic fizzy cola drink.", Price = 2.49m, ImageUrl = "", IsAvailable = true, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 5, Name = "Fanta", Description = "Orange flavored soft drink.", Price = 2.49m, ImageUrl = "", IsAvailable = true, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

                // Appetizer (CategoryId = 2)
                new Dish { Id = 6, Name = "Samosa", Description = "Crispy pastry stuffed with spiced potatoes and peas.", Price = 4.99m, ImageUrl = "", IsAvailable = true, CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 7, Name = "Pakora", Description = "Mixed vegetable fritters fried till golden.", Price = 5.99m, ImageUrl = "", IsAvailable = true, CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 8, Name = "Manchurian", Description = "Crispy vegetable balls tossed in Indo-Chinese sauce.", Price = 8.99m, ImageUrl = "", IsAvailable = true, CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 9, Name = "Spring Roll", Description = "Crispy rolls stuffed with veggies and noodles.", Price = 6.49m, ImageUrl = "", IsAvailable = true, CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

                // Mains (CategoryId = 3)
                new Dish { Id = 10, Name = "Butter Chicken", Description = "Creamy tomato-based curry with tender chicken pieces.", Price = 15.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 11, Name = "Dal Makhni", Description = "Slow-cooked black lentils in a rich buttery gravy.", Price = 12.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 12, Name = "Khoya Kaju", Description = "Cashew nuts cooked in rich khoya gravy.", Price = 14.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 13, Name = "Malai Methi Matar", Description = "Creamy curry with fenugreek leaves and green peas.", Price = 13.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 14, Name = "Shahi Paneer", Description = "Royal paneer curry in rich cashew and cream gravy.", Price = 13.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 15, Name = "Kadhai Paneer", Description = "Paneer cooked with capsicum and onions in spicy masala.", Price = 13.99m, ImageUrl = "", IsAvailable = true, CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

                // Desserts (CategoryId = 4)
                new Dish { Id = 16, Name = "Kulfi", Description = "Traditional Indian ice cream with pistachios and cardamom.", Price = 4.99m, ImageUrl = "", IsAvailable = true, CategoryId = 4, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 17, Name = "Cake", Description = "Soft slice of freshly baked cake.", Price = 5.49m, ImageUrl = "", IsAvailable = true, CategoryId = 4, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 18, Name = "Kheer", Description = "Rice pudding cooked in milk with nuts and cardamom.", Price = 4.49m, ImageUrl = "", IsAvailable = true, CategoryId = 4, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 19, Name = "Halwa", Description = "Warm semolina halwa with ghee and nuts.", Price = 4.99m, ImageUrl = "", IsAvailable = true, CategoryId = 4, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 20, Name = "Falooda", Description = "Cold dessert drink with vermicelli, basil seeds and ice cream.", Price = 6.49m, ImageUrl = "", IsAvailable = true, CategoryId = 4, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

                // Breads (CategoryId = 5)
                new Dish { Id = 21, Name = "Tawa Roti", Description = "Whole wheat flatbread cooked on tawa.", Price = 1.99m, ImageUrl = "", IsAvailable = true, CategoryId = 5, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 22, Name = "Poori", Description = "Deep-fried puffed wheat bread (2 pieces).", Price = 3.99m, ImageUrl = "", IsAvailable = true, CategoryId = 5, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 23, Name = "Garlic Naan", Description = "Tandoor-baked naan topped with garlic and butter.", Price = 3.49m, ImageUrl = "", IsAvailable = true, CategoryId = 5, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 24, Name = "Butter Naan", Description = "Soft leavened naan brushed with butter.", Price = 3.49m, ImageUrl = "", IsAvailable = true, CategoryId = 5, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Dish { Id = 25, Name = "Tandoori Roti", Description = "Whole wheat roti cooked in tandoor.", Price = 2.49m, ImageUrl = "", IsAvailable = true, CategoryId = 5, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
            
             }
           public override int SaveChanges()
                {
                    ApplyAuditInfo();
                    return base.SaveChanges();
                }

            public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                ApplyAuditInfo();
                return base.SaveChangesAsync(cancellationToken);
            }

            private void ApplyAuditInfo()
            {
                var entries = ChangeTracker.Entries<Base>();

                var now = DateTime.UtcNow;

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Entity.CreatedAt == default)
                            entry.Entity.CreatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.UpdatedAt = now;
                    }
    }
}
  
    }
}
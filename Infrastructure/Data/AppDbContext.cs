using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
      // Конфигурация сущности Role
            modelBuilder.Entity<Role>()
                .HasKey(r => r.id);
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Конфигурация сущности User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            // Связь "Многие к одному" между User и Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Запрещает каскадное удаление ролей

            // Конфигурация сущности Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .IsRequired();

            // Связь "Многие к одному" между Product и Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull); // При удалении категории не удалять товары, а устанавливать NULL

            // Конфигурация сущности Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Связь "Многие к одному" между Order и User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление заказов при удалении пользователя

            // Конфигурация сущности OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Price)
                .HasPrecision(18, 4); 
            
            // Связь "Многие к одному" между OrderItem и Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // При удалении заказа удаляются все связанные элементы

            // Связь "Многие к одному" между OrderItem и Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // При удалении товара элементы заказа не удаляются

            // Конфигурация сущности ShoppingCart
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<ShoppingCart>()
                .Property(sc => sc.CreatedAt)
                .IsRequired();

            // Связь "Один к одному" между ShoppingCart и User
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithOne(u => u.ShoppingCart)
                .HasForeignKey<ShoppingCart>(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация сущности ShoppingCartItem
            modelBuilder.Entity<ShoppingCartItem>()
                .HasKey(sci => sci.Id);

            // Связь "Многие к одному" между ShoppingCartItem и ShoppingCart
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Cart)
                .WithMany(sc => sc.ShoppingCartItems)
                .HasForeignKey(sci => sci.CartId)
                .OnDelete(DeleteBehavior.Cascade); // При удалении корзины удаляются все её элементы

            // Связь "Многие к одному" между ShoppingCartItem и Product
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(sci => sci.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // При удалении товара элементы корзины не удаляются

            // Конфигурация сущности Review
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired();
            modelBuilder.Entity<Review>()
                .Property(r => r.Comment)
                .HasMaxLength(1000);
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedAt)
                .IsRequired();

            // Связь "Многие к одному" между Review и User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь "Многие к одному" между Review и Product
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Заполнение начальных данных для Role
            modelBuilder.Entity<Role>().HasData(
                new Role { id = 1, Name = "Admin" },
                new Role { id = 2, Name = "User" }
            );

            base.OnModelCreating(modelBuilder);
    }
}
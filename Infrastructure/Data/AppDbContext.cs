using Domain.Entities;
using Domain.Enums;
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
    
        var baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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

            // Роли
            modelBuilder.Entity<Role>().HasData(
                new Role { id = 1, Name = "Admin" },
                new Role { id = 2, Name = "User" },
                new Role { id = 3, Name = "Supplier" } // Добавлен поставщик
            );

            // Пароли (реальные пароли в комментариях, хеши SHA256 в данных)
            // Пароль для админа: "Admin123!"
            // Пароль для пользователя: "User123!"
            // Пароль для поставщика: "Supplier123!"
            
            // Пользователи
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "System",
                    Email = "admin@example.com",
                    PasswordHash = "3eb3fe66b31e3b4d10fa70b5cad49c7112294af6ae4e476a1c405155d45aa121", // SHA256 от "Admin123!"
                    RoleId = 1,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate,
                },
                new User
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "user@example.com",
                    PasswordHash = "bc5848f227cc161eb5f68dfe98cb13110a9c843ce69e953a88107d865583d397", // SHA256 от "User123!"
                    RoleId = 2,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                },
                new User
                {
                    Id = 3,
                    FirstName = "Supplier",
                    LastName = "Company",
                    Email = "supplier@example.com",
                    PasswordHash = "ad26fd82dfd1a497137cac44cfbff4db0c0e515680c070edf000fc5d68e76656", // SHA256 от "Supplier123!!"
                    RoleId = 3,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                }
            );

            modelBuilder.Entity<RefreshToken>().HasData(
            );
            
            // Категории
            modelBuilder.Entity<Category>().HasData(
                new Category 
                { 
                    Id = 1, 
                    Name = "Electronics", 
                    Description = "Electronic devices and components",
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                },
                new Category 
                { 
                    Id = 2, 
                    Name = "Groceries", 
                    Description = "Food products and groceries",
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                },
                new Category 
                { 
                    Id = 3, 
                    Name = "Clothing", 
                    Description = "Apparel and accessories",
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                }
            );

            // Товары
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High-performance business laptop",
                    Price = 1200.00m,
                    Quantity = 50,
                    CategoryId = 1,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 800.00m,
                    Quantity = 100,
                    CategoryId = 1,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                },
                new Product
                {
                    Id = 3,
                    Name = "Organic Apples",
                    Description = "Fresh organic apples, 1kg",
                    Price = 3.50m,
                    Quantity = 500,
                    CategoryId = 2,
                    CreatedAt = baseDate,
                    UpdatedAt = baseDate
                }
            );

            // Корзины покупок
            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart
                {
                    Id = 1,
                    UserId = 2, // Обычный пользователь
                    CreatedAt = baseDate
                }
            );

            // Элементы корзины
            modelBuilder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem
                {
                    Id = 1,
                    CartId = 1,
                    ProductId = 1, // Laptop
                    Quantity = 2
                }
            );

            // Заказы
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 2,
                    Status = Status.Delivered,
                    TotalPrice = 2400.00m, // 2 x Laptop
                    CreatedAt = baseDate.AddDays(-7),
                    UpdatedAt = baseDate.AddDays(-6)
                }
            );

            // Элементы заказа
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1, // Laptop
                    Quantity = 2,
                    Price = 1200.00m
                }
            );

            // Отзывы
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    UserId = 2,
                    ProductId = 1,
                    Rating = 5,
                    Comment = "Excellent laptop for business use",
                    CreatedAt = baseDate.AddDays(-5)
                }
            );

            // Refresh токены
            modelBuilder.Entity<RefreshToken>().HasData(
                new RefreshToken
                {
                    Id = 1,
                    UserId = 1,
                    Token = "admin_refresh_token_sample",
                    ExpiresAt = baseDate.AddDays(30)
                },
                new RefreshToken
                {
                    Id = 2,
                    UserId = 2,
                    Token = "user_refresh_token_sample",
                    ExpiresAt = baseDate.AddDays(30)
                },
                new RefreshToken
                {
                    Id = 3,
                    UserId = 3,
                    Token = "supplier_refresh_token_sample",
                    ExpiresAt = baseDate.AddDays(30)
                }
            );

            base.OnModelCreating(modelBuilder);
    }
}
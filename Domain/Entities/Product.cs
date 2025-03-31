using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    // Связь с отзывами
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    // Связь с элементами корзины
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    // Связь с элементами заказа
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

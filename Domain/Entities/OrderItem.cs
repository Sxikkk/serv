using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; } // Связь с заказом

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; } // Связь с товаром

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

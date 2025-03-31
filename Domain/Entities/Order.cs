using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    public Status Status { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than 0")]
    public decimal TotalPrice { get; set; }

    // Связь с элементами заказа
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

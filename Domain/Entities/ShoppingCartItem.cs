using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CartId { get; set; }
    [ForeignKey("CartId")]
    public ShoppingCart Cart { get; set; } // Связь с корзиной

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; } // Связь с товаром

    public int Quantity { get; set; }
}


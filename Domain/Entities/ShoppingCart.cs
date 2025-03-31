using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ShoppingCart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } // Связь с пользователем

    // Связь с элементами корзины
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public DateTime CreatedAt { get; set; }
}

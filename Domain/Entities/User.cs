using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    // Связь с корзиной покупок
    [JsonIgnore]
    public ShoppingCart ShoppingCart { get; set; }

    // Связь с заказами
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    // Связь с отзывами
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual RefreshToken? RefreshToken { get; set; }
}

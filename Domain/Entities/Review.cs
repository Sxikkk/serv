using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } // Связь с пользователем

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; } // Связь с товаром

    public int Rating { get; set; } // Оценка товара от 1 до 5
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}


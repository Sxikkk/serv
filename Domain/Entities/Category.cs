using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string Description { get; set; }

    // Связь с товарами
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}




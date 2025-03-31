using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Role
{
    [Key]
    public int id { get; set; }
    public string Name { get; set; }

    public ICollection<User> Users = new List<User>();
}
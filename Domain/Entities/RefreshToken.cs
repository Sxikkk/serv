using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }
}
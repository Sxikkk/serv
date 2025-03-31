using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum Status
{
    [Display(Name = "Pending")]
    Pending,

    [Display(Name = "Shipped")]
    Shipped,

    [Display(Name = "Delivered")]
    Delivered,

    [Display(Name = "Canceled")]
    Canceled
}
using System.ComponentModel.DataAnnotations;

namespace Inventory_System_with_Security.Models;

public class Supplier
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(120)]
    public string ContactName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? SupplyHistory { get; set; }
}

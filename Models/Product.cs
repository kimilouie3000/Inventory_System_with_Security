using System.ComponentModel.DataAnnotations;

namespace Inventory_System_with_Security.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string Sku { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int StockLevel { get; set; }

    [Range(0, 999999999)]
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public string IntegrityHash { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace Inventory_System_with_Security.DTOs;

public class ProductRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Sku { get; set; } = string.Empty;
    [Range(0, int.MaxValue)] public int StockLevel { get; set; }
    [Range(0, 999999999)] public decimal Price { get; set; }
    [Required] public int CategoryId { get; set; }
}

public class CategoryRequest
{
    [Required] public string Name { get; set; } = string.Empty;
}

public class SupplierRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string ContactName { get; set; } = string.Empty;
    [Required, EmailAddress] public string ContactEmail { get; set; } = string.Empty;
    public string? SupplyHistory { get; set; }
}

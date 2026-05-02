using Inventory_System_with_Security.Data;
using Inventory_System_with_Security.DTOs;
using Inventory_System_with_Security.Models;
using Inventory_System_with_Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_System_with_Security.Controllers;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductsController(AppDbContext db, IHashService hashService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await db.Products.Include(p => p.Category).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(ProductRequest request)
    {
        var categoryExists = await db.Categories.AnyAsync(c => c.Id == request.CategoryId);
        if (!categoryExists) return BadRequest(new { message = "Category does not exist." });

        var product = new Product
        {
            Name = request.Name.Trim(),
            Sku = request.Sku.Trim().ToUpperInvariant(),
            StockLevel = request.StockLevel,
            Price = request.Price,
            CategoryId = request.CategoryId
        };
        product.IntegrityHash = hashService.ComputeSha256($"{product.Name}|{product.Sku}|{product.StockLevel}|{product.Price}|{product.CategoryId}");

        db.Products.Add(product);
        await db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductRequest request)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();

        product.Name = request.Name.Trim();
        product.Sku = request.Sku.Trim().ToUpperInvariant();
        product.StockLevel = request.StockLevel;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        product.IntegrityHash = hashService.ComputeSha256($"{product.Name}|{product.Sku}|{product.StockLevel}|{product.Price}|{product.CategoryId}");

        await db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return NoContent();
    }
}

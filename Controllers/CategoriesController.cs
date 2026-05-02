using Inventory_System_with_Security.Data;
using Inventory_System_with_Security.DTOs;
using Inventory_System_with_Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_System_with_Security.Controllers;

[ApiController]
[Authorize]
[Route("api/categories")]
public class CategoriesController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await db.Categories.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        var category = new Category { Name = request.Name.Trim() };
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return Ok(category);
    }
}

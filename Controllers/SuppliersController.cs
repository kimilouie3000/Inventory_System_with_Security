using Inventory_System_with_Security.Data;
using Inventory_System_with_Security.DTOs;
using Inventory_System_with_Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_System_with_Security.Controllers;

[ApiController]
[Authorize]
[Route("api/suppliers")]
public class SuppliersController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await db.Suppliers.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(SupplierRequest request)
    {
        var supplier = new Supplier
        {
            Name = request.Name.Trim(),
            ContactName = request.ContactName.Trim(),
            ContactEmail = request.ContactEmail.Trim().ToLowerInvariant(),
            SupplyHistory = request.SupplyHistory?.Trim()
        };

        db.Suppliers.Add(supplier);
        await db.SaveChangesAsync();
        return Ok(supplier);
    }
}

using Inventory_System_with_Security.Data;
using Inventory_System_with_Security.DTOs;
using Inventory_System_with_Security.Models;
using Inventory_System_with_Security.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_System_with_Security.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, IAesEncryptionService aes, IJwtTokenService jwt) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        if (request.Password != request.ConfirmPassword)
            return BadRequest(new { message = "Password and Confirm Password do not match." });

        var email = request.Email.Trim().ToLowerInvariant();
        var exists = await db.Users.AnyAsync(x => x.Email == email);
        if (exists) return Conflict(new { message = "Email already exists." });

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 10),
            FirstNameEncrypted = aes.Encrypt(request.FirstName.Trim()),
            MiddleNameEncrypted = string.IsNullOrWhiteSpace(request.MiddleName) ? null : aes.Encrypt(request.MiddleName.Trim()),
            LastNameEncrypted = aes.Encrypt(request.LastName.Trim())
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(Register), new { user.Id }, new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var email = request.Email.Trim().ToLowerInvariant();
        var user = await db.Users.SingleOrDefaultAsync(x => x.Email == email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials." });

        var token = jwt.GenerateToken(user);
        return Ok(new { token });
    }
}

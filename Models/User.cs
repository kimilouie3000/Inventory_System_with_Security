using System.ComponentModel.DataAnnotations;

namespace Inventory_System_with_Security.Models;

public class User
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string FirstNameEncrypted { get; set; } = string.Empty;

    public string? MiddleNameEncrypted { get; set; }

    [Required]
    public string LastNameEncrypted { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

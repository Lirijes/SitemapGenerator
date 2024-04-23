using System.ComponentModel.DataAnnotations;

namespace SitemapGenerator.ViewModels;

//denna vy har inget med databasen att göra, den representerar formuläret i vyn

public class RegisterViewModel
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Phonenumber")]
    public int? PhoneNumber { get; set; }

    [Display(Name = "Street Name")]
    public string? StreeetName { get; set; }

    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [Display(Name = "City")]
    public string? City { get; set; }
}

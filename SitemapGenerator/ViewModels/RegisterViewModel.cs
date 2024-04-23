using System.ComponentModel.DataAnnotations;

namespace SitemapGenerator.ViewModels;

//denna vy har inget med databasen att göra, den representerar formuläret i vyn

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please enter your User Name")]
    [Display(Name = "User Name")]
    public string UserName { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Confirm Email")]
    public string ConfirmEmail { get; set; } = null!;

    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;
}

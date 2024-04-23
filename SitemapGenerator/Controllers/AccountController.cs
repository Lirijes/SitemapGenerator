using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SitemapGenerator.Models.Entities;
using SitemapGenerator.Contexts;
using SitemapGenerator.ViewModels;

namespace SitemapGenerator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager; // Need this to be able to create a new user using Identity
    private readonly DataContext _context;
    private readonly SignInManager<UserEntity> _signInManager; // Need this to be able to sign in using Sign in Manager
    private readonly IConfiguration _configuration; // Need this to store the token

    public AccountController(UserManager<UserEntity> userManager, DataContext context, SignInManager<UserEntity> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _configuration = configuration;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return Conflict(new { Message = "User is already exists." });
        }

        var user = new UserEntity { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Registration was successful." });
        }

        return BadRequest(new { Errors = result.Errors.Select(x => x.Description) });
    }



    public IActionResult Index()
    {
        return View();
    }
}

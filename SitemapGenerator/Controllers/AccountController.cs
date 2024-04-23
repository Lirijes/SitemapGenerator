using Microsoft.AspNetCore.Mvc;
using SitemapGenerator.ViewModels;

namespace SitemapGenerator.Controllers;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        return View(model);
    }



    public IActionResult Index()
    {
        return View();
    }
}

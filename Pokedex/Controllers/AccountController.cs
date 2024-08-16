
using Microsoft.AspNetCore.Mvc;
using Pokedex.ViewModels;

namespace Pokedex.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginVM loginVM = new()
        {
            UrlRetorno = returnUrl ?? Url.Content("~/")
        };

        return View(loginVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}

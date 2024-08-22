
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pokedex.ViewModels;

namespace Pokedex.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly ILogger<AccountController> _signInManager;
    private readonly ILogger<AccountController> _userManager;

    public AccountController(
        ILogger<AccountController> logger,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager
    ) {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (ModelState.IsValid)
        {
            string username = login.Email;

            if (isValidEmail(login.Email))
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user != null)
                {
                    userName = user.username;
                }

                var result = await _signInManager.passwordSignInAsync(
                    username,
                    login.Senha,
                    login.Lembrar,
                    lockoutOnFaliure: true
                );

                if (result.isSucced)
                {
                    _logger.LogInformation($"Usuário {login.Email} acessou o sistema!");
                    return LocalRedirect(login.UrlRetorno);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning($"Usuário {login.Email} foi bloqueado!");
                    return RedirectToAction("Lockout");
                }
            }

            ModelState.AddModelError(string.Empty, "Credênciais inválidas!");
        }

        return View(login);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        _logger.LogInformation($"Usuário {ClaimTypes.Email} fez logout.");
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Lockout()
    {
        return View();
    }

    private bool isValidEmail(string email)
    {
        try
        {
            MailAddress mail= new(email);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}

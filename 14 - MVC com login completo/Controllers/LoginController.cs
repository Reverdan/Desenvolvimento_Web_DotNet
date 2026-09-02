using System.Security.Claims;
using LoginSeguroMvc.DAL;
using LoginSeguroMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSeguroMvc.Controllers;

public class LoginController(LoginDao loginDao) : Controller
{
    [HttpGet]
    public IActionResult Index(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Usuario))
        {
            ModelState.AddModelError(string.Empty, "Informe usuário e senha.");
            return View(model);
        }

        string usuario = model.Usuario.Trim();
        if (!loginDao.Autenticar(usuario, model.Senha))
        {
            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
            return View(model);
        }

        var claims = new[] { new Claim(ClaimTypes.Name, usuario) };
        var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identidade));

        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction(nameof(Painel));
    }

    [Authorize]
    [HttpGet]
    public IActionResult Painel()
    {
        return View(new DashboardViewModel { Usuario = User.Identity?.Name ?? string.Empty });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sair()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult AcessoNegado()
    {
        return View();
    }
}

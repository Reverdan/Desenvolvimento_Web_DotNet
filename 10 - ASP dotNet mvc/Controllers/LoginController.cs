using LoginMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginMvc.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(LoginViewModel model)
    {
        string nome = (model.Usuario ?? string.Empty).Trim();
        string senhaDigitada = (model.Senha ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senhaDigitada))
        {
            model.Mensagem = "Preencha todos os campos antes de entrar.";
            model.TipoMensagem = "error";
            return View(model);
        }

        if (nome == "admin" && senhaDigitada == "1234")
        {
            model.Mensagem = "Login realizado com sucesso!";
            model.TipoMensagem = "success";
            return View(model);
        }

        model.Mensagem = "Nome de usuário ou senha inválidos.";
        model.TipoMensagem = "error";
        return View(model);
    }
}

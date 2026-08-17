using LoginApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(LoginRequest? request)
    {
        string nome = (request?.Usuario ?? string.Empty).Trim();
        string senha = (request?.Senha ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
        {
            return BadRequest(new LoginResponse
            {
                Sucesso = false,
                Mensagem = "Preencha todos os campos antes de entrar."
            });
        }

        if (nome == "admin" && senha == "1234")
        {
            return Ok(new LoginResponse
            {
                Sucesso = true,
                Mensagem = "Login realizado com sucesso!"
            });
        }

        return Unauthorized(new LoginResponse
        {
            Sucesso = false,
            Mensagem = "Nome de usuário ou senha inválidos."
        });
    }
}

using CRUDPessoas.modelo;
using Microsoft.AspNetCore.Mvc;
using WebApiPessoas.Models;

namespace WebApiPessoas.Controllers;

[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase
{


    [HttpPost("logar")]
    public IActionResult Logar(string usuario, string senha)
    {
        return (usuario == "admin" && senha == "123") ? Ok(new { mensagem = "Login realizado com sucesso." }) : Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
    }

    [HttpGet]
    public IActionResult PesquisarPorNome([FromQuery] string nome = "")
    {
        Controle controle = new();
        List<Pessoa> pessoas = controle.PesquisarPessoaPorNome(nome);
        return Responder(controle, pessoas);
    }

    [HttpGet("{id:int}")]
    public IActionResult PesquisarPorId(int id)
    {
        Controle controle = new();
        Pessoa? pessoa = controle.PesquisarPessoaPorId(id.ToString());
        return Responder(controle, pessoa);
    }

    [HttpPost]
    public IActionResult Cadastrar(PessoaRequest? request)
    {
        if (request is null) return BadRequest(new { mensagem = "Informe os dados da pessoa." });
        Controle controle = new();
        controle.CadastrarPessoa(Dados(request, "0"));
        return Responder(controle, null, StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Editar(int id, PessoaRequest? request)
    {
        if (request is null) return BadRequest(new { mensagem = "Informe os dados da pessoa." });
        Controle controle = new();
        controle.EditarPessoa(Dados(request, id.ToString()));
        return Responder(controle, null);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Excluir(int id)
    {
        Controle controle = new();
        controle.ExcluirPessoa(id.ToString());
        return Responder(controle, null);
    }

    private static List<string> Dados(PessoaRequest request, string id) => [id, request.nome, request.rg, request.cpf];

    private IActionResult Responder(Controle controle, object? dados, int sucesso = StatusCodes.Status200OK)
    {
        if (!string.IsNullOrEmpty(controle.mensagem) && controle.mensagem.StartsWith("Erro", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(new { mensagem = controle.mensagem });
        }
        return StatusCode(sucesso, new { mensagem = controle.mensagem, dados });
    }
}
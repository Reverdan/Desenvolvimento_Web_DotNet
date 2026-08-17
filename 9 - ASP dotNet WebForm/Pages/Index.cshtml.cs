using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Usuario { get; set; } = string.Empty;

    [BindProperty]
    public string Senha { get; set; } = string.Empty;

    public string Mensagem { get; private set; } = string.Empty;
    public string TipoMensagem { get; private set; } = string.Empty;

    public void OnPost()
    {
        string nome = (Usuario ?? string.Empty).Trim();
        string senhaDigitada = (Senha ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senhaDigitada))
        {
            ExibirMensagem("Preencha todos os campos antes de entrar.", "error");
            return;
        }

        if (nome == "admin" && senhaDigitada == "1234")
        {
            ExibirMensagem("Login realizado com sucesso!", "success");
            return;
        }

        ExibirMensagem("Nome de usuário ou senha inválidos.", "error");
    }

    private void ExibirMensagem(string texto, string tipo)
    {
        Mensagem = texto;
        TipoMensagem = tipo;
    }
}

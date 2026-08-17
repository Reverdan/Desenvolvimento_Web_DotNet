namespace LoginMvc.Models;

public class LoginViewModel
{
    public string Usuario { get; set; } = string.Empty;

    public string Senha { get; set; } = string.Empty;

    public string Mensagem { get; set; } = string.Empty;

    public string TipoMensagem { get; set; } = string.Empty;
}

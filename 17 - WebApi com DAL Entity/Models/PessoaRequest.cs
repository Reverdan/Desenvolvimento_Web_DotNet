namespace WebApiPessoas.Models;

public class PessoaRequest
{
    public string nome { get; set; } = string.Empty;
    public string rg { get; set; } = string.Empty;
    public string cpf { get; set; } = string.Empty;
}
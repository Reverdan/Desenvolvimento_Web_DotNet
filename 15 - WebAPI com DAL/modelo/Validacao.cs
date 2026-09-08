namespace CRUDPessoas.modelo;

public class Validacao
{
    public int id { get; set; }
    public string mensagem { get; set; } = string.Empty;

    public void ValidarId(string numId)
    {
        try
        {
            id = Convert.ToInt32(numId);
            mensagem = string.Empty;
        }
        catch
        {
            mensagem = "Erro de conversão";
        }
    }

    public void ValidarDadosPessoa(List<string> listaDadosPessoa)
    {
        mensagem = string.Empty;
        ValidarId(listaDadosPessoa[0]);

        if (string.IsNullOrWhiteSpace(listaDadosPessoa[1]))
        {
            mensagem += "O nome é obrigatório. ";
        }
        else if (listaDadosPessoa[1].Length < 3)
        {
            mensagem += "O nome deve ter no mínimo 3 caracteres. ";
        }
        else if (listaDadosPessoa[1].Length > 50)
        {
            mensagem += "O nome deve ter no máximo 50 caracteres. ";
        }

        if (!string.IsNullOrEmpty(listaDadosPessoa[2]) && listaDadosPessoa[2].Length > 11)
        {
            mensagem += "O RG deve ter no máximo 11 caracteres. ";
        }

        if (!string.IsNullOrEmpty(listaDadosPessoa[3]) && listaDadosPessoa[3].Length > 13)
        {
            mensagem += "O CPF deve ter no máximo 13 caracteres. ";
        }
    }
}
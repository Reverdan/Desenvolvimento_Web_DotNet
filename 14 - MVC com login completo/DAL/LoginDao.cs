using System.Globalization;
using System.Text;

namespace LoginSeguroMvc.DAL;

public sealed class LoginDao
{
    private readonly string _arquivo;

    public LoginDao(IWebHostEnvironment ambiente)
    {
        _arquivo = Path.Combine(ambiente.ContentRootPath, "DAL", "Database", "login-users.txt");
    }

    public bool Autenticar(string usuario, string senha)
    {
        LoginUserRecord? registro = Buscar(usuario);
        return PasswordHasher.Verificar(senha, registro);
    }

    private LoginUserRecord? Buscar(string usuario)
    {
        if (!File.Exists(_arquivo))
        {
            return null;
        }

        foreach (string linha in File.ReadLines(_arquivo, Encoding.UTF8))
        {
            string[] campos = linha.Split('|');
            if (campos.Length != 4 || !string.Equals(campos[0], usuario, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!int.TryParse(campos[1], NumberStyles.None, CultureInfo.InvariantCulture, out int iteracoes))
            {
                return null;
            }

            try
            {
                return new LoginUserRecord(
                    campos[0],
                    iteracoes,
                    Convert.FromBase64String(campos[2]),
                    Convert.FromBase64String(campos[3]));
            }
            catch (FormatException)
            {
                return null;
            }
        }

        return null;
    }
}

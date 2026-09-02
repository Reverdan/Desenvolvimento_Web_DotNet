using System.Security.Cryptography;

namespace LoginSeguroMvc.DAL;

public static class PasswordHasher
{
    private const int TamanhoSalt = 16;
    private const int TamanhoHash = 32;
    public const int IteracoesPadrao = 210_000;

    public static (byte[] Salt, byte[] Hash, int Iteracoes) CriarHash(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(TamanhoSalt);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            IteracoesPadrao,
            HashAlgorithmName.SHA512,
            TamanhoHash);

        return (salt, hash, IteracoesPadrao);
    }

    public static bool Verificar(string senha, LoginUserRecord? usuario)
    {
        byte[] salt = usuario?.Salt ?? new byte[TamanhoSalt];
        byte[] hashEsperado = usuario?.Hash ?? new byte[TamanhoHash];
        int iteracoes = usuario?.Iteracoes > 0 ? usuario.Iteracoes : IteracoesPadrao;
        byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            iteracoes,
            HashAlgorithmName.SHA512,
            hashEsperado.Length);

        return CryptographicOperations.FixedTimeEquals(hashCalculado, hashEsperado);
    }
}

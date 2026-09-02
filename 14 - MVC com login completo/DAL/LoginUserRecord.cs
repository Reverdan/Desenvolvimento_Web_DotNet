namespace LoginSeguroMvc.DAL;

public sealed record LoginUserRecord(
    string Usuario,
    int Iteracoes,
    byte[] Salt,
    byte[] Hash);

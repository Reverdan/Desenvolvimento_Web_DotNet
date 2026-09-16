using Microsoft.Data.SqlClient;

namespace CRUDPessoas.DAL;

public static class Conexao
{
    public static SqlConnection con = new();
    public static string mensagem = string.Empty;
    public static string stringConexao =
        @"Data Source=DESKTOP-0BMMDJG\SQLEXPRESS;Initial Catalog=ds34a;User ID=sa;Password=unip;Encrypt=False";

    public static SqlConnection Conectar()
    {
        mensagem = string.Empty;
        try
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.ConnectionString = stringConexao;
                con.Open();
            }
        }
        catch (Exception ex)
        {
            mensagem += ex.Message;
        }

        return con;
    }

    public static void Desconectar()
    {
        try
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception ex)
        {
            mensagem += ex.Message;
        }
    }
}
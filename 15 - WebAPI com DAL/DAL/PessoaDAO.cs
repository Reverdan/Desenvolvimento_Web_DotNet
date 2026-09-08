using CRUDPessoas.modelo;
using Microsoft.Data.SqlClient;

namespace CRUDPessoas.DAL;

public class PessoaDAO
{
    public void CadastrarPessoa(Pessoa pessoa)
    {
        try
        {
            SqlConnection conexao = Conexao.Conectar();
            const string comandoSql = "INSERT INTO Pessoas (nome, rg, cpf) VALUES (@nome, @rg, @cpf)";
            using SqlCommand comando = new(comandoSql, conexao);
            comando.Parameters.AddWithValue("@nome", pessoa.nome);
            comando.Parameters.AddWithValue("@rg", pessoa.rg);
            comando.Parameters.AddWithValue("@cpf", pessoa.cpf);
            comando.ExecuteNonQuery();
            Conexao.mensagem = "Pessoa cadastrada com sucesso.";
        }
        catch (Exception ex)
        {
            Conexao.mensagem = "Erro ao cadastrar pessoa: " + ex.Message;
        }
        finally { Conexao.Desconectar(); }
    }

    public Pessoa PesquisarPessoaPorId(Pessoa pessoa)
    {
        try
        {
            SqlConnection conexao = Conexao.Conectar();
            const string comandoSql = "SELECT id, nome, rg, cpf FROM Pessoas WHERE id = @id";
            using SqlCommand comando = new(comandoSql, conexao);
            comando.Parameters.AddWithValue("@id", pessoa.id);
            using SqlDataReader leitor = comando.ExecuteReader();
            if (leitor.Read())
            {
                pessoa.id = Convert.ToInt32(leitor["id"]);
                pessoa.nome = leitor["nome"].ToString() ?? string.Empty;
                pessoa.rg = leitor["rg"].ToString() ?? string.Empty;
                pessoa.cpf = leitor["cpf"].ToString() ?? string.Empty;
            }
            Conexao.mensagem = "Pesquisa realizada com sucesso.";
        }
        catch (Exception ex) { Conexao.mensagem = "Erro ao pesquisar pessoa: " + ex.Message; }
        finally { Conexao.Desconectar(); }
        return pessoa;
    }

    public void EditarPessoa(Pessoa pessoa)
    {
        try
        {
            SqlConnection conexao = Conexao.Conectar();
            const string comandoSql = "UPDATE Pessoas SET nome = @nome, rg = @rg, cpf = @cpf WHERE id = @id";
            using SqlCommand comando = new(comandoSql, conexao);
            comando.Parameters.AddWithValue("@id", pessoa.id);
            comando.Parameters.AddWithValue("@nome", pessoa.nome);
            comando.Parameters.AddWithValue("@rg", pessoa.rg);
            comando.Parameters.AddWithValue("@cpf", pessoa.cpf);
            comando.ExecuteNonQuery();
            Conexao.mensagem = "Pessoa editada com sucesso.";
        }
        catch (Exception ex) { Conexao.mensagem = "Erro ao editar pessoa: " + ex.Message; }
        finally { Conexao.Desconectar(); }
    }

    public void ExcluirPessoa(Pessoa pessoa)
    {
        try
        {
            SqlConnection conexao = Conexao.Conectar();
            const string comandoSql = "DELETE FROM Pessoas WHERE id = @id";
            using SqlCommand comando = new(comandoSql, conexao);
            comando.Parameters.AddWithValue("@id", pessoa.id);
            comando.ExecuteNonQuery();
            Conexao.mensagem = "Pessoa excluída com sucesso.";
        }
        catch (Exception ex) { Conexao.mensagem = "Erro ao excluir pessoa: " + ex.Message; }
        finally { Conexao.Desconectar(); }
    }

    public List<Pessoa> PesquisarPessoaPorNome(Pessoa pessoa)
    {
        List<Pessoa> listaPessoas = [];
        try
        {
            SqlConnection conexao = Conexao.Conectar();
            const string comandoSql = "SELECT id, nome, rg, cpf FROM Pessoas WHERE nome LIKE @nome";
            using SqlCommand comando = new(comandoSql, conexao);
            comando.Parameters.AddWithValue("@nome", "%" + pessoa.nome + "%");
            using SqlDataReader leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                listaPessoas.Add(new Pessoa
                {
                    id = Convert.ToInt32(leitor["id"]),
                    nome = leitor["nome"].ToString() ?? string.Empty,
                    rg = leitor["rg"].ToString() ?? string.Empty,
                    cpf = leitor["cpf"].ToString() ?? string.Empty
                });
            }
            Conexao.mensagem = "Pesquisa realizada com sucesso.";
        }
        catch (Exception ex) { Conexao.mensagem = "Erro ao pesquisar pessoa: " + ex.Message; }
        finally { Conexao.Desconectar(); }
        return listaPessoas;
    }
}
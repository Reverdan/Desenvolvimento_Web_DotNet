using CRUDPessoas.DAL;

namespace CRUDPessoas.modelo;

public class Controle
{
    public string mensagem { get; set; } = string.Empty;

    public void CadastrarPessoa(List<string> listaDadosPessoa)
    {
        listaDadosPessoa[0] = "0";
        Validacao validacao = new();
        validacao.ValidarDadosPessoa(listaDadosPessoa);
        if (!string.IsNullOrEmpty(validacao.mensagem)) { mensagem = validacao.mensagem; return; }
        Pessoa pessoa = new() { id = 0, nome = listaDadosPessoa[1], rg = listaDadosPessoa[2], cpf = listaDadosPessoa[3] };
        new PessoaDAO().CadastrarPessoa(pessoa);
        mensagem = Conexao.mensagem;
    }

    public Pessoa? PesquisarPessoaPorId(string numId)
    {
        Validacao validacao = new();
        validacao.ValidarId(numId);
        if (!string.IsNullOrEmpty(validacao.mensagem)) { mensagem = validacao.mensagem; return null; }
        Pessoa pessoa = new() { id = validacao.id };
        Pessoa pessoaRetorno = new PessoaDAO().PesquisarPessoaPorId(pessoa);
        mensagem = Conexao.mensagem;
        return pessoaRetorno;
    }

    public void EditarPessoa(List<string> listaDadosPessoa)
    {
        Validacao validacao = new();
        validacao.ValidarDadosPessoa(listaDadosPessoa);
        if (!string.IsNullOrEmpty(validacao.mensagem)) { mensagem = validacao.mensagem; return; }
        Pessoa pessoa = new() { id = validacao.id, nome = listaDadosPessoa[1], rg = listaDadosPessoa[2], cpf = listaDadosPessoa[3] };
        new PessoaDAO().EditarPessoa(pessoa);
        mensagem = Conexao.mensagem;
    }

    public void ExcluirPessoa(string numId)
    {
        Validacao validacao = new();
        validacao.ValidarId(numId);
        if (!string.IsNullOrEmpty(validacao.mensagem)) { mensagem = validacao.mensagem; return; }
        new PessoaDAO().ExcluirPessoa(new Pessoa { id = validacao.id });
        mensagem = Conexao.mensagem;
    }

    public List<Pessoa> PesquisarPessoaPorNome(string nome)
    {
        List<string> listaDados = ["0", nome, "", ""];
        Validacao validacao = new();
        validacao.ValidarDadosPessoa(listaDados);
        if (!string.IsNullOrEmpty(validacao.mensagem)) { mensagem = validacao.mensagem; return []; }
        List<Pessoa> listaPessoas = new PessoaDAO().PesquisarPessoaPorNome(new Pessoa { nome = nome });
        mensagem = Conexao.mensagem;
        return listaPessoas;
    }
}
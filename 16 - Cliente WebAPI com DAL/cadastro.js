const API_URL = 'http://localhost:5000/api/pessoas';

const formCadastro = document.getElementById('formCadastro');
const botaoCadastrar = document.getElementById('botaoCadastrar');
const mensagem = document.getElementById('mensagem');

formCadastro.addEventListener('submit', async (event) => {
  event.preventDefault();
  botaoCadastrar.disabled = true;
  exibirMensagem('Enviando cadastro...', '');

  const dados = {
    nome: document.getElementById('nome').value.trim(),
    rg: document.getElementById('rg').value.trim(),
    cpf: document.getElementById('cpf').value.trim()
  };

  try {
    const resposta = await fetch(API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dados)
    });
    const corpo = await lerResposta(resposta);

    if (!resposta.ok) {
      throw new Error(corpo.mensagem || 'Não foi possível cadastrar a pessoa.');
    }

    exibirMensagem(corpo.mensagem || 'Pessoa cadastrada com sucesso.', 'success');
    formCadastro.reset();
  } catch (erro) {
    exibirMensagem(mensagemDaFalha(erro), 'error');
  } finally {
    botaoCadastrar.disabled = false;
  }
});

async function lerResposta(resposta) {
  const texto = await resposta.text();
  return texto ? JSON.parse(texto) : {};
}

function mensagemDaFalha(erro) {
  return erro instanceof TypeError
    ? 'Não foi possível conectar à API. Verifique se ela está em execução.'
    : erro.message;
}

function exibirMensagem(texto, tipo) {
  mensagem.textContent = texto;
  mensagem.className = `message ${tipo}`;
}

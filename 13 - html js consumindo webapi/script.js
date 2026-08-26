const API_URL = 'http://localhost:5000/api/login';

const formLogin = document.getElementById('formLogin');
const usuario = document.getElementById('usuario');
const senha = document.getElementById('senha');
const botaoEntrar = document.getElementById('entrar');
const mensagem = document.getElementById('mensagem');

formLogin.addEventListener('submit', async function (event) {
  event.preventDefault();

  const nome = usuario.value.trim();
  const senhaDigitada = senha.value.trim();

  botaoEntrar.disabled = true;

  try {
    const resposta = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ usuario: nome, senha: senhaDigitada })
    });

    const dados = await resposta.json();
    exibirMensagem(dados.mensagem, resposta.ok ? 'success' : 'error');
  } catch (erro) {
    exibirMensagem('Não foi possível conectar à API. Verifique se ela está em execução.', 'error');
  } finally {
    botaoEntrar.disabled = false;
  }
});

function exibirMensagem(texto, tipo) {
  mensagem.textContent = texto;
  mensagem.className = 'message ' + tipo;
}

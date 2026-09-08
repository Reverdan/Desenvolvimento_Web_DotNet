const API_URL = 'http://localhost:5192/api/pessoas';

document.getElementById('botaoPesquisarId').addEventListener('click', pesquisarPorId);
document.getElementById('botaoPesquisarNome').addEventListener('click', pesquisarPorNome);
document.getElementById('botaoExcluir').addEventListener('click', excluirPessoa);
document.getElementById('formOperacoes').addEventListener('submit', salvarPessoa);
document.getElementById('resultados').addEventListener('click', usarPessoaEncontrada);

async function pesquisarPorNome() {
  const nome = valor('nomePessoa');
  if (!nome) {
    exibirMensagem('Informe um nome para pesquisar.', 'error');
    return;
  }

  await executarConsulta(`${API_URL}?nome=${encodeURIComponent(nome)}`, (corpo) => {
    const pessoas = Array.isArray(corpo.dados) ? corpo.dados : [];
    document.getElementById('resultados').innerHTML = pessoas.length
      ? pessoas.map(renderizarPessoa).join('')
      : '<p class="message">Nenhuma pessoa encontrada.</p>';
  });
}

async function pesquisarPorId() {
  const id = valor('idPessoa');
  if (!id) {
    exibirMensagem('Informe um ID para pesquisar.', 'error');
    return;
  }

  await executarConsulta(`${API_URL}/${id}`, (corpo) => {
    if (!corpo.dados) {
      exibirMensagem('Pessoa não encontrada.', 'error');
      return;
    }
    preencherCampos(corpo.dados);
  });
}

async function salvarPessoa(event) {
  event.preventDefault();
  const id = valor('idPessoa');
  if (!id) {
    exibirMensagem('Pesquise ou informe o ID antes de salvar.', 'error');
    return;
  }

  await executarConsulta(`${API_URL}/${id}`, null, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(dadosDosCampos())
  });
}

async function excluirPessoa() {
  const id = valor('idPessoa');
  if (!id) {
    exibirMensagem('Informe o ID da pessoa que deseja excluir.', 'error');
    return;
  }
  if (!window.confirm(`Excluir a pessoa de ID ${id}?`)) return;
  await executarConsulta(`${API_URL}/${id}`, null, { method: 'DELETE' });
}

async function executarConsulta(url, aoSucesso, opcoes = {}) {
  const mensagem = document.getElementById('mensagem');
  exibirMensagem(mensagem, 'Processando...', '');

  try {
    const resposta = await fetch(url, opcoes);
    const corpo = await lerResposta(resposta);
    if (!resposta.ok) throw new Error(corpo.mensagem || 'A API recusou a operação.');
    if (aoSucesso) aoSucesso(corpo);
    exibirMensagem(mensagem, corpo.mensagem || 'Operação realizada com sucesso.', 'success');
  } catch (erro) {
    exibirMensagem(mensagem, mensagemDaFalha(erro), 'error');
  }
}

async function lerResposta(resposta) {
  const texto = await resposta.text();
  return texto ? JSON.parse(texto) : {};
}

function renderizarPessoa(pessoa) {
  const pessoaCodificada = encodeURIComponent(JSON.stringify(pessoa));
  return `<article class="person-row"><div><strong>${escaparHtml(pessoa.nome)}</strong><span>ID ${pessoa.id} · RG ${escaparHtml(pessoa.rg || '-')} · CPF ${escaparHtml(pessoa.cpf || '-')}</span></div><button type="button" class="usar-pessoa" data-pessoa="${pessoaCodificada}">Usar</button></article>`;
}

function usarPessoaEncontrada(event) {
  const botao = event.target.closest('.usar-pessoa');
  if (!botao) return;
  preencherCampos(JSON.parse(decodeURIComponent(botao.dataset.pessoa)));
  exibirMensagem('Pessoa carregada nos campos.', 'success');
}

function preencherCampos(pessoa) {
  document.getElementById('idPessoa').value = pessoa.id ?? '';
  document.getElementById('nomePessoa').value = pessoa.nome ?? '';
  document.getElementById('rgPessoa').value = pessoa.rg ?? '';
  document.getElementById('cpfPessoa').value = pessoa.cpf ?? '';
}

function dadosDosCampos() {
  return {
    nome: valor('nomePessoa'),
    rg: valor('rgPessoa'),
    cpf: valor('cpfPessoa')
  };
}

function valor(id) {
  return document.getElementById(id).value.trim();
}

function escaparHtml(texto) {
  return String(texto).replace(/[&<>'"]/g, (caractere) => ({
    '&': '&amp;', '<': '&lt;', '>': '&gt;', "'": '&#39;', '"': '&quot;'
  }[caractere]));
}

function mensagemDaFalha(erro) {
  return erro instanceof TypeError
    ? 'Não foi possível conectar à API. Verifique se ela está em execução.'
    : erro.message;
}

function exibirMensagem(elementoOuTexto, textoOuTipo, tipo) {
  const elemento = typeof elementoOuTexto === 'string'
    ? document.getElementById('mensagem')
    : elementoOuTexto;
  const texto = typeof elementoOuTexto === 'string' ? elementoOuTexto : textoOuTipo;
  const classe = typeof elementoOuTexto === 'string' ? textoOuTipo : tipo;
  elemento.textContent = texto;
  elemento.className = `message ${classe}`;
}

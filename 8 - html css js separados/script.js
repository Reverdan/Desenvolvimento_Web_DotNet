const formLogin = document.getElementById('formLogin');
const usuario = document.getElementById('usuario');
const senha = document.getElementById('senha');

formLogin.addEventListener('submit', function (event) {
  event.preventDefault();

  const nome = usuario.value.trim();
  const senhaDigitada = senha.value.trim();

  if (nome === '' || senhaDigitada === '') {
    alert('Preencha todos os campos antes de entrar.');
    return;
  }

  if (nome === 'admin' && senhaDigitada === '1234') {
    alert('Login realizado com sucesso!');
  } else {
    alert('Nome de usuário ou senha inválidos.');
  }
});

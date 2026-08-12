# README - HTML, CSS e JavaScript separados

Esta pasta apresenta a mesma lógica do exercício anterior, porém com uma organização mais profissional: o HTML, o CSS e o JavaScript foram separados em arquivos diferentes.

## O que esta página apresenta

Nesta versão:
- o arquivo `index.html` guarda a estrutura da página;
- o arquivo `styles.css` guarda todos os estilos visuais;
- o arquivo `script.js` guarda a lógica de validação do formulário.

O formulário continua com a mesma funcionalidade do exercício 7:
- o nome deve ser `admin`;
- a senha deve ser `1234`;
- se os campos estiverem vazios, mostra uma mensagem;
- se os dados forem corretos, mostra sucesso;
- se forem incorretos, mostra erro.

## Estrutura dos arquivos

### 1) `index.html`
- contém a estrutura da tela;
- faz referência ao CSS com a tag `link`;
- faz referência ao JavaScript com a tag `script`.

### 2) `styles.css`
- define o layout da página;
- estiliza o formulário, os campos e o botão;
- controla cores, espaçamento, bordas, sombra e foco.

### 3) `script.js`
- obtém os elementos do formulário;
- captura os valores digitados;
- valida se o usuário e a senha estão corretos;
- mostra mensagens com `alert()`.

## Como os arquivos se conectam

No `index.html`, temos:

```html
<link rel="stylesheet" href="styles.css">
```

Essa linha conecta o HTML ao arquivo de estilos.

Também temos:

```html
<script src="script.js"></script>
```

Essa linha conecta o HTML ao arquivo JavaScript.

Assim, a página fica organizada e cada parte da aplicação fica em seu lugar.

## Código do HTML

```html
<!DOCTYPE html>
<html lang="pt-BR">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Login com CSS e JS separados</title>
  <link rel="stylesheet" href="styles.css">
</head>
<body>
  <div class="login-box">
    <h1>Login</h1>

    <form id="formLogin">
      <label for="usuario">Nome de usuário</label>
      <input type="text" id="usuario" name="usuario" placeholder="Digite seu nome">

      <label for="senha">Senha</label>
      <input type="password" id="senha" name="senha" placeholder="Digite sua senha">

      <button type="submit" id="entrar">Entrar</button>
    </form>
  </div>

  <script src="script.js"></script>
</body>
</html>
```

### O que é novo aqui

- `link rel="stylesheet" href="styles.css"` conecta o arquivo CSS;
- `script src="script.js"` conecta o arquivo JavaScript;
- o HTML fica responsável só pela estrutura, e não pelo estilo ou pela lógica.

## Código do CSS

```css
* {
  box-sizing: border-box;
}

body {
  margin: 0;
  font-family: Arial, sans-serif;
  background: linear-gradient(135deg, #eef5ff, #dfeeff);
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
}

.login-box {
  width: 360px;
  background-color: #ffffff;
  padding: 30px 25px;
  border-radius: 12px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.12);
}

h1 {
  margin-top: 0;
  margin-bottom: 22px;
  text-align: center;
  color: #1e2d3d;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: bold;
  color: #334155;
}

input {
  width: 100%;
  padding: 12px 10px;
  margin-bottom: 18px;
  border: 1px solid #c7d2e0;
  border-radius: 8px;
  outline: none;
}

input:focus {
  border-color: #4a90e2;
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.15);
}

button {
  width: 100%;
  padding: 12px;
  border: none;
  border-radius: 8px;
  background-color: #4a90e2;
  color: #ffffff;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

button:hover {
  background-color: #3f81d4;
}
```

### Explicação do CSS

- `body` centraliza a página na tela;
- `.login-box` cria o bloco do formulário;
- `h1` centraliza o título;
- `label` deixa o texto do nome e da senha visíveis;
- `input` define a aparência dos campos;
- `input:focus` mostra uma borda azul quando o usuário clica no campo;
- `button` estiliza o botão de entrar;
- `button:hover` muda a cor quando o mouse passa por cima.

## Código do JavaScript

```js
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
```

## Explicação linha a linha do JavaScript

### `const formLogin = document.getElementById('formLogin');`

Busca o formulário no HTML pelo `id` `formLogin`.

### `const usuario = document.getElementById('usuario');`

Busca o campo de nome de usuário.

### `const senha = document.getElementById('senha');`

Busca o campo de senha.

### `formLogin.addEventListener('submit', function (event) { ... })`

O código fica escutando o evento `submit`, que acontece quando o formulário é enviado.

### `event.preventDefault();`

Impede o envio padrão do formulário, evitando que a página recarregue.

### `const nome = usuario.value.trim();`

Pega o valor do input de usuário e remove espaços extras com `.trim()`.

### `const senhaDigitada = senha.value.trim();`

Pega o valor do campo de senha e remove espaços extras.

### `if (nome === '' || senhaDigitada === '')`

Verifica se algum campo ficou vazio.

- `nome === ''` significa que o nome está vazio;
- `senhaDigitada === ''` significa que a senha está vazia;
- `||` significa "ou".

Se algum estiver vazio, aparece um alerta.

### `alert('Preencha todos os campos antes de entrar.');`

Mostra uma mensagem para o usuário.

### `return;`

Para a execução da função para não continuar a validação.

### `if (nome === 'admin' && senhaDigitada === '1234')`

Valida se os dados informados estão corretos.

- `nome === 'admin'` verifica o usuário
- `senhaDigitada === '1234'` verifica a senha
- `&&` significa "e"

Só passa se os dois forem verdadeiros.

### `alert('Login realizado com sucesso!');`

Se os dados estiverem certos, aparece uma mensagem de sucesso.

### `else`

Se a condição anterior for falsa, executa o bloco do `else`.

### `alert('Nome de usuário ou senha inválidos.');`

Mostra uma mensagem de erro para o usuário.

## Vantagens de separar os arquivos

- HTML ficou responsável pela estrutura.
- CSS ficou responsável pela aparência.
- JavaScript ficou responsável pela lógica.
- O código fica mais organizado.
- É mais fácil reutilizar partes do projeto.
- A manutenção do projeto fica mais simples.

## Resumo rápido

Este exercício mostra o padrão mais usado em desenvolvimento web:

- HTML para estrutura
- CSS para visual
- JavaScript para comportamento

Essa separação deixa o projeto muito mais organizado e profissional.

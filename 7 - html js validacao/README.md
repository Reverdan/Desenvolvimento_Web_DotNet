# README - Login com validação em JavaScript

Esta pasta mostra uma tela de login com CSS e JavaScript no mesmo arquivo HTML. A principal diferença aqui é que o JavaScript valida os campos antes de permitir o acesso.

## O que esta página apresenta

Nesta versão, o usuário digita nome e senha. Ao clicar em "Entrar", o código verifica:
- se os campos estão vazios;
- se o nome é `admin`;
- se a senha é `1234`.

Se a validação for correta, aparece uma mensagem de sucesso. Caso contrário, aparece uma mensagem de erro.

## Estrutura do arquivo

### `index.html`
- contém toda a estrutura da página;
- inclui o CSS dentro da tag `style`;
- inclui o JavaScript dentro da tag `script` no final do `body`.

## JavaScript em detalhes

A parte principal está no script abaixo:

```html
<script>
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
</script>
```

## Explicação linha a linha do JavaScript

### `const formLogin = document.getElementById('formLogin');`

Essa linha busca no HTML o elemento cujo `id` é `formLogin`.

```js
const formLogin = document.getElementById('formLogin');
```

- `const` cria uma variável constante;
- `document` representa o documento HTML carregado no navegador;
- `getElementById` procura um elemento pelo seu id;
- `'formLogin'` é o nome do id definido no formulário.

Essa variável guarda o formulário inteiro para que possamos controlar o que acontece quando ele for enviado.

### `const usuario = document.getElementById('usuario');`

Essa linha guarda o campo de texto do nome do usuário:

```js
const usuario = document.getElementById('usuario');
```

Ela busca o input com o id `usuario`, que está no HTML.

### `const senha = document.getElementById('senha');`

Essa linha guarda o campo de senha:

```js
const senha = document.getElementById('senha');
```

Ela busca o input com id `senha` para poder ler o valor digitado.

### `formLogin.addEventListener('submit', function (event) { ... });`

Essa linha registra um evento no formulário:

```js
formLogin.addEventListener('submit', function (event) {
```

- `addEventListener` conecta uma função a um evento do navegador;
- `'submit'` é o evento que acontece quando o formulário é enviado;
- `function (event)` define a função que será executada quando esse evento acontecer;
- `event` representa o objeto do evento, contendo informações do envio.

Em outras palavras: quando o usuário clicar em "Entrar", o JavaScript executa a função.

### `event.preventDefault();`

```js
event.preventDefault();
```

Essa linha impede que o formulário seja enviado de forma padrão para a página, o que faria a página recarregar.

Sem essa linha, o navegador tentaria enviar os dados e provavelmente sairia da página. Ao usar `preventDefault()`, o código controla o comportamento manualmente.

### `const nome = usuario.value.trim();`

```js
const nome = usuario.value.trim();
```

Essa linha pega o valor digitado no campo do nome e remove espaços extras antes e depois.

- `usuario.value` pega o texto escrito no input;
- `.trim()` remove espaços em branco do começo e do fim;

Exemplo:
- se o usuário digitar " admin ", o valor vira "admin".

### `const senhaDigitada = senha.value.trim();`

```js
const senhaDigitada = senha.value.trim();
```

Essa linha faz o mesmo com a senha:
- pega o valor do input de senha;
- remove espaços extras;
- guarda em `senhaDigitada`.

### `if (nome === '' || senhaDigitada === '')`

```js
if (nome === '' || senhaDigitada === '') {
```

Esse é o primeiro teste de validação.

- `nome === ''` verifica se o campo de nome está vazio;
- `senhaDigitada === ''` verifica se o campo de senha está vazio;
- `||` significa "ou";

Se qualquer um dos campos estiver vazio, a condição entra no bloco.

### `alert('Preencha todos os campos antes de entrar.');`

```js
alert('Preencha todos os campos antes de entrar.');
```

A função `alert()` mostra uma janela de mensagem para o usuário.

Essa mensagem aparece quando o usuário tenta entrar sem preencher os campos corretamente.

### `return;`

```js
return;
```

A palavra `return` encerra a execução da função.

Isso é importante porque, se o campo estiver vazio, não precisa continuar validando a senha e o nome. O código para ali.

### `if (nome === 'admin' && senhaDigitada === '1234')`

```js
if (nome === 'admin' && senhaDigitada === '1234') {
```

Aqui começa a validação correta do login.

- `nome === 'admin'` verifica se o usuário digitou exatamente `admin`;
- `senhaDigitada === '1234'` verifica se a senha digitada é exatamente `1234`;
- `&&` significa "e";

Ou seja, só entra no bloco se os dois estiverem corretos ao mesmo tempo.

### `alert('Login realizado com sucesso!');`

```js
alert('Login realizado com sucesso!');
```

Se os dados estiverem certos, o navegador mostra uma mensagem avisando que o login foi bem-sucedido.

### `else`

```js
} else {
```

A palavra `else` significa "se não".

Ou seja: se a condição anterior não for verdadeira, o código executa o bloco do `else`.

### `alert('Nome de usuário ou senha inválidos.');`

```js
alert('Nome de usuário ou senha inválidos.');
```

Essa mensagem aparece quando o nome ou a senha estão errados.

## Diferença entre `==` e `===` no JavaScript

Uma dúvida muito comum em JavaScript é: por que usar `===` em vez de `==`?

A resposta é simples: `==` compara valores, mas pode converter tipos antes de comparar. Já `===` compara valor e tipo ao mesmo tempo.

### 1) Operador `==`

```js
console.log(5 == '5');
```

Esse código retorna `true`.

Por quê?

Porque o JavaScript converte a string `'5'` para número e compara:

```js
5 == 5
```

Resultado: `true`.

Esse comportamento é chamado de coerção de tipos. Ele pode causar bugs, porque o JavaScript tenta "adivinhar" o que você quer comparar.

### 2) Operador `===`

```js
console.log(5 === '5');
```

Esse código retorna `false`.

Por quê?

Porque agora o JavaScript compara:
- número `5`
- com string `'5'`

Como os tipos são diferentes, a comparação é falsa.

### Exemplo prático com login

No nosso código, usamos:

```js
if (nome === 'admin' && senhaDigitada === '1234') {
```

Isso é importante porque:
- `nome` é uma string;
- `'admin'` também é uma string;
- `senhaDigitada` é uma string;
- `'1234'` também é uma string.

Então a comparação correta é `===`.

Se usássemos `==`, o código ainda poderia funcionar em alguns casos, mas isso seria menos seguro e menos preciso.

### Exemplos de comparação

```js
console.log(1 == '1');    // true
console.log(1 === '1');   // false

console.log(true == 1);   // true
console.log(true === 1);  // false

console.log(null == undefined); // true
console.log(null === undefined); // false
```

Esses exemplos mostram que `==` pode gerar comparações estranhas, porque ele faz conversão automática.

### Regra prática

Para evitar erros, a recomendação é:

- use `===` para comparar valores e tipos;
- use `!==` para verificar se algo é diferente em valor e tipo.

Exemplo:

```js
if (senhaDigitada !== '1234') {
  alert('Senha incorreta');
}
```

### No contexto do nosso exercício

Aqui a validação correta é:

```js
if (nome === 'admin' && senhaDigitada === '1234') {
  alert('Login realizado com sucesso!');
}
```

Isso garante que o usuário digitou exatamente o valor esperado, sem conversões automáticas que podem mascarar erros.

## O que aprendemos no exercício

Neste exemplo, vimos como:
- buscar elementos do HTML com `document.getElementById`;
- capturar o valor digitado pelo usuário com `.value`;
- limpar espaços extras com `.trim()`;
- impedir o envio do formulário com `preventDefault()`;
- verificar condições com `if` e `else`;
- exibir mensagens para o usuário com `alert()`;
- comparar valores com `===` para evitar erros causados por coerção de tipos.

Esses conceitos são a base da interação com páginas web usando JavaScript.

## Resumo rápido

A lógica de validação é simples:

```js
if (nome === 'admin' && senhaDigitada === '1234') {
  alert('Login realizado com sucesso!');
} else {
  alert('Nome de usuário ou senha inválidos.');
}
```

E a regra importante é:

- `==` compara valores, podendo converter tipos
- `===` compara valor e tipo ao mesmo tempo

Em validação de login, o mais seguro é usar `===`.

Esse tipo de verificação é muito usado em sistemas de login e em formulários que precisam validar dados antes de prosseguir.

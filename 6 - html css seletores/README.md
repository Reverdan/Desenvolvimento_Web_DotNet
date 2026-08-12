# README - Seletores CSS

Esta pasta mostra como aplicar estilos de forma mais específica usando seletores CSS. Em vez de estilizar tudo da mesma maneira, o CSS passa a identificar elementos por nome, classe, atributo e estado.

## O que esta página apresenta

Nesta versão, além de separar o HTML do CSS em arquivos diferentes, usamos vários tipos de seletores para deixar a página mais organizada e visualmente melhor estruturada.

## Estrutura dos arquivos

### 1) `index.html`
- Contém a estrutura da tela de login.
- Usa classes como `login-card`, `campo` e `btn` para organizar os elementos.
- Aplica o link para o arquivo CSS externo.

### 2) `styles.css`
- Guarda todos os estilos da página.
- Usa seletores mais específicos para controlar cada parte da tela.

## O que é novo em relação aos exercícios anteriores

### 1) Seletor de elemento
```css
body {
  margin: 0;
  font-family: Arial, sans-serif;
}
```
Este seletor aplica o estilo para a tag `body`, ou seja, para a página inteira.

### 2) Seletor de classe
```css
.login-card {
  width: 360px;
  background-color: #ffffff;
}
```
A classe `.login-card` seleciona apenas os elementos que possuem essa classe. Isso permite aplicar estilos a um bloco específico sem afetar outros elementos.

### 3) Seletor descendente
```css
.campo label {
  display: block;
  font-weight: bold;
}
```
Aqui, o estilo é aplicado aos elementos `label` que estão dentro de um elemento com a classe `.campo`.

### 4) Seletor de atributo
```css
input[type="password"] {
  letter-spacing: 0.08em;
}
```
Este seletor pega somente os campos do tipo `password`. É muito útil quando queremos estilos diferentes para diferentes tipos de input.

### 5) Pseudo-classe `:focus`
```css
.campo input:focus {
  border-color: #4a90e2;
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.18);
}
```
Quando o usuário clica no campo, ele entra em foco e recebe um destaque visual. Isso melhora a experiência do usuário.

### 6) Pseudo-classe `:hover`
```css
.btn:hover {
  background-color: #3d7dd9;
  transform: translateY(-1px);
}
```
Quando o mouse passa sobre o botão, o estilo muda. Isso cria uma interação visual mais dinâmica.

## Explicação linha a linha do CSS novo

### `body`
```css
body {
  margin: 0;
```
Remove a margem padrão do navegador para que o conteúdo fique alinhado corretamente.

```css
  font-family: Arial, sans-serif;
```
Define a fonte da página.

```css
  background: linear-gradient(135deg, #eef4ff, #dfefff);
```
Cria um fundo com gradiente de azul claro, deixando a página mais bonita.

```css
  display: flex;
```
Transforma o `body` em um container flexível.

```css
  justify-content: center;
```
Centraliza o conteúdo horizontalmente.

```css
  align-items: center;
```
Centraliza o conteúdo verticalmente.

```css
  min-height: 100vh;
```
Garante que a página ocupe pelo menos a altura total da tela do navegador.

### `.login-card`
```css
.login-card {
```
Seleciona o elemento que tem a classe `login-card`.

```css
  width: 360px;
```
Define a largura do cartão da página.

```css
  background-color: #ffffff;
```
Fixa a cor de fundo do bloco em branco.

```css
  padding: 30px 25px;
```
Adiciona espaço interno ao redor do conteúdo.

```css
  border-radius: 12px;
```
Arredonda os cantos do bloco.

```css
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.12);
```
Cria uma sombra suave para dar profundidade ao painel.

### `.campo`
```css
.campo {
  margin-bottom: 18px;
}
```
Cria espaço entre os campos do formulário.

### `.campo label`
```css
.campo label {
  display: block;
```
Faz o `label` ocupar uma linha inteira, ficando acima do input.

```css
  margin-bottom: 8px;
```
Cria espaço entre o texto do label e o campo de entrada.

```css
  font-weight: bold;
```
Deixa o texto mais forte visualmente.

```css
  color: #3b4a5a;
```
Define a cor do texto do label.

### `.campo input`
```css
.campo input {
  width: 100%;
```
Faz o campo ocupar toda a largura disponível do container.

```css
  padding: 12px 10px;
```
Adiciona espaço interno ao campo.

```css
  border: 1px solid #cdd9e5;
```
Define borda fina e clara para o input.

```css
  border-radius: 8px;
```
Arredonda as bordas do input.

```css
  box-sizing: border-box;
```
Garante que o `padding` e a `border` não aumentem a largura total do campo.

```css
  transition: border-color 0.2s ease, box-shadow 0.2s ease;
```
Cria uma transição suave ao mudar a borda e a sombra.

### `input[type="password"]`
```css
input[type="password"] {
  letter-spacing: 0.08em;
}
```
Aplica uma separação maior entre os caracteres da senha, deixando a visualização mais agradável.

### `.campo input:focus`
```css
.campo input:focus {
  border-color: #4a90e2;
```
Quando o campo recebe foco, a borda muda para azul.

```css
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.18);
```
Cria um brilho ao redor do campo para indicar que ele está selecionado.

```css
  outline: none;
```
Remove o contorno padrão do navegador para deixar o estilo personalizado.

### `.btn`
```css
.btn {
  width: 100%;
```
O botão ocupa a largura total da área disponível.

```css
  padding: 12px;
```
Define o espaço interno do botão.

```css
  border: none;
```
Remove a borda padrão do botão.

```css
  border-radius: 8px;
```
Arredonda os cantos do botão.

```css
  background-color: #4a90e2;
```
Define a cor azul do botão.

```css
  color: #ffffff;
```
Define a cor do texto em branco.

```css
  font-weight: bold;
```
Deixa o texto mais forte.

```css
  cursor: pointer;
```
Muda o cursor para uma mão ao passar o mouse.

```css
  transition: background-color 0.2s ease, transform 0.2s ease;
```
Cria uma animação suave de mudança de cor e movimento.

### `.btn:hover`
```css
.btn:hover {
  background-color: #3d7dd9;
```
Quando o mouse passa sobre o botão, a cor escurece levemente.

```css
  transform: translateY(-1px);
```
Move o botão um pouco para cima, criando um efeito de elevação.

### `.btn:active`
```css
.btn:active {
  transform: translateY(0);
}
```
Quando o botão é clicado, ele volta ao lugar para dar sensação de clique.

## Resumo rápido

Neste exercício, aprendemos a:
- usar seletores de elemento, classe e atributo;
- aplicar estilos específicos a campos e botões;
- usar pseudo-classes como `:focus` e `:hover`;
- deixar a página mais interativa e profissional.

Esses conceitos são fundamentais para qualquer projeto web, porque permitem controlar exatamente como cada elemento deve aparecer e reagir.

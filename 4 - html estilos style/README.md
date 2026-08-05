# README - Estilos com tag style

Esta pasta contém uma página de login com estilos definidos dentro da tag `style` no cabeçalho do arquivo [index.html](index.html). Essa versão mostra como organizar o CSS em um único lugar, separando a estrutura do HTML da aparência visual.

## O que esta página apresenta

A página demonstra a abordagem mais comum de estilização em páginas web: usar a tag `style` dentro do elemento `head` para definir regras de CSS. Essa forma é mais organizada do que aplicar estilos diretamente em cada tag e serve como base para aprender CSS de forma mais estruturada.

## O que é CSS

CSS é a linguagem usada para controlar a aparência de uma página HTML. Enquanto o HTML define a estrutura, ou seja, o que existe na página — títulos, parágrafos, formulários, imagens e links — o CSS define como essa estrutura deve aparecer visualmente. Com ele, é possível alterar cores, fontes, espaços, alinhamento, tamanhos, bordas, sombras e muito mais.

A grande vantagem do CSS é que ele permite estilizar vários elementos de forma consistente sem precisar repetir o mesmo código várias vezes. Imagine, por exemplo, que você tenha vários botões iguais em uma página. Em vez de colocar a mesma formatação manualmente em cada botão, você pode criar uma regra CSS para todos eles de uma vez. Isso torna o código mais limpo, mais organizado e mais fácil de manter.

Essa ideia é muito importante no desenvolvimento web, porque evita duplicação de estilos. Quando componentes iguais recebem a mesma aparência por meio de uma única regra, o projeto fica mais profissional e mais simples de ajustar no futuro. Se um dia você quiser mudar a cor de todos os botões, basta alterar uma regra no CSS em vez de editar cada botão manualmente.

Em resumo, o CSS serve para deixar a página mais bonita, mais organizada e mais fácil de manter, além de garantir que elementos iguais tenham uma aparência uniforme.

## Conceitos principais

### 1) `style`
- A tag `style` permite escrever regras de CSS diretamente no documento HTML.
- Ela fica normalmente dentro do elemento `head`.
- O navegador interpreta o conteúdo dela como código CSS.

### 2) Seletor
- Um seletor é a parte do CSS que diz ao navegador qual elemento da página deve receber aquele estilo.
- Pense nele como um "endereço" usado para encontrar o elemento certo.
- Exemplos comuns: `body`, `form`, `h1`, `input` e `button`.
- O seletor `body` altera a página inteira; o seletor `form` mexe apenas no formulário; o seletor `button` muda somente os botões.
- Quando escrevemos algo como `input, button`, estamos dizendo que a mesma regra vale para os dois elementos ao mesmo tempo.
- Isso ajuda a evitar repetir código e deixa a pagina mais organizada.
- Exemplo: `h1 { color: blue; }` faz com que todos os títulos `h1` fiquem azuis.
- Outro exemplo: `button { cursor: pointer; }` faz com que o cursor mude quando passar sobre o botão.

### 3) Regras CSS
- Dentro da tag `style`, cada regra possui um seletor e uma lista de propriedades.
- Exemplo: `body { font-family: Arial, sans-serif; }`.
- Isso deixa o código mais limpo e fácil de manter.

### 4) Organização do CSS
- Ao colocar os estilos em um único bloco, o HTML fica mais simples.
- O código fica mais legível porque a estrutura e a aparência ficam separadas em partes diferentes.
- Essa abordagem é a base para trabalhar com arquivos CSS externos depois.

## Exemplo do código usado

```html
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      background-color: #f2f4f8;
      margin: 0;
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
    }

    form {
      background-color: white;
      padding: 30px;
      border-radius: 10px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
      width: 320px;
    }

    button {
      background-color: #4a90e2;
      color: white;
      border: none;
      border-radius: 5px;
      cursor: pointer;
    }
  </style>
</head>
```

## Resumo rápido

Nesta página foram apresentados os conceitos principais da estilização com tag `style`:
- uso da tag `style` no `head`;
- definição de seletores e regras CSS;
- organização dos estilos em um único lugar;
- separação entre estrutura HTML e aparência visual.

Essa forma de CSS é importante porque facilita a manutenção do código e prepara o aluno para aprender estilos mais avançados.

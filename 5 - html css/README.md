# README - HTML com CSS em arquivo separado

Esta pasta contém uma página de login semelhante à do exercício anterior, mas com os estilos organizados em um arquivo separado chamado [styles.css](styles.css).

## O que esta página apresenta

Nesta versão, o HTML ficou responsável apenas pela estrutura da página, enquanto o CSS foi colocado em um arquivo próprio. Essa abordagem é mais organizada e mais comum em projetos reais.

## Estrutura dos arquivos

### 1) `index.html`
- Contém a estrutura do formulário e os elementos visuais da página.
- Faz a ligação com o arquivo CSS por meio da tag `link`.

### 2) `styles.css`
- Armazena todas as regras de estilo da página.
- Centraliza cores, fontes, margens, espaçamentos e posicionamento.

## Vantagens de usar um arquivo separado para CSS

- O código fica mais organizado.
- A estrutura HTML fica mais limpa.
- É mais fácil manter e atualizar os estilos.
- Vários arquivos HTML podem compartilhar o mesmo CSS.

## Exemplo de ligação entre os arquivos

No arquivo `index.html`:

```html
<link rel="stylesheet" href="styles.css">
```

Assim, o navegador entende que os estilos estão no arquivo `styles.css`.

## Resumo rápido

Nesta página foi apresentado o conceito de separar o HTML do CSS em arquivos diferentes:
- HTML para a estrutura;
- CSS para a aparência;
- uso de um arquivo externo para organizar melhor o projeto.

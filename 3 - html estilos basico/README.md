# README - Estilos básicos em HTML

Esta pasta contém uma página de login com aparência mais agradável no arquivo [index.html](index.html). Nessa versão, os estilos foram aplicados diretamente nas tags, usando o atributo `style` dentro de cada elemento HTML.

## O que esta página apresenta

A página demonstra a forma mais simples de estilizar um documento HTML: a aplicação de propriedades de CSS diretamente na própria tag. Essa abordagem é conhecida como estilo inline e é muito útil para pequenos ajustes e exemplos didáticos.

## Estrutura da página

### 1) `style` inline
- Atributo usado para aplicar formatação diretamente em uma tag.
- Exemplo: `<body style="..."></body>`.
- Esse tipo de estilo é colocado dentro da própria tag, no mesmo elemento que recebe a formatação.
- Ele é útil para pequenas alterações rápidas e para aprender o conceito básico de estilização.

### 2) `font-family`
- Define a família de fontes usada no elemento.
- Exemplo: `font-family: Arial, sans-serif;`.
- O navegador tenta usar a primeira fonte da lista; se não encontrar, usa uma fonte semelhante.
- Esse atributo é importante para dar uma aparência mais organizada e legível ao conteúdo.

### 3) `background-color`
- Define a cor de fundo do elemento.
- Exemplo: `background-color: #f2f4f8;`.
- Pode ser aplicado ao corpo da página (`body`) ou a elementos específicos, como um formulário.
- Ajuda a criar contraste e melhorar a visualização da interface.

### 4) `margin`
- Define a margem externa de um elemento.
- Exemplo: `margin: 0;`.
- Margens servem para afastar elementos uns dos outros e controlar o espaço ao redor.
- Quando usada com valor `0`, elimina o espaçamento padrão que o navegador pode aplicar.

### 5) `padding`
- Define o espaço interno entre o conteúdo e a borda do elemento.
- Exemplo: `padding: 30px;`.
- Esse atributo deixa o conteúdo mais confortável visualmente e ajuda a criar área interna no bloco.

### 6) `display: flex`
- Define que o elemento será tratado como um contêiner flexível.
- Esse recurso permite alinhar elementos de maneira mais organizada e simples.
- No exemplo, ele foi usado no `body` para centralizar o formulário na tela.

### 7) `justify-content: center`
- Alinha os itens horizontalmente no centro do contêiner flexível.
- Em outras palavras, ajuda a centralizar o conteúdo na página no eixo horizontal.
- É muito usado para posicionar caixas ou blocos no meio da tela.

### 8) `align-items: center`
- Alinha os itens verticalmente no centro do contêiner flexível.
- Em conjunto com `justify-content`, permite centralizar o conteúdo tanto na horizontal quanto na vertical.
- Esse efeito é muito comum em páginas com formulários e caixas de login.

### 9) `min-height: 100vh`
- Define a altura mínima do elemento como 100% da altura da janela do navegador.
- O valor `vh` significa viewport height, ou seja, altura da área visível da tela.
- Isso faz com que o conteúdo ocupe toda a altura da tela, facilitando o centralamento visual.

### 10) `border-radius`
- Arredonda as bordas de um elemento.
- Exemplo: `border-radius: 10px;`.
- Esse recurso dá um aspecto mais moderno e agradável ao formulário.

### 11) `box-shadow`
- Adiciona uma sombra ao redor do elemento.
- Exemplo: `box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);`.
- A sombra ajuda a destacar o bloco e dar profundidade visual à interface.
- O termo `rgba` significa `red, green, blue, alpha`.
- Ele é usado para definir cores com transparência.
- Os três primeiros valores representam as cores básicas: vermelho, verde e azul.
- O quarto valor, chamado `alpha`, controla a transparência da cor, indo de `0` a `1`.
- Quanto mais próximo de `0`, mais transparente a cor fica; quanto mais próximo de `1`, mais opaca ela fica.
- No exemplo `rgba(0, 0, 0, 0.15)`, a cor é preta, com uma transparência leve, o que cria uma sombra suave.

### 12) `width`
- Define a largura do elemento.
- Exemplo: `width: 320px;`.
- No exemplo, a largura foi usada para determinar o tamanho do formulário.

### 13) `text-align: center`
- Centraliza o texto dentro do elemento.
- Exemplo: `text-align: center;`.
- Foi aplicado ao título principal para deixá-lo mais bonito visualmente.

### 14) `color`
- Define a cor do texto.
- Exemplo: `color: #333;`.
- Essa propriedade ajuda a melhorar o contraste entre o texto e o fundo.

### 15) `box-sizing: border-box`
- Faz com que a largura e a altura do elemento incluam o padding e a borda.
- Isso evita que o elemento fique maior do que o esperado quando recebe preenchimento interno.
- É muito útil em campos de formulário e botões.

### 16) `cursor: pointer`
- Define o formato do cursor quando o mouse passa por cima do botão.
- O valor `pointer` faz com que o cursor fique com a aparência de clique.
- Isso dá feedback visual ao usuário.

## Exemplo do código usado

```html
<body style="font-family: Arial, sans-serif; background-color: #f2f4f8; margin: 0; padding: 0; display: flex; justify-content: center; align-items: center; min-height: 100vh;">
  <form style="background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15); width: 320px;">
    <h1 style="text-align: center; margin-bottom: 20px; color: #333;">Login</h1>
    <hr>
    <b>Nome de usuário:</b><br>
    <input type="text" name="nome" placeholder="Digite seu nome" style="width: 100%; padding: 10px; margin-top: 8px; box-sizing: border-box;"><br><br>
    <b>Senha:</b><br>
    <input type="password" name="senha" placeholder="Digite sua senha" style="width: 100%; padding: 10px; margin-top: 8px; box-sizing: border-box;"><br><br>
    <button type="button" style="width: 100%; padding: 10px; margin-top: 8px; box-sizing: border-box; background-color: #4a90e2; color: white; border: none; border-radius: 5px; cursor: pointer;">Entrar</button>
  </form>
</body>
```

## Resumo rápido

Nesta página foram abordados conceitos básicos de estilo inline em HTML:
- uso do atributo `style` diretamente nas tags;
- definição de cores, fontes, margens e espaçamentos;
- centralização de elementos com Flexbox;
- arredondamento, sombras e aparência visual de formulários;
- ajustes em campos de formulário e botões.

Esses recursos são fundamentais para entender como a aparência de uma página pode ser melhorada sem usar um arquivo CSS separado.

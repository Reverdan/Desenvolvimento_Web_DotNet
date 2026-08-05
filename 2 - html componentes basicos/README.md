# README - Página de login em HTML puro

Esta pasta contém uma página de login simples no arquivo [index.html](index.html), criada apenas com HTML puro, sem CSS e sem JavaScript.

## O que esta página apresenta

A página demonstra alguns elementos HTML que não foram explicados no README anterior, especialmente componentes de formulário.

## Estrutura da página

### 1) `<meta charset="UTF-8">`
- Define a codificação de caracteres da página como UTF-8.
- Isso permite que acentos e caracteres especiais sejam exibidos corretamente.
- A palavra `charset` significa conjunto de caracteres, ou seja, o conjunto de símbolos que o navegador pode interpretar.
- O valor `UTF-8` é um padrão moderno e amplamente usado na web, porque suporta praticamente todos os caracteres do mundo, incluindo letras com acento, ç, cedilha, símbolos e emojis.
- Sem essa tag, pode acontecer de palavras como "coração", "série" ou "Olá" aparecerem com sinais estranhos ou incorretos no navegador.
- Exemplos de uso:
  - páginas com textos em português, espanhol, francês ou outros idiomas;
  - sites que utilizam caracteres especiais ou símbolos específicos;
  - aplicações que exibem emojis ou letras de alfabetos diferentes.
- Possibilidades adicionais:
  - ela é essencial em páginas com formulários, textos explicativos e conteúdos diversos;
  - ela ajuda a manter a consistência da exibição em diferentes navegadores e sistemas operacionais.
- Em resumo, essa tag garante que o conteúdo da página seja mostrado corretamente, sem problemas de codificação.

### 2) `<meta name="viewport" content="width=device-width, initial-scale=1.0">`
- Ajusta a exibição da página para diferentes tamanhos de tela.
- É muito importante para que a página fique responsiva em celulares e tablets.
- O termo `viewport` significa a área visível da página no navegador.
- O valor `width=device-width` faz a página usar a largura do dispositivo do usuário.
- O valor `initial-scale=1.0` define o zoom inicial como 100%, evitando que a página apareça muito ampliada ou reduzida no primeiro carregamento.
- Sem essa meta tag, alguns sites podem parecer desalinhados ou difíceis de ler em telas menores.
- Exemplos de uso:
  - páginas de login, cadastro e e-commerce, que precisam funcionar bem em celular;
  - sites com imagens, formulários e menus que devem se adaptar automaticamente;
  - páginas com conteúdo longo, onde a leitura deve ser confortável em qualquer dispositivo.
- Possibilidades adicionais:
  - `maximum-scale=1.0` limita o zoom do usuário;
  - `user-scalable=no` impede que o usuário faça zoom;
  - `shrink-to-fit=no` ajuda a controlar o comportamento em alguns dispositivos antigos.
- Em resumo, essa tag melhora muito a experiência do usuário ao garantir que a página seja exibida corretamente em diferentes telas.

### 3) `<h1>`
- Define um título principal na página.
- Neste exemplo, ele serve para mostrar o texto "Login" na parte superior.
- O elemento `h1` representa o título mais importante de uma página.
- Ele é usado para indicar o tema principal do conteúdo e ajuda na organização visual e semântica da página.
- Em HTML existem seis níveis de títulos: `h1` até `h6`, sendo `h1` o mais importante e `h6` o menos importante.
- Exemplos de uso:
  - título de uma página inicial;
  - nome de uma seção principal;
  - cabeçalho de uma tela de login, cadastro ou painel.
- Possibilidades adicionais:
  - pode ser usado junto com outros elementos para criar uma hierarquia de conteúdo;
  - é importante para acessibilidade, pois leitores de tela usam essa estrutura para navegar melhor.
- Em resumo, o `h1` ajuda a identificar rapidamente o assunto principal da página e melhora a experiência de leitura.

### 4) `<hr>`
- Cria uma linha horizontal para separar conteúdos visualmente.
- É uma tag simples, sem fechamento, usada para dividir seções.
- O nome `hr` vem de "horizontal rule", que em inglês significa "regra horizontal".
- Sua função é representar uma divisão visual entre blocos de conteúdo, como tópicos, seções ou partes de uma página.
- Pode ser usada para organizar melhor a leitura e dar uma estrutura mais clara ao layout.

### 5) `<form>`
- Representa um formulário de entrada de dados.
- É o container onde ficam campos como nome, senha e botões.
- Em páginas web, formulários são usados para coletar informações do usuário.

## Campos de entrada

### 6) `<input>`
- Cria campos para o usuário digitar informações.
- Pode ter diferentes tipos, como:
  - `type="text"`: campo de texto comum.
  - `type="password"`: campo para senha, ocultando o conteúdo digitado.
- Além de `type`, o elemento `input` pode receber outros atributos importantes, como:
  - `value`: define um valor inicial já preenchido no campo;
  - `required`: torna o campo obrigatório;
  - `disabled`: desabilita o campo para impedir digitação;
  - `maxlength`: limita a quantidade máxima de caracteres;
  - `readonly`: permite apenas leitura, sem edição.
- Exemplos de uso:
  - `<input type="text" value="Maria">` para já mostrar um valor inicial;
  - `<input type="text" required>` para exigir preenchimento;
  - `<input type="text" maxlength="10">` para limitar caracteres;
  - `<input type="password" readonly>` para exibir uma senha fixa sem permitir alteração.
- Em resumo, o `input` é um componente muito flexível, pois pode ser adaptado para diferentes funções com base em seus atributos.

### 7) `name="..."`
- Define o nome do campo dentro do formulário.
- Esse valor pode ser usado depois para identificar os dados enviados.

### 8) `placeholder="..."`
- Exibe um texto temporário dentro do campo antes do usuário digitar.
- Ajuda a orientar o usuário sobre o que preencher.

## Botão

### 9) `<button>`
- Cria um botão clicável.
- Neste exemplo, o botão é exibido com o texto "Entrar".
- O atributo `type="button"` indica que ele não envia o formulário por enquanto.
- Além de `type`, o elemento `button` também pode receber outros atributos importantes, como:
  - `value`: define um valor associado ao botão;
  - `disabled`: desativa o botão para impedir cliques;
  - `name`: identifica o botão dentro de um formulário;
  - `autofocus`: faz com que o botão receba o foco automaticamente ao carregar a página.
- Exemplos de uso:
  - `<button type="submit">Enviar</button>` para enviar os dados do formulário;
  - `<button type="button" disabled>Não disponível</button>` para mostrar um botão inativo;
  - `<button name="login">Entrar</button>` para identificar o botão em um formulário;
  - `<button autofocus>Começar</button>` para focar o botão ao abrir a página.
- Em resumo, o `button` é um componente interativo essencial para ações como enviar, cancelar, salvar e navegar.

## Formatação inline

### 10) `<b>`
- Deixa o texto em negrito.
- Foi usado para destacar os rótulos dos campos.

### 11) `<br>`
- Quebra a linha no HTML.
- Serve para organizar os elementos visualmente, sem precisar de CSS.

## Exemplo do código usado

```html
<form>
  <b>Nome de usuário:</b><br>
  <input type="text" name="nome" placeholder="Digite seu nome"><br><br>
  <b>Senha:</b><br>
  <input type="password" name="senha" placeholder="Digite sua senha"><br><br>
  <button type="button">Entrar</button>
</form>
```

## Resumo rápido

Nesta página foram abordados elementos importantes para interação com o usuário:
- metadados com `<meta>`
- títulos com `<h1>`
- linhas com `<hr>`
- formulários com `<form>`
- campos de entrada com `<input>`
- botões com `<button>`
- marcação visual com `<b>` e `<br>`

Esses recursos são fundamentais para criar telas simples de cadastro, login e envio de dados.

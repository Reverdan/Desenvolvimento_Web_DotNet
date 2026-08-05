# README - Estrutura inicial de uma página HTML pura

Este projeto contém uma página HTML mínima no arquivo [index.html](index.html).

## Código da página

```html
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <title>Minha Página</title>
</head>
<body>
</body>
</html>
```

## Explicação didática de cada item

### 1) `<!DOCTYPE html>`
- **O que é:** uma declaração que informa ao navegador que o documento usa **HTML5**.
- **Por que é importante:** evita que o navegador entre em “modo de compatibilidade” com versões antigas, garantindo comportamento moderno.
- **Observação:** não é uma tag de abertura/fechamento; é uma declaração especial.

### 2) `<html lang="pt-BR"> ... </html>`
- **O que é:** o elemento raiz da página, que envolve todo o conteúdo HTML.
- **Atributo `lang="pt-BR"`:** define o idioma principal como português do Brasil.
- **Por que é importante:** ajuda leitores de tela, mecanismos de busca e ferramentas de tradução a entenderem o idioma correto.

### 3) `<head> ... </head>`
- **O que é:** a área de metadados e configurações do documento.
- **O que vai aqui normalmente:** título da aba, metatags, links para CSS, scripts de configuração etc.
- **Importante:** o conteúdo do `head` geralmente não aparece diretamente na área visual da página.

### 4) `<title>Minha Página</title>`
- **O que é:** define o título do documento.
- **Onde aparece:** na aba do navegador, no histórico e em resultados de busca.
- **Boas práticas:** usar um título claro e descritivo para facilitar navegação e SEO básico.

### 5) `<body> ... </body>`
- **O que é:** área onde fica o conteúdo visível da página.
- **Neste arquivo:** está vazio, pronto para receber textos, imagens, links, listas, tabelas e outros elementos.

## Leitura linha por linha

1. `<!DOCTYPE html>`: ativa padrão HTML5.
2. `<html lang="pt-BR">`: começa o documento e define idioma.
3. `<head>`: inicia metadados.
4. `<title>Minha Página</title>`: título da aba.
5. `</head>`: encerra metadados.
6. `<body>`: inicia conteúdo visual.
7. `</body>`: encerra conteúdo visual.
8. `</html>`: encerra o documento.

## Resumo rápido

Essa estrutura é o esqueleto inicial de uma página HTML pura:
- declaração do tipo de documento (`DOCTYPE`),
- elemento raiz (`html`),
- área de configuração (`head`),
- título (`title`),
- área de conteúdo (`body`).

Com isso, você já tem uma base correta para começar a construir qualquer página web.

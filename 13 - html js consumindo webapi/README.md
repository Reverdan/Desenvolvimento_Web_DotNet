# README - Cliente HTML/CSS/JS consumindo a Web API

Este exercício cria um cliente separado, em HTML, CSS e JavaScript tradicionais (sem framework), que consome o endpoint JSON criado no exercício 11 (`11 - ASP dotNet WebAPI`).

As credenciais didáticas continuam sendo:

- usuário: `admin`;
- senha: `1234`.

> A validação de verdade continua acontecendo no servidor (exercício 11). Este cliente apenas envia os dados e exibe a resposta.

## Por que um projeto separado?

No exercício 11, a Web API não possui tela própria — ela só responde a requisições HTTP. Este exercício mostra o outro lado dessa arquitetura: um cliente independente (poderia ser uma página web, um app mobile ou outro serviço) que chama o endpoint pela rede.

```text
index.html + script.js                  LoginController (exercício 11)
(porta do cliente, ex.: 5500)   ---->    http://localhost:5000/api/login
        |                                        |
        | fetch() com JSON no body               | valida no servidor
        v                                        v
   exibe mensagem de sucesso/erro        devolve JSON + status HTTP
```

Como o cliente roda em uma origem (porta/protocolo) diferente da API, é necessário **CORS** (*Cross-Origin Resource Sharing*) habilitado no servidor. Sem isso, o navegador bloqueia a resposta por segurança.

## Pré-requisito: rodar a Web API do exercício 11

Abra um terminal na pasta do exercício 11 e execute:

```powershell
Set-Location "..\11 -  ASP dotNet WebAPI"
dotnet run --urls http://localhost:5000
```

Se preferir a porta padrão do Kestrel, basta rodar `dotnet run` sem `--urls`; neste exercício a URL fixa considerada no cliente é `http://localhost:5000`.

O `Program.cs` desse projeto já foi ajustado com uma política de CORS liberando qualquer origem, método e cabeçalho, apenas para fins didáticos:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();
```

> Em uma aplicação real, `AllowAnyOrigin` deve ser trocado por uma lista específica de origens confiáveis (`WithOrigins("https://meusite.com")`), para não expor a API a qualquer site.

## Como criar a estrutura manualmente

```powershell
Set-Location "Sua Pasta"
New-Item -ItemType Directory -Path "13 - html js consumindo webapi" -Force
Set-Location "13 - html js consumindo webapi"
New-Item -ItemType File -Path index.html, styles.css, script.js
code index.html styles.css script.js
```

## Componentes do projeto

### `index.html`

Formulário simples com campos de usuário e senha, um botão de envio e um parágrafo (`#mensagem`) para exibir o retorno da API:

```html
<form id="formLogin">
  <input type="text" id="usuario" name="usuario">
  <input type="password" id="senha" name="senha">
  <button type="submit" id="entrar">Entrar</button>
</form>

<p id="mensagem" class="message"></p>
```

### `script.js`

Usa `fetch` com `async/await` para enviar os dados em JSON e ler a resposta:

```javascript
const API_URL = 'http://localhost:5000/api/login';

formLogin.addEventListener('submit', async function (event) {
  event.preventDefault();

  const resposta = await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ usuario: nome, senha: senhaDigitada })
  });

  const dados = await resposta.json();
  exibirMensagem(dados.mensagem, resposta.ok ? 'success' : 'error');
});
```

Pontos importantes:

- `resposta.ok` é `true` para status `200-299` e `false` para `400` ou `401`, o que permite decidir a classe CSS (`success`/`error`) sem precisar checar o código numérico;
- o corpo é convertido para JSON com `JSON.stringify` antes de enviar, e a resposta é lida com `resposta.json()`;
- um bloco `try/catch` trata falhas de rede, como a API estar desligada;
- o botão é desabilitado durante a chamada (`botaoEntrar.disabled = true`) para evitar múltiplos cliques.

### `styles.css`

Reaproveita o mesmo visual dos exercícios anteriores (`login-box`, `.message`, `.success`, `.error`), para manter a identidade visual entre os exemplos.

## Como testar

1. Rode a API do exercício 11 na porta `5000` (veja o pré-requisito acima).
2. Abra o arquivo `index.html` deste exercício diretamente no navegador, ou sirva-o com um servidor estático (por exemplo, a extensão *Live Server* do VS Code).
3. Preencha os campos e clique em **Entrar**:
   - campos vazios devem retornar `400 Bad Request` e a mensagem correspondente;
   - `admin` / senha errada deve retornar `401 Unauthorized`;
   - `admin` / `1234` deve retornar `200 OK` e a mensagem de sucesso.

## Diferença em relação aos exercícios anteriores

| Exercício | Onde roda a validação | Quem serve a tela | Comunicação |
| --- | --- | --- | --- |
| 7 e 8 | No navegador, com JavaScript | O próprio HTML estático | Nenhuma chamada de rede |
| 9, 10 | No servidor (Razor Pages / MVC) | O próprio servidor ASP.NET Core | Formulário HTML tradicional (`POST` com recarregamento de página) |
| 11 | No servidor (Web API) | Nenhuma; só expõe JSON | Chamada HTTP feita por outro cliente |
| 12 | No servidor (componente Blazor) | O próprio servidor ASP.NET Core | Interatividade via SignalR, sem `fetch` manual |
| 13 | No servidor (reutiliza a API do exercício 11) | Um cliente HTML/CSS/JS separado | `fetch` assíncrono consumindo JSON pela rede, com CORS |

## Dica prática

Para esse exemplo didático, a URL da API está fixa no `script.js`. Em um projeto real, essa URL normalmente viria de uma configuração de ambiente (ex.: variável de build, arquivo `.env` equivalente para front-end), para facilitar a troca entre desenvolvimento, homologação e produção.

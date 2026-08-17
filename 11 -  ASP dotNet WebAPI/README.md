# README - ASP.NET Core Web API com validação server-side

Este exercício repete a regra de login dos exercícios 9 e 10, mas utiliza ASP.NET Core Web API. Em vez de renderizar uma página HTML, a aplicação expõe um endpoint HTTP que recebe JSON e devolve JSON.

As credenciais didáticas continuam sendo:

- usuário: `admin`;
- senha: `1234`.

> Esta é uma demonstração de endpoint. Em uma aplicação real, credenciais não devem ficar escritas no código, e o login deve usar banco de dados, hash de senha e um mecanismo de autenticação como ASP.NET Core Identity ou JWT.

## O que é uma Web API?

Uma Web API é uma aplicação que disponibiliza dados e operações por meio de requisições HTTP. Ela normalmente não possui telas, CSS ou Views. Um cliente separado, como uma aplicação HTML/JavaScript, mobile ou desktop, chama os endpoints e interpreta as respostas.

Neste exercício:

```text
Cliente HTTP
    |
    | POST /api/login
    | JSON: { "usuario": "admin", "senha": "1234" }
    v
LoginController
    |
    | valida os dados no servidor
    v
Resposta JSON + status HTTP
```

O navegador pode ser um cliente, mas não existe uma página de login dentro deste projeto. A interface seria criada em outro projeto ou no exercício 8, consumindo este endpoint.

## Como criar manualmente

### 1. Verificar o SDK

Abra o PowerShell e confirme o SDK .NET instalado:

```powershell
dotnet --version
dotnet --info
```

Este projeto usa `net8.0`. O comando `dotnet --version` deve mostrar uma versão instalada do SDK 8.

### 2. Criar a pasta e o template

A partir da pasta que contém os exercícios:

```powershell
Set-Location "Sua Pasta"
New-Item -ItemType Directory -Path "11 -  ASP dotNet WebAPI" -Force
Set-Location "11 -  ASP dotNet WebAPI"
dotnet new webapi --name LoginApi --output . --no-https --no-openapi --force
```

O comando `dotnet new webapi` usa o template oficial **ASP.NET Core Web API**. Os parâmetros significam:

- `--name LoginApi`: define o nome do projeto e do arquivo `.csproj`;
- `--output .`: cria o projeto na pasta atual;
- `--no-https`: simplifica o exemplo, sem certificado HTTPS local;
- `--no-openapi`: não adiciona Swagger/OpenAPI ao projeto mínimo;
- `--force`: permite gerar arquivos em uma pasta que já possui conteúdo.

Para consultar opções disponíveis:

```powershell
dotnet new list
dotnet new webapi --help
```

### 3. Criar a estrutura da API

Crie as pastas e arquivos:

```powershell
New-Item -ItemType Directory -Path Models -Force
New-Item -ItemType Directory -Path Controllers -Force
code Program.cs
code Models\LoginRequest.cs
code Models\LoginResponse.cs
code Controllers\LoginController.cs
```

A estrutura final será:

```text
11 -  ASP dotNet WebAPI/
|-- Controllers/
|   `-- LoginController.cs
|-- Models/
|   |-- LoginRequest.cs
|   `-- LoginResponse.cs
|-- Program.cs
|-- LoginApi.csproj
`-- README.md
```

### 4. Restaurar, compilar e executar

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5191
```

A API ficará disponível em `http://localhost:5191`. Para parar o servidor, pressione `Ctrl+C`.

## Componentes do projeto

### `Program.cs`

Registra o suporte a Controllers com `AddControllers()` e publica as rotas dos Controllers com `MapControllers()`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

`AddControllers()` prepara o MVC usado para APIs, incluindo o model binding e a serialização JSON. `MapControllers()` faz o roteamento baseado nos atributos do Controller.

### `LoginRequest.cs`

É o DTO (*Data Transfer Object*) de entrada. Ele representa o JSON enviado pelo cliente:

```json
{
  "usuario": "admin",
  "senha": "1234"
}
```

As propriedades são anuláveis porque o cliente pode não enviar o corpo ou pode enviar campos vazios. O Controller trata esses casos antes de comparar as credenciais.

### `LoginResponse.cs`

É o DTO de saída. A API devolve uma mensagem e um booleano indicando o resultado:

```json
{
  "mensagem": "Login realizado com sucesso!",
  "sucesso": true
}
```

O ASP.NET Core converte automaticamente o objeto C# para JSON.

### `LoginController.cs`

O Controller contém o endpoint:

```csharp
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
```

`[Route("api/[controller]")]` usa o nome da classe sem o sufixo `Controller`. Portanto, `LoginController` resulta na rota base `/api/login`.

O método:

```csharp
[HttpPost]
public IActionResult Login(LoginRequest? request)
```

recebe requisições `POST /api/login`. O parâmetro `LoginRequest` é preenchido pelo model binding a partir do JSON do corpo da requisição.

A ação retorna status HTTP diferentes:

- `BadRequest(...)`: `400` quando algum campo está vazio;
- `Ok(...)`: `200` quando as credenciais estão corretas;
- `Unauthorized(...)`: `401` quando as credenciais não correspondem.

O status HTTP comunica o resultado para o cliente de forma padronizada. A mensagem JSON fornece uma explicação que a interface pode exibir.

## Entendendo as anotações do Controller

Em C#, os elementos escritos entre colchetes, como `[ApiController]`, são **atributos**. Eles adicionam metadados à classe ou ao método. Na Web API, esses atributos orientam o roteamento, o model binding, a validação e a forma como o Controller responde às requisições.

### `[ApiController]`

```csharp
[ApiController]
public class LoginController : ControllerBase
```

`[ApiController]` marca a classe como um Controller de API. Entre seus comportamentos mais importantes estão:

- ajuda o ASP.NET Core a interpretar parâmetros recebidos no corpo, na rota ou na query string;
- habilita inferências de binding, como entender que um objeto complexo deve vir do corpo JSON;
- transforma erros de validação do ModelState em respostas HTTP `400` automaticamente, quando há regras de validação aplicáveis;
- padroniza comportamentos comuns de APIs.

Este atributo não cria a rota sozinho e não autentica o usuário. Ele define convenções próprias para uma API.

### `[Route("api/[controller]")]`

```csharp
[Route("api/[controller]")]
public class LoginController : ControllerBase
```

`[Route]` define o modelo de URL usado pelo Controller. O trecho `[controller]` é um token substituído pelo nome da classe sem o sufixo `Controller`:

```text
LoginController -> Login
api/[controller] -> api/login
```

Por isso, a rota base deste exercício é `/api/login`. Essa é uma rota por atributo, também chamada de attribute routing, pois está escrita diretamente na classe e nos métodos.

### `[HttpPost]`

```csharp
[HttpPost]
public IActionResult Login(LoginRequest? request)
```

`[HttpPost]` restringe o método para requisições HTTP `POST`. Como está sem um caminho adicional, ele usa a rota da classe e atende:

```text
POST /api/login
```

O corpo JSON é convertido em `LoginRequest` pelo model binding antes da execução do método. O parâmetro `request` pode ser `null` se o cliente não enviar um corpo válido; por isso o código usa `request?.Usuario` e `request?.Senha`.

### Como os atributos trabalham juntos

Os três atributos formam uma sequência de decisão:

```text
[ApiController]
  |
  | define comportamentos de API e binding
  v
[Route("api/[controller]")]
  |
  | define a rota base /api/login
  v
[HttpPost]
  |
  | aceita somente POST
  v
Login(LoginRequest? request)
  |
  | recebe o JSON convertido em objeto
  v
resposta HTTP + JSON
```

Se `[HttpPost]` for removido, o método não terá uma restrição explícita de verbo. Se `[Route]` for removido, não haverá essa rota por atributo. Se `[ApiController]` for removido, o Controller ainda poderá funcionar com configuração adicional, mas perderá convenções e comportamentos automáticos importantes para APIs.

### Atributos não são regras de login

Os atributos configuram como a requisição chega ao método. Eles não comparam `admin` com `1234` e não decidem os status de sucesso ou erro. Essa regra está no corpo de `Login`:

```csharp
if (nome == "admin" && senha == "1234")
{
  return Ok(...);
}
```

Portanto, a separação é:

| Elemento | Responsabilidade |
| --- | --- |
| `[ApiController]` | Aplicar convenções de Controller de API e binding. |
| `[Route]` | Definir o caminho da URL. |
| `[HttpPost]` | Restringir a action ao verbo POST. |
| `LoginRequest` | Representar os dados recebidos. |
| Corpo de `Login` | Validar credenciais e escolher o status HTTP. |

## Testando o endpoint

### PowerShell

Com a aplicação executando em outra janela:

```powershell
$body = @{ usuario = "admin"; senha = "1234" } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5191/api/login" -Method Post -ContentType "application/json" -Body $body
```

Resposta esperada:

```text
mensagem sucesso
-------- --------
Login realizado com sucesso! True
```

Para testar credenciais inválidas:

```powershell
$body = @{ usuario = "user"; senha = "wrong" } | ConvertTo-Json
Invoke-WebRequest -Uri "http://localhost:5191/api/login" -Method Post -ContentType "application/json" -Body $body
```

Essa chamada deve retornar `401 Unauthorized`.

Para testar campos vazios:

```powershell
$body = @{ usuario = ""; senha = "" } | ConvertTo-Json
Invoke-WebRequest -Uri "http://localhost:5191/api/login" -Method Post -ContentType "application/json" -Body $body
```

Essa chamada deve retornar `400 Bad Request`.

### cURL

```bash
curl -i -X POST http://localhost:5191/api/login \
  -H "Content-Type: application/json" \
  -d '{"usuario":"admin","senha":"1234"}'
```

A opção `-i` exibe os cabeçalhos, incluindo o status HTTP.

## Diferenças entre Web API, MVC e Razor Pages

Os exercícios 9, 10 e 11 aplicam a mesma regra, mas têm responsabilidades diferentes:

| Aspecto | Razor Pages | MVC | Web API |
| --- | --- | --- | --- |
| Projeto | `dotnet new webapp` | `dotnet new mvc` | `dotnet new webapi` |
| Saída principal | HTML renderizado | HTML renderizado | JSON ou outro dado |
| Possui View | Sim, `.cshtml` | Sim, `.cshtml` | Não neste exercício |
| Entrada | Campos de um formulário HTML | Campos de um formulário HTML | Corpo JSON da requisição |
| Endpoint | Página e `OnPost` | Controller e action | Controller e action |
| Cliente típico | Navegador | Navegador | Site separado, app mobile ou outro serviço |
| Status de sucesso | Página HTTP `200` | Página HTTP `200` | `200 OK` com JSON |
| Erro de dados vazios | Mensagem na página | Mensagem na View | `400 Bad Request` |
| Credenciais incorretas | Mensagem na página | Mensagem na View | `401 Unauthorized` |
| Antiforgery | Proteção automática do formulário POST | Proteção do formulário com Tag Helper | Não usado neste endpoint JSON simples |

### Diferença para Razor Pages

No Razor Pages, `Pages/Index.cshtml` contém a tela e `Pages/Index.cshtml.cs` contém o `PageModel`, com métodos como `OnPost`. O objetivo é entregar uma página pronta para o navegador.

Na Web API, não existe `Pages`, `Views` ou HTML de login. O Controller recebe uma representação dos dados e devolve uma representação dos dados. O cliente decide como apresentar a resposta.

### Diferença para MVC

No MVC, o Controller normalmente retorna uma View com `return View(model)`. A View transforma o modelo em HTML.

Na Web API, o Controller herda de `ControllerBase` e retorna `Ok`, `BadRequest` ou `Unauthorized` com objetos que são serializados para JSON. Não há View para renderizar.

## Por que não usamos token antifalsificação aqui?

O exercício 9 usa um formulário HTML renderizado pelo servidor. Nesse cenário, o navegador envia cookies automaticamente, então o token antifalsificação ajuda a proteger requisições `POST` contra CSRF.

Este exercício recebe JSON em um endpoint de API e não usa o fluxo de formulário baseado em cookie. Por isso, o endpoint não adiciona token antifalsificação. Isso não significa que a API esteja automaticamente protegida: uma API real precisa de autenticação, autorização, HTTPS e uma estratégia adequada de tokens, como JWT ou OAuth 2.0, conforme o cenário.

Se uma API usar autenticação por cookie, ela deverá considerar proteção CSRF. Se usar `Authorization: Bearer` com tokens que não são enviados automaticamente pelo navegador, o risco e a estratégia são diferentes, mas ainda é necessário proteger credenciais, validar origem quando aplicável e usar HTTPS.

## Fluxo completo

1. O cliente monta um objeto JSON.
2. O cliente envia `POST /api/login` com `Content-Type: application/json`.
3. O model binder converte o JSON em `LoginRequest`.
4. `LoginController` remove espaços e verifica os campos.
5. O Controller compara `admin` e `1234` no servidor.
6. A API retorna status HTTP e um objeto `LoginResponse` em JSON.
7. O cliente interpreta a resposta e decide como exibir o resultado.

Não há JavaScript de validação dentro desta API. Se uma tela quiser consumir o endpoint, ela poderá fazer uma requisição `fetch`, mas a decisão de login continuará no servidor.

## Limitações didáticas

Este exemplo não cria sessão, não emite JWT, não consulta banco de dados e não armazena senhas com hash. O objetivo é demonstrar a estrutura de uma Web API, o recebimento de JSON, o model binding, os status HTTP e a validação server-side.

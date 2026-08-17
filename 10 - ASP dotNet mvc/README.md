# README - ASP.NET Core MVC com validação server-side

Este exercício reproduz o login do exercício 9 usando ASP.NET Core MVC. A regra continua sendo executada no servidor e usa as credenciais `admin` e `1234`.

## Como criar manualmente

### 1. Verificar o SDK

```powershell
dotnet --version
dotnet --info
```

Este projeto usa o .NET 8. O primeiro comando deve exibir uma versão instalada do SDK.

### 2. Criar a pasta e o template MVC

Execute os comandos a partir da pasta que contém os demais exercícios:

```powershell
Set-Location "Sua Pasta"
New-Item -ItemType Directory -Path "10 - ASP dotnet mvc" -Force
Set-Location "10 - ASP dotnet mvc"
dotnet new mvc --name LoginMvc --output . --no-https --force
```

O comando `dotnet new mvc` usa o template **ASP.NET Core MVC**. Os parâmetros significam:

- `--name LoginMvc`: define o nome do projeto e do arquivo `.csproj`;
- `--output .`: gera os arquivos na pasta atual;
- `--no-https`: simplifica o exemplo, sem configuração de HTTPS local;
- `--force`: permite gerar os arquivos em uma pasta que já contém arquivos.

Para consultar templates e opções:

```powershell
dotnet new list
dotnet new mvc --help
```

### 3. Restaurar, compilar e executar

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5190
```

Acesse `http://localhost:5190`. Para parar o servidor, pressione `Ctrl+C`.

## Estrutura MVC deste exercício

```text
10 - ASP dotnet mvc/
|-- Controllers/
|   `-- LoginController.cs
|-- Models/
|   `-- LoginViewModel.cs
|-- Views/
|   |-- _ViewImports.cshtml
|   `-- Login/
|       `-- Index.cshtml
|-- wwwroot/
|   `-- styles.css
|-- Program.cs
`-- LoginMvc.csproj
```

### `Program.cs`

Registra os serviços MVC com `AddControllersWithViews()` e configura a rota padrão:

```text
{controller=Login}/{action=Index}/{id?}
```

Isso significa que uma requisição para `/` será encaminhada para `LoginController.Index()`.

### `LoginViewModel.cs`

É o modelo usado pela View e pelo Controller. Ele transporta `Usuario` e `Senha` na requisição e também guarda a mensagem que será exibida na tela.

### `LoginController.cs`

É o centro do fluxo MVC:

- `Index()` com `[HttpGet]` exibe o formulário inicialmente;
- `Index(LoginViewModel model)` com `[HttpPost]` recebe os dados enviados;
- `[ValidateAntiForgeryToken]` protege o POST contra falsificação de requisição;
- o Controller remove espaços, verifica campos vazios, compara as credenciais e retorna a View com o resultado.

A validação é server-side porque a decisão acontece dentro da ação C# do Controller, depois que o navegador envia o formulário.

## Entendendo as anotações do Controller

Em C#, os elementos escritos entre colchetes, como `[HttpPost]`, são **atributos**. Eles adicionam metadados ao método ou à classe. O ASP.NET Core MVC lê esses metadados durante o roteamento e a execução da requisição.

### `[HttpGet]`

```csharp
[HttpGet]
public IActionResult Index()
{
	return View(new LoginViewModel());
}
```

`[HttpGet]` informa que essa action deve atender requisições HTTP `GET`. O `GET` é usado para solicitar a tela inicial sem enviar os dados do login. Quando o usuário acessa `/` ou `/Login/Index` pelo navegador, a rota encontra `LoginController.Index()` e o método retorna a View vazia.

O atributo também evita que essa mesma assinatura seja escolhida para um `POST`. Assim, a action de exibição e a action de processamento podem ter o mesmo nome, desde que tenham verbos HTTP diferentes.

### `[HttpPost]`

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Index(LoginViewModel model)
{
	// valida os dados recebidos
}
```

`[HttpPost]` informa que essa action atende requisições HTTP `POST`. O formulário da View usa `method="post"`, então o navegador envia os campos para essa segunda action `Index`.

O método recebe `LoginViewModel model`. O model binder lê os campos `Usuario` e `Senha` enviados pelo formulário e monta esse objeto antes de executar o corpo da action.

Se `[HttpPost]` fosse removido, o MVC ainda teria uma action chamada `Index`, mas a separação explícita por verbo deixaria de existir. Isso poderia causar ambiguidade com a action `GET` ou permitir que uma action fosse escolhida para um verbo que não deveria processar.

### `[ValidateAntiForgeryToken]`

```csharp
[ValidateAntiForgeryToken]
public IActionResult Index(LoginViewModel model)
```

Esse atributo exige que o `POST` contenha um token antifalsificação válido. O Tag Helper do `<form>` gera um token oculto na View, e o navegador o envia junto com `Usuario` e `Senha`. O framework verifica o token antes de executar a action.

O objetivo é reduzir o risco de CSRF: um site externo não deve conseguir enviar, silenciosamente, um formulário para a aplicação usando a sessão do usuário. Se o token estiver ausente ou inválido, a requisição normalmente termina com `400 Bad Request` e o corpo da action não é executado.

Esse atributo não verifica se `admin` e `1234` estão corretos. Ele protege a origem do formulário; a validação das credenciais continua no código da action.

### Por que os dois métodos podem se chamar `Index`?

O nome do método sozinho não identifica completamente uma action MVC. O framework considera a combinação de controller, nome da action, rota e atributos HTTP:

```text
GET  /Login/Index -> Index()                  [HttpGet]
POST /Login/Index -> Index(LoginViewModel)    [HttpPost]
```

Essa separação é chamada de **seleção de action**. O `[HttpGet]` e o `[HttpPost]` funcionam como restrições que dizem quando cada método pode ser escolhido.

### Atributo, parâmetro e regra de negócio

É importante separar as responsabilidades:

| Elemento | Responsabilidade |
| --- | --- |
| `[HttpGet]` | Selecionar a action para requisições GET. |
| `[HttpPost]` | Selecionar a action para requisições POST. |
| `[ValidateAntiForgeryToken]` | Validar a proteção antifalsificação antes da action. |
| `LoginViewModel model` | Receber os dados enviados pelo formulário. |
| Corpo da action | Aplicar a regra de negócio do login. |

### `Views/Login/Index.cshtml`

É a View MVC. Ela contém HTML e Razor, mas não contém a regra de autenticação. Os atributos que começam com `asp-` são **Tag Helpers** do ASP.NET Core MVC.

## Entendendo as tags `asp-`

As tags `asp-` não são tags HTML padrão. São instruções reconhecidas pelo Razor durante a renderização no servidor. Antes de enviar a resposta ao navegador, o ASP.NET Core processa essas instruções e gera HTML comum.

Para que isso funcione, o arquivo `Views/_ViewImports.cshtml` precisa conter:

```cshtml
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

Sem essa configuração, atributos como `asp-for` poderiam chegar ao navegador sem serem processados.

### `asp-for`: conecta o campo ao Model

Na View, usamos:

```html
<label asp-for="Usuario">Nome de usuário</label>
<input asp-for="Usuario" class="input" placeholder="Digite seu nome">
```

O valor `Usuario` deve corresponder à propriedade `Usuario` da classe `LoginViewModel`:

```csharp
public string Usuario { get; set; } = string.Empty;
```

Durante a renderização, o `asp-for` gera atributos HTML semelhantes a:

```html
<label for="Usuario">Nome de usuário</label>
<input type="text" id="Usuario" name="Usuario" class="input" placeholder="Digite seu nome">
```

O ponto mais importante é o atributo `name="Usuario"`. Quando o formulário é enviado, o navegador envia um par `Usuario=valor`. O model binder do MVC usa esse nome para preencher `LoginViewModel.Usuario` no Controller.

No campo de senha, usamos:

```html
<label asp-for="Senha">Senha</label>
<input asp-for="Senha" class="input" type="password" placeholder="Digite sua senha">
```

O resultado mantém a propriedade ligada ao Model e gera um campo com `name="Senha"` e `type="password"`. Assim, o valor enviado chega a `LoginViewModel.Senha`.

O `asp-for` também pode usar os valores do Model para preencher novamente a View após um POST. Isso é útil quando o servidor retorna a página com erros. Para senhas, o framework evita reapresentar o valor por motivos de segurança.

### `asp-controller` e `asp-action`: definem o destino do formulário

O formulário usa:

```html
<form asp-controller="Login" asp-action="Index" method="post">
```

Esses atributos indicam:

- `asp-controller="Login"`: use o `LoginController`;
- `asp-action="Index"`: envie para a action `Index`;
- `method="post"`: faça uma requisição HTTP POST, apropriada para enviar dados ao servidor.

O Tag Helper transforma essa combinação em um `action` HTML semelhante a:

```html
<form action="/Login/Index" method="post">
```

O navegador não conhece `asp-controller` nem `asp-action`; ele conhece apenas o HTML final gerado pelo servidor.

### O token antifalsificação do `form`

Como o formulário usa Tag Helpers e a action POST possui `[ValidateAntiForgeryToken]`, o ASP.NET Core adiciona automaticamente um campo oculto semelhante a:

```html
<input name="__RequestVerificationToken" type="hidden" value="...">
```

O navegador envia esse token junto com `Usuario` e `Senha`. O atributo `[ValidateAntiForgeryToken]` confere se o token é válido antes de executar a action. Essa proteção ajuda a impedir que outro site envie formulários indevidos para a aplicação usando a sessão do usuário.

### `asp-validation-for` e validação de Model

Este exercício faz a regra de login manualmente no Controller, mas MVC também oferece mensagens ligadas ao Model. Por exemplo:

```cshtml
<span asp-validation-for="Usuario"></span>
```

Esse Tag Helper exibe a mensagem de validação associada à propriedade `Usuario`, caso o Model tenha regras como `[Required]`. Ele não substitui a validação de credenciais deste exercício: verificar se o usuário é `admin` e a senha é `1234` continua sendo responsabilidade da action.

### Resumo do caminho dos dados

```text
asp-for="Usuario"
	|
	v
HTML name="Usuario"
	|
	v
POST /Login/Index
	|
	v
LoginViewModel.Usuario
	|
	v
LoginController.Index(LoginViewModel model)
```

Portanto, os atributos `asp-` não realizam a autenticação. Eles conectam a View aos dados e às rotas do MVC. A decisão de login continua sendo feita no servidor pelo `LoginController`.

### `wwwroot/styles.css`

Contém os estilos visuais do formulário, equivalentes aos usados no exercício 9.

## Fluxo completo do MVC

1. O navegador faz `GET /`.
2. A rota padrão chama `LoginController.Index()` com `[HttpGet]`.
3. O Controller cria um `LoginViewModel` vazio e retorna `Views/Login/Index.cshtml`.
4. O usuário preenche o formulário.
5. O formulário faz `POST` para `/Login/Index`.
6. O model binder converte os campos enviados em um `LoginViewModel`.
7. O método `[HttpPost] Index(LoginViewModel model)` valida os dados.
8. O Controller retorna a mesma View com a mensagem de sucesso ou erro.

Não há JavaScript responsável pela regra de login.

## Diferenças entre Razor Pages e MVC

Os exercícios 9 e 10 possuem a mesma tela, as mesmas credenciais e a mesma validação, mas organizam o código de maneiras diferentes.

| Aspecto | Exercício 9: Razor Pages | Exercício 10: MVC |
| --- | --- | --- |
| Template | `dotnet new webapp` | `dotnet new mvc` |
| Organização principal | Uma pasta `Pages` por página | Pastas separadas `Controllers`, `Models` e `Views` |
| Arquivo da tela | `Pages/Index.cshtml` | `Views/Login/Index.cshtml` |
| Código server-side | `Pages/Index.cshtml.cs` com `PageModel` | `Controllers/LoginController.cs` com `Controller` |
| Recebimento do formulário | Método `OnPost()` | Ação `[HttpPost] Index(LoginViewModel model)` |
| Navegação | Cada página possui sua rota com `@page` | Rotas relacionam controller e action |
| Modelo | Propriedades no `PageModel` | Classe explícita `LoginViewModel` |
| Configuração | `AddRazorPages()` e `MapRazorPages()` | `AddControllersWithViews()` e `MapControllerRoute()` |
| Melhor uso | Sites orientados a páginas e CRUD simples | Aplicações maiores com separação clara de responsabilidades |

### Diferença de responsabilidade

No Razor Pages, o arquivo da página e seu code-behind ficam juntos: `Index.cshtml` e `Index.cshtml.cs`. O `PageModel` controla aquela página específica.

No MVC, a View é responsável pela apresentação, o Model representa os dados e o Controller coordena a requisição. Essa separação facilita centralizar ações, reutilizar Models e organizar aplicações com muitos fluxos.

### Diferença no fluxo de requisição

No Razor Pages, `@page` torna o arquivo uma página acessível e o framework procura métodos como `OnGet` e `OnPost`.

No MVC, a URL é interpretada por uma rota formada por controller e action. Neste exercício, `/` usa `LoginController` e a action `Index`. O atributo `[HttpGet]` diferencia a exibição do formulário, enquanto `[HttpPost]` diferencia o processamento.

### Qual escolher?

Use Razor Pages quando a aplicação for naturalmente organizada por telas independentes e cada tela tiver seu próprio comportamento.

Use MVC quando a aplicação precisar de controllers explícitos, múltiplas Views por controller, Models reutilizáveis ou uma separação tradicional entre apresentação, fluxo e dados.

Para este login, as duas opções funcionam. O MVC foi usado no exercício 10 para demonstrar a separação explícita entre Controller, Model e View.

## Testes

Teste as três situações:

- `admin` e `1234`: exibe `Login realizado com sucesso!`;
- campos vazios: exibe `Preencha todos os campos antes de entrar.`;
- qualquer outra combinação: exibe `Nome de usuário ou senha inválidos.`.

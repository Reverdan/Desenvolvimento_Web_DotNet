# README - ASP.NET Core com validação server-side

## Como criar o projeto manualmente

Esta é a sequência completa de comandos para criar este exercício do zero usando o SDK .NET 8 e o template ASP.NET Core Razor Pages.

### 1. Verificar os pré-requisitos

Abra o PowerShell e confirme que o SDK está instalado:

```powershell
dotnet --version
dotnet --info
```

O comando `dotnet --version` deve mostrar uma versão do SDK, como `8.0.424`. Se o comando não existir, instale o SDK .NET 8 antes de continuar.

### 2. Criar a pasta do exercício

No terminal, navegue até a pasta onde ficam os exercícios e crie a pasta do projeto:

```powershell
Set-Location "Sua Pasta"
New-Item -ItemType Directory -Path "9 - ASP dotNet" -Force
Set-Location "9 - ASP dotNet"
```

### 3. Criar a aplicação Razor Pages

Execute o template oficial do ASP.NET Core diretamente na pasta atual:

```powershell
dotnet new webapp --name LoginServerSide --output . --no-https --force
```

#### O que significa `webapp`?

`webapp` é o nome curto do template oficial **ASP.NET Core Web App (Razor Pages)**. Um template é um modelo pronto usado pelo comando `dotnet new` para gerar a estrutura inicial de um tipo de aplicação.

Neste caso, `webapp` foi escolhido porque a tela de login é uma página HTML com um formulário e a validação precisa acontecer no servidor. Cada página possui um arquivo `.cshtml` para a marcação e, opcionalmente, um arquivo `.cshtml.cs` para o código server-side. O método `OnPost` recebe o envio do formulário.

Os parâmetros usados no comando significam:

- `--name LoginServerSide`: define o nome do projeto e do arquivo `.csproj`;
- `--output .`: cria o projeto na pasta atual;
- `--no-https`: não cria configuração de HTTPS local, simplificando o exemplo didático;
- `--force`: permite gerar os arquivos mesmo que a pasta já contenha arquivos.

Para listar todos os templates instalados no SDK, use:

```powershell
dotnet new list
```

Para ver a ajuda e todas as opções de um template específico:

```powershell
dotnet new webapp --help
```

#### Outras opções de aplicação web

O SDK oferece outros templates. A escolha depende da arquitetura desejada:

| Comando | Tecnologia | Quando usar |
| --- | --- | --- |
| `dotnet new webapp` | Razor Pages | Sites e telas organizadas por página, como este login. |
| `dotnet new mvc` | ASP.NET Core MVC | Aplicações que separam explicitamente Models, Views e Controllers. |
| `dotnet new webapi` | ASP.NET Core Web API | APIs HTTP que retornam dados, normalmente JSON, sem páginas HTML. |
| `dotnet new web` | ASP.NET Core vazio | Projetos mínimos em que a configuração e os endpoints serão montados manualmente. |
| `dotnet new blazor` | Blazor | Interfaces interativas usando componentes .NET no navegador ou no servidor. |

Exemplos de criação, sempre executados a partir da pasta que conterá o projeto:

```powershell
# Aplicação MVC
dotnet new mvc --name LoginMvc --output . --no-https

# API HTTP
dotnet new webapi --name LoginApi --output . --no-https

# Aplicação ASP.NET Core vazia
dotnet new web --name LoginMinimal --output . --no-https

# Aplicação Blazor
dotnet new blazor --name LoginBlazor --output . --no-https
```

Esses comandos são alternativas e não devem ser executados na pasta deste exercício depois que ela já foi criada, pois cada template gera uma estrutura diferente. Para este login com formulário e validação server-side por página, `webapp` é a opção mais direta. Para um frontend separado ou um aplicativo mobile consumindo dados, `webapi` seria mais apropriado.

Esse comando cria a estrutura inicial, incluindo:

- `LoginServerSide.csproj`: arquivo do projeto;
- `Program.cs`: configuração e inicialização do servidor;
- `Pages/Index.cshtml`: página inicial;
- `Pages/Index.cshtml.cs`: code-behind da página;
- `Pages/_ViewImports.cshtml`: imports e Tag Helpers;
- `wwwroot/`: arquivos estáticos, como CSS, JavaScript e imagens.

Confira os arquivos criados:

```powershell
Get-ChildItem -Recurse -File
```

### 4. Restaurar as dependências

Restaure os pacotes necessários para o projeto:

```powershell
dotnet restore
```

### 5. Configurar o servidor Razor Pages

Abra o arquivo `Program.cs` no editor:

```powershell
code Program.cs
```

Mantenha a configuração do template com os serviços Razor Pages e os arquivos estáticos:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
```

### 6. Criar a tela de login

Abra a página e o code-behind:

```powershell
code Pages\Index.cshtml
code Pages\Index.cshtml.cs
```

Em `Pages/Index.cshtml`, crie o formulário com `method="post"`, os campos `Usuario` e `Senha` usando `asp-for` e o botão de envio. A página deve exibir `Model.Mensagem` após o processamento do servidor.

Em `Pages/Index.cshtml.cs`, crie as propriedades vinculadas ao formulário e o método `OnPost`. Nesse método:

1. remova espaços extras com `Trim()`;
2. verifique se os campos estão vazios;
3. compare o usuário `admin` e a senha `1234`;
4. defina uma mensagem de sucesso ou erro para a página.

### 7. Habilitar os Tag Helpers

Confira se `Pages/_ViewImports.cshtml` contém o comando abaixo. Ele habilita `asp-for` e o token antifalsificação automático do formulário:

```powershell
code Pages\_ViewImports.cshtml
```

```cshtml
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

## Como funciona o token antifalsificação

O token antifalsificação é uma proteção contra **CSRF** (*Cross-Site Request Forgery*, ou falsificação de requisição entre sites). O objetivo é garantir que uma requisição `POST` foi iniciada pela página legítima da aplicação, e não por um formulário escondido em outro site.

### 1. O servidor gera os tokens

Quando o navegador acessa a página com `GET`, o Tag Helper do formulário gera automaticamente um campo oculto semelhante a este:

```html
<input name="__RequestVerificationToken" type="hidden" value="TOKEN_DA_REQUISICAO">
```

O ASP.NET Core também envia um cookie de antifalsificação ao navegador. Na prática, a proteção usa dois valores relacionados:

- um token no cookie;
- um token no campo oculto do formulário.

O valor real é longo e protegido. Ele não deve ser fixado manualmente no código nem compartilhado entre usuários.

### 2. O navegador envia o formulário

Quando o usuário clica em **Entrar**, o navegador envia os campos do login e o campo oculto no mesmo `POST`:

```text
Usuario=admin
Senha=1234
__RequestVerificationToken=TOKEN_DA_REQUISICAO
```

O cookie correspondente também acompanha a requisição automaticamente.

### 3. O ASP.NET Core valida a combinação

O Razor Pages valida automaticamente requisições de alteração, como `POST`, antes de executar o método `OnPost`. Ele compara o token recebido no formulário com o token armazenado no cookie e verifica se eles são válidos para a aplicação.

Neste projeto, o método `OnPost` contém a regra do login, mas só é alcançado depois dessa proteção inicial. O token não decide se o usuário e a senha estão corretos; ele apenas confirma a origem legítima da requisição.

### 4. O que acontece sem o token

Se um cliente enviar um `POST` diretamente, sem primeiro carregar a página e obter o cookie e o campo oculto, a validação antifalsificação falha. Normalmente o ASP.NET Core responde com:

```text
HTTP 400 - Bad Request
```

Nesse caso, o método `OnPost` nem é executado. Isso é diferente de credenciais inválidas: com um token válido e usuário ou senha incorretos, o `OnPost` é executado e a aplicação exibe a mensagem de login inválido.

### 5. De onde a proteção vem neste projeto

A proteção depende de três partes:

1. o formulário `method="post"` em `Pages/Index.cshtml`;
2. os Tag Helpers habilitados em `Pages/_ViewImports.cshtml`;
3. a validação antifalsificação padrão do Razor Pages para métodos HTTP inseguros, como `POST`.

O código da View não precisa escrever manualmente o `<input>` oculto. O formulário é suficiente para que o Tag Helper o gere:

```html
<form method="post">
	<input asp-for="Usuario">
	<input asp-for="Senha" type="password">
	<button type="submit">Entrar</button>
</form>
```

### Token antifalsificação não é autenticação

É importante separar os conceitos:

| Recurso | Finalidade |
| --- | --- |
| Token antifalsificação | Verifica se o `POST` veio de um formulário legítimo da aplicação. |
| Usuário e senha | Verificam se as credenciais correspondem ao login esperado. |
| Cookie de sessão ou autenticação | Mantém o estado de um usuário que já foi autenticado. |

Neste exercício, o token protege o envio do formulário, enquanto `OnPost` compara `admin` com `1234`. O token não substitui uma autenticação real, um banco de dados ou o uso de ASP.NET Core Identity em uma aplicação de produção.

### 8. Adicionar o CSS

Crie o arquivo de estilos na pasta pública do ASP.NET Core:

```powershell
New-Item -ItemType Directory -Path wwwroot -Force
code wwwroot\styles.css
```

Na página Razor, referencie o arquivo assim:

```html
<link rel="stylesheet" href="~/styles.css">
```

O CSS pode reproduzir a aparência do exercício 8: formulário centralizado, campos com foco, botão azul e mensagens de sucesso ou erro.

### 9. Compilar o projeto

Antes de executar, compile e confirme que não existem erros:

```powershell
dotnet build
```

Para uma compilação sem restaurar novamente os pacotes:

```powershell
dotnet build --no-restore
```

### 10. Executar a aplicação

Inicie o servidor:

```powershell
dotnet run
```

Para escolher uma porta fixa:

```powershell
dotnet run --urls http://localhost:5189
```

Abra `http://localhost:5189` no navegador. Para parar o servidor, pressione `Ctrl+C` no terminal.

### 11. Testar a validação server-side

Teste as três situações:

- `admin` e `1234`: login realizado com sucesso;
- campos vazios: mensagem para preencher todos os campos;
- qualquer outra combinação: usuário ou senha inválidos.

O navegador envia uma requisição `POST`; a decisão não é feita por JavaScript. O método `OnPost` executa a validação no servidor e renderiza a página novamente com o resultado.

Esta pasta apresenta a mesma tela de login do exercício 8, mas a validação é executada no servidor usando ASP.NET Core Razor Pages.

## O que esta página apresenta

O formulário continua usando as credenciais:

- nome de usuário: `admin`;
- senha: `1234`.

A diferença é que o navegador apenas envia os dados ao servidor. A validação acontece no método `OnPost`, no arquivo `Pages/Index.cshtml.cs`, depois do envio do formulário.

## Estrutura dos arquivos

### `Pages/Index.cshtml`

Contém a estrutura da página e o formulário HTML. Os atributos `asp-for` conectam os campos às propriedades do code-behind.

### `Pages/Index.cshtml.cs`

Recebe o envio do formulário no método `OnPost`, remove espaços extras, verifica campos vazios e compara usuário e senha no servidor. A mensagem é devolvida na própria página.

## Entendendo as anotações do código

Em C#, os elementos escritos entre colchetes, como `[BindProperty]`, são chamados de **atributos**. Em materiais de desenvolvimento web, também é comum chamá-los de anotações. Eles adicionam metadados a uma classe, propriedade ou método. O ASP.NET Core lê esses metadados e usa as informações para decidir como tratar a requisição.

### `[BindProperty]`

No `IndexModel`, a anotação aparece antes das propriedades que recebem os dados do formulário:

```csharp
[BindProperty]
public string Usuario { get; set; } = string.Empty;

[BindProperty]
public string Senha { get; set; } = string.Empty;
```

Ela informa ao Razor Pages que os valores enviados pelo navegador devem ser associados a essas propriedades do `PageModel`. Quando o formulário envia campos com os nomes `Usuario` e `Senha`, o model binding procura propriedades com esses mesmos nomes e preenche o objeto antes de executar `OnPost`.

O caminho dos dados fica assim:

```text
input name="Usuario"
	|
	v
requisição POST
	|
	v
[BindProperty] Usuario
	|
	v
OnPost()
```

Sem `[BindProperty]`, o método `OnPost` ainda poderia ser executado, mas as propriedades do `IndexModel` não seriam automaticamente preenchidas pelo formulário. Outra alternativa seria receber os valores diretamente como parâmetros do método, por exemplo `OnPost(string usuario, string senha)`, mas `[BindProperty]` mantém os dados organizados no modelo da página.

Essa anotação apenas transporta os dados. Ela não verifica se o usuário é `admin`, não compara a senha `1234` e não substitui o token antifalsificação. A regra de login continua no corpo de `OnPost`.

### O método `OnPost`

O nome `OnPost` segue a convenção do Razor Pages. O framework associa esse método a requisições HTTP `POST` da página. Portanto, quando o formulário usa `method="post"`, o Razor Pages procura e executa `OnPost`.

```csharp
public void OnPost()
{
    // Os valores já foram associados às propriedades pelo model binding.
}
```

`OnPost` não está entre colchetes porque seu comportamento é definido pela convenção de nomes do Razor Pages. Em outras palavras, no Razor Pages o método é reconhecido pelo prefixo `On` mais o verbo HTTP: `OnGet`, `OnPost`, `OnPut` e assim por diante.

### Diferença entre atributo e convenção

`[BindProperty]` é uma instrução explícita colocada no código. Já `OnPost` é uma convenção: o framework identifica o método pelo nome. Essa é uma diferença importante em relação ao MVC e à Web API, que usam atributos como `[HttpPost]` para selecionar ações.

### `Pages/_ViewImports.cshtml`

Habilita os Tag Helpers do ASP.NET Core, usados pelos atributos `asp-for`, e permite que o formulário inclua automaticamente o token antifalsificação.

### `Program.cs` e `LoginServerSide.csproj`

Configuram o servidor Razor Pages e permitem compilar e executar a aplicação com o SDK .NET 8.

### `wwwroot/styles.css`

Mantém a aparência do exercício 8: formulário centralizado, campos com foco, botão azul e mensagens de sucesso ou erro.

## Fluxo da validação

1. O usuário preenche o formulário.
2. O botão envia uma requisição `POST` para o servidor.
3. O método `OnPost` lê as propriedades `Usuario` e `Senha`.
4. O servidor valida os dados e renderiza novamente a página com a mensagem correspondente.

Não há arquivo JavaScript nesta versão, porque a regra de login é executada no servidor.

## Como executar

Abra um terminal na pasta e execute:

```powershell
dotnet run --project LoginServerSide.csproj
```

Acesse o endereço exibido no terminal e teste:

- `admin` e `1234`: login realizado com sucesso;
- campos vazios: mensagem para preencher todos os campos;
- qualquer outra combinação: usuário ou senha inválidos.

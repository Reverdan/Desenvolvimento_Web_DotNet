# README - ASP.NET Core Blazor com validação server-side

Este exercício repete a regra de login dos exercícios anteriores, mas usando o Blazor. A ideia é manter a mesma lógica de autenticação em um componente interativo, com validação no servidor e controle do estado da interface.

As credenciais didáticas continuam sendo:

- usuário: `admin`
- senha: `1234`

> Em uma aplicação real, credenciais não devem ficar hardcoded no código. O ideal é usar banco de dados, criptografia de senha e autenticação de usuários.

## O que é Blazor?

Blazor é a tecnologia do ASP.NET Core para criar interfaces interativas usando C# no navegador.

Existem duas formas principais:

- Blazor Web App: combina componentes e arquitetura moderna;
- Blazor Server / WebAssembly: dependendo do tipo de execução desejado.

Neste exercício, a ideia é apresentar a mesma tela de login em um componente da interface, com lógica server-side e atualização da UI com base no estado da página.

## Como criar manualmente

### 1. Verificar o SDK

Abra o PowerShell e confira se o .NET 8 está instalado:

```powershell
dotnet --version
dotnet --info
```

### 2. Criar a pasta e o template Blazor

A partir da pasta que contém os demais exercícios:

```powershell
Set-Location "Sua Pasta"
New-Item -ItemType Directory -Path "12 - ASP dotNet Blazor" -Force
Set-Location "12 - ASP dotNet Blazor"
dotnet new blazor --name LoginBlazor --output . --no-https --force
```

O comando `dotnet new blazor` cria uma aplicação Blazor com estrutura inicial de projeto e páginas/componentes.

Parâmetros:

- `--name LoginBlazor`: define o nome do projeto;
- `--output .`: cria o projeto na pasta atual;
- `--no-https`: simplifica o projeto sem HTTPS local;
- `--force`: permite recriar os arquivos em uma pasta que já exista.

Para consultar opções do template:

```powershell
dotnet new list
dotnet new blazor --help
```

### 3. Restaurar, compilar e executar

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5192
```

A aplicação ficará disponível em `http://localhost:5192`. Para encerrar, pressione `Ctrl+C`.

## Estrutura do projeto

```text
12 - ASP dotNet Blazor/
|-- Components/
|   |-- Pages/
|   |   `-- Home.razor
|   `-- Layout/
|-- Program.cs
|-- appsettings.json
|-- LoginBlazor.csproj
`-- wwwroot/
```

A estrutura exata pode variar conforme a versão do template usado, mas o ponto central continua sendo a criação de um componente Razor com formulário e processamento no servidor.

## Como funciona a tela de login em Blazor

Normalmente, o componente de login é construído em um arquivo `.razor` e pode conter:

```razor
@page "/"

<h3>Login</h3>

<EditForm Model="loginModel" OnValidSubmit="Login">
    <InputText @bind-Value="loginModel.Usuario" />
    <InputText type="password" @bind-Value="loginModel.Senha" />
    <button type="submit">Entrar</button>
</EditForm>

@if (!string.IsNullOrEmpty(mensagem))
{
    <p>@mensagem</p>
}
```

A lógica do formulário pode ficar em um bloco `@code`:

```csharp
@code {
    private LoginModel loginModel = new();
    private string mensagem = string.Empty;

    private void Login()
    {
        if (string.IsNullOrWhiteSpace(loginModel.Usuario) || string.IsNullOrWhiteSpace(loginModel.Senha))
        {
            mensagem = "Preencha usuário e senha.";
            return;
        }

        if (loginModel.Usuario == "admin" && loginModel.Senha == "1234")
        {
            mensagem = "Login realizado com sucesso!";
        }
        else
        {
            mensagem = "Usuário ou senha inválidos.";
        }
    }
}
```

## Entendendo a abordagem

Blazor combina o modelo de UI reativa com o poder do C#:

- o navegador interage com o componente;
- o código do componente roda no servidor (ou em WebAssembly, dependendo da configuração);
- o estado da tela é atualizado sem recarregar a página inteira.

Isso diferencia o Blazor de HTML puro, Razor Pages e MVC, porque a interface é tratada como um conjunto de componentes reutilizáveis e reativos.

## Diferença em relação aos exercícios anteriores

| Exercício | Tecnologia | Como a validação acontece |
| --- | --- | --- |
| 8 | HTML + CSS + JS | No navegador, com JavaScript |
| 9 | Razor Pages | No servidor, com `OnPost` |
| 10 | MVC | No servidor, em `Controller` |
| 11 | Web API | No servidor, via endpoint HTTP |
| 12 | Blazor | No servidor/componente, com `OnValidSubmit` ou método do componente |

## Dica prática

Para esse exemplo didático, a lógica pode ser simples, mas em projetos reais o ideal é:

- validar entradas no cliente e no servidor;
- usar banco de dados para autenticação;
- armazenar senhas com hash;
- aplicar autorização e autenticação com Identity ou JWT.

## Exercício sugerido

Crie uma página de login em Blazor com:

1. campo usuário;
2. campo senha;
3. botão entrar;
4. mensagem de sucesso ou erro;
5. validação de campos vazios;
6. comparação com `admin` e `1234`.

Esse mesmo padrão pode ser expandido depois para uma aplicação com navegação, componentes reutilizáveis e autenticação completa.

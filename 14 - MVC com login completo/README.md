# ASP.NET Core MVC com login completo e segurança

Este exercício implementa um fluxo completo de autenticação com ASP.NET Core MVC. O usuário informa suas credenciais, o servidor consulta uma DAL, valida a senha derivada criptograficamente, cria um cookie de autenticação e libera um painel protegido. Também existe uma ação de logout que invalida o cookie.

> Este é um exemplo didático. O arquivo de texto substitui um banco de dados somente para demonstrar a separação DAO/DAL. Em produção, use um banco gerenciado, HTTPS obrigatório, armazenamento de segredos fora do código e, preferencialmente, ASP.NET Core Identity.

## Executar

Verifique se o SDK .NET 8 está instalado:

```powershell
dotnet --version
```

A partir desta pasta, restaure, compile e execute:

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5191
```

Acesse `http://localhost:5191`.

Credencial inicial do exemplo:

- Usuário: `admin`
- Senha: `Unip@1234!`

Depois do login, a aplicação redireciona para `/Login/Painel`. Tente abrir esse endereço em uma janela anônima: sem o cookie, o ASP.NET Core redireciona para o login.

## Estrutura

```text
14 - MVC com login completo/
|-- Controllers/
|   `-- LoginController.cs
|-- DAL/
|   |-- Database/
|   |   `-- login-users.txt
|   |-- LoginDao.cs
|   |-- LoginUserRecord.cs
|   `-- PasswordHasher.cs
|-- Models/
|   |-- DashboardViewModel.cs
|   `-- LoginViewModel.cs
|-- Views/
|   |-- _ViewImports.cshtml
|   |-- _ViewStart.cshtml
|   `-- Login/
|       |-- AcessoNegado.cshtml
|       |-- Index.cshtml
|       `-- Painel.cshtml
|-- wwwroot/
|   `-- styles.css
|-- LoginSeguroMvc.csproj
`-- Program.cs
```

## Fluxo de autenticação

1. `GET /Login` exibe o formulário.
2. O formulário envia usuário e senha por `POST` e inclui automaticamente o token antifalsificação por causa do Tag Helper e do `[ValidateAntiForgeryToken]`.
3. `LoginController` valida o formato básico e encaminha os dados ao `LoginDao`.
4. `LoginDao` lê o registro correspondente no arquivo da DAL. O Controller não acessa o arquivo diretamente.
5. `PasswordHasher` deriva um hash PBKDF2-SHA512 da senha digitada usando o salt e o número de iterações armazenados.
6. A comparação usa `CryptographicOperations.FixedTimeEquals`, reduzindo diferenças de tempo entre comparações.
7. Em caso de sucesso, o Controller cria uma identidade com uma claim de nome e chama `SignInAsync`.
8. O middleware de cookie serializa a identidade em um cookie protegido por Data Protection. O painel exige `[Authorize]`.
9. `Sair` chama `SignOutAsync` e remove a autenticação antes de voltar ao formulário.

Uma senha não é criptografada para ser recuperada. Ela é transformada em um derivado unidirecional: mesmo que alguém leia o arquivo, não encontra a senha em texto puro.

## DAL e arquivo simulado

A classe `LoginDao` é o objeto de acesso a dados (DAO). Sua responsabilidade é localizar e interpretar registros da fonte de dados simulada. A aplicação registra uma única instância no container de injeção de dependência:

```csharp
builder.Services.AddSingleton<LoginDao>();
```

O arquivo `DAL/Database/login-users.txt` possui uma linha por usuário, com quatro campos separados por `|`:

```text
usuario|iteracoes|salt-em-base64|hash-em-base64
```

O registro inicial se parece com isto, mas não contém a senha:

```text
admin|210000|TTGuPmMhq2avQA1tRl6HDA==|AezsEEn2m1wEt03hcifNSxj/1A7KYOAmG4pGW3vhQAc=
```

O projeto copia esse arquivo para a saída e para o publish por meio de `CopyToOutputDirectory` e `CopyToPublishDirectory`. Se o arquivo for alterado manualmente, mantenha o formato e gere um novo hash; nunca substitua os campos por uma senha real.

## Por que PBKDF2, salt e SHA-512?

`PasswordHasher.CriarHash` gera um salt aleatório de 16 bytes para cada senha. O salt impede que duas senhas iguais tenham o mesmo registro e dificulta o uso de tabelas pré-computadas. PBKDF2 repete o cálculo 210.000 vezes, tornando tentativas de força bruta mais caras.

O registro guarda o salt, as iterações e o resultado derivado de 32 bytes. Guardar o salt não é um problema: ele não precisa ser secreto. O segredo é a senha original. Na verificação, o mesmo algoritmo é executado com a senha recebida e o resultado é comparado ao hash salvo.

O método `Verificar` também calcula um derivado com valores fictícios quando o usuário não existe. Isso evita retornar imediatamente sem executar trabalho criptográfico e reduz a diferença de tempo entre usuário inexistente e senha incorreta.

Para uma nova senha, gere um registro com o método `CriarHash` em uma ferramenta administrativa protegida. Não coloque a senha em `login-users.txt`, no código-fonte ou em logs.

## Proteções MVC utilizadas

### Cookie de autenticação

`Program.cs` registra `AddCookie`. O cookie é `HttpOnly`, tem política `SameSite=Lax`, expira em 30 minutos e usa expiração deslizante. `HttpOnly` impede acesso comum ao cookie por JavaScript. `SameSite` reduz o envio em requisições cross-site.

`CookieSecurePolicy.SameAsRequest` facilita o exemplo em HTTP local. Em produção, sirva a aplicação somente com HTTPS e configure `CookieSecurePolicy.Always`.

### Autorização

A action `Painel` possui `[Authorize]`, portanto só é executada quando o middleware encontra uma identidade autenticada. Uma requisição sem autenticação é encaminhada para `/Login`.

A action `Sair` também exige autenticação e aceita somente `POST`. Logout por POST evita que uma simples visita a um link execute a operação. O formulário gera o token antifalsificação, que é validado no servidor.

### Antiforgery e redirecionamento

As operações que mudam estado (`login` e `logout`) usam `[ValidateAntiForgeryToken]`. O `returnUrl` só é seguido quando `Url.IsLocalUrl` confirma que o endereço aponta para a própria aplicação, evitando um redirecionamento aberto para outro site.

A mensagem de erro é genérica: `Usuário ou senha inválidos.`. Assim, a tela não informa se um usuário existe.

## Responsabilidade de cada camada

| Camada | Arquivos | Responsabilidade |
| --- | --- | --- |
| View | `Views/Login` | Formulário, mensagens e painel HTML/Razor. |
| Model | `Models` | Dados recebidos pelo formulário e dados exibidos. |
| Controller | `Controllers/LoginController.cs` | Recebe HTTP, valida antiforgery, chama a DAL e cria/remove a sessão. |
| DAL | `DAL/LoginDao.cs` | Lê o armazenamento simulado e entrega o resultado da autenticação. |
| Criptografia | `DAL/PasswordHasher.cs` | Gera e verifica derivados PBKDF2 com salt. |
| Middleware | `Program.cs` | Configura autenticação por cookie e autorização. |

O Controller não contém `admin`, não compara senha literal e não conhece o formato do arquivo. A única senha documentada é a credencial inicial do exercício; o arquivo guarda somente seu derivado.

## Teste manual

1. Execute a aplicação e abra `/Login/Painel` diretamente. O resultado esperado é redirecionamento para o login.
2. Envie uma senha errada. O resultado esperado é a mensagem genérica de credenciais inválidas.
3. Entre com `admin` e `Unip@1234!`. O resultado esperado é o painel autenticado.
4. Feche o cookie ou use uma janela anônima e confirme novamente o bloqueio do painel.
5. Clique em `Sair com segurança` e confirme que o painel volta a exigir login.
6. Inspecione `DAL/Database/login-users.txt` e confirme que não há senha em texto puro.

## Limitações intencionais

O arquivo de texto não tem transação, concorrência, cadastro, recuperação de senha, bloqueio progressivo, auditoria ou migrações. Essas são responsabilidades que devem ser resolvidas por uma solução de identidade e persistência adequada em um sistema real. O objetivo deste exercício é tornar visíveis as fronteiras MVC, a DAL/DAO e os princípios mínimos de armazenamento seguro de senhas.

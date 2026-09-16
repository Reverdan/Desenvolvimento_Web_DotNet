# ASP.NET Core Web API com DAL e Entity Framework Core

Este exercício parte da API do exercício [15 - WebAPI com DAL](../15%20-%20WebAPI%20com%20DAL/README.md) e introduz o **Entity Framework Core** como alternativa ao ADO.NET puro. A classe `Controle` continua sendo o ponto de entrada da regra de negócio; a diferença fica na camada de acesso a dados (`DAL/PessoaDAO.cs`), que passa a usar EF Core para uma das operações do CRUD.

## Executar

Verifique se o SDK .NET 10 está instalado:

```powershell
dotnet --version
```

Se necessário, instale o SDK:

```powershell
winget install Microsoft.DotNet.SDK.10
```

A partir desta pasta, restaure, compile e execute:

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5192
```

A API ficará disponível em `http://localhost:5192`.

A documentação interativa do Swagger ficará disponível em `http://localhost:5192/swagger` ou `http://localhost:5192/swagger/index.html`. O Swagger está habilitado independentemente do ambiente de execução. Se a aplicação for iniciada em outra porta, substitua `5192` na URL.

Após alterar a configuração, reinicie a aplicação para carregar o middleware do Swagger.

> As classes `DAL/Conexao.cs` e `DAL/ConexaoEntity.cs` mantêm a configuração de conexão do projeto original: SQL Server local, banco `ds34a`, usuário `sa` e senha `unip`. Ajuste essa configuração no ambiente local antes de testar as operações no banco.

## Endpoints

| Método | Rota | Operação |
| --- | --- | --- |
| `POST` | `/api/pessoas/logar` | Autentica um usuário fixo (`admin`/`123`), sem relação com o CRUD de pessoas. |
| `GET` | `/api/pessoas?nome=ana` | Pesquisa pessoas pelo nome. Informe um nome para realizar a pesquisa. |
| `GET` | `/api/pessoas/{id}` | Pesquisa uma pessoa pelo ID. |
| `POST` | `/api/pessoas` | Cadastra uma pessoa (via Entity Framework Core). |
| `PUT` | `/api/pessoas/{id}` | Edita uma pessoa. |
| `DELETE` | `/api/pessoas/{id}` | Exclui uma pessoa. |

Os endpoints `POST /api/pessoas` e `PUT /api/pessoas/{id}` recebem:

```json
{
  "nome": "Ana Silva",
  "rg": "123456789",
  "cpf": "12345678901"
}
```

As respostas incluem `mensagem` e, nas consultas, os dados em `dados`.

## O que é uma API REST?

API significa **Application Programming Interface**, ou Interface de Programação de Aplicações. Uma API define como outro sistema pode conversar com a aplicação. Neste projeto, um cliente HTML/JavaScript, aplicativo mobile ou outro programa envia requisições HTTP para a API e recebe respostas JSON.

REST significa **Representational State Transfer**. Não é uma biblioteca, uma linguagem ou um protocolo diferente do HTTP. REST é um conjunto de princípios para organizar serviços distribuídos usando os recursos e os verbos já existentes no protocolo HTTP.

Neste projeto, o recurso principal é `pessoas`. A API não cria uma URL diferente para cada ação, como `/cadastrarPessoa` ou `/excluirPessoa`. Ela usa a mesma coleção `/api/pessoas` e deixa o método HTTP indicar a intenção:

```text
POST   /api/pessoas       criar uma pessoa
GET    /api/pessoas/{id}  consultar uma pessoa
PUT    /api/pessoas/{id}  substituir ou atualizar uma pessoa
DELETE /api/pessoas/{id}  excluir uma pessoa
```

## O que significa RESTful?

RESTful significa **que uma API segue os princípios REST**. Uma API pode usar HTTP e JSON sem ser bem RESTful. Para ser RESTful, ela deve modelar a aplicação como recursos, usar os métodos HTTP de maneira coerente, comunicar o resultado por status HTTP e manter as requisições independentes entre si.

REST é o estilo arquitetural; RESTful é a característica de uma aplicação que implementa esse estilo. Por exemplo:

```text
API REST       conjunto de princípios e convenções
API RESTful    API implementada seguindo esses princípios
```

## Princípios REST aplicados nesta API

### 1. Recursos identificáveis por URLs

Uma URL representa um recurso, normalmente usando substantivos no plural. Neste exercício, `pessoas` representa a coleção de pessoas:

- `/api/pessoas` representa a coleção;
- `/api/pessoas/5` representa a pessoa de ID 5;
- `/api/pessoas?nome=ana` representa uma consulta filtrada da coleção.

O ID faz parte da URL porque identifica o recurso. Ele não precisa ser enviado novamente no corpo do `PUT`:

```http
PUT /api/pessoas/5
Content-Type: application/json
```

```json
{
    "nome": "Ana Souza",
    "rg": "123456789",
    "cpf": "12345678901"
}
```

### 2. Uso dos métodos HTTP

Cada método expressa uma operação conhecida pelo cliente e pelo servidor:

| Método | Uso geral | Neste projeto |
| --- | --- | --- |
| `GET` | Ler dados | Pesquisa por nome ou ID. |
| `POST` | Criar recurso | Cadastra uma nova pessoa. |
| `PUT` | Atualizar recurso conhecido | Edita a pessoa informada na URL. |
| `DELETE` | Remover recurso | Exclui a pessoa informada na URL. |

O método faz parte do contrato da API. Uma requisição `GET` não deve excluir dados, e uma requisição `DELETE` não deve ser usada para consultar uma pessoa.

### 3. Representações em JSON

REST não exige JSON, mas JSON é uma representação muito comum em APIs HTTP. O recurso pessoa é enviado no corpo da requisição e na resposta em formato estruturado:

```json
{
    "mensagem": "Pesquisa realizada com sucesso.",
    "dados": {
        "id": 5,
        "nome": "Ana Souza",
        "rg": "123456789",
        "cpf": "12345678901"
    }
}
```

O cliente não precisa conhecer as classes internas `Controle` ou `PessoaDAO`. Ele conhece o contrato HTTP, os campos JSON, as rotas e os status de resposta.

### 4. Uso dos status HTTP

O status HTTP informa o resultado geral da requisição, enquanto o campo `mensagem` fornece uma explicação legível para o cliente:

- `200 OK`: consulta, edição ou exclusão processada;
- `201 Created`: cadastro aceito;
- `400 Bad Request`: dados inválidos ou erro ao processar a operação, incluindo exceções lançadas pelo EF Core ao salvar (`SaveChanges`);
- `401 Unauthorized`: usuário ou senha inválidos no endpoint `logar`;
- `404 Not Found`: normalmente representa uma rota inexistente. A implementação atual ainda devolve o objeto vazio em algumas consultas sem registro;
- `500 Internal Server Error`: erro inesperado que, em uma API de produção, deve ser tratado por uma política global de exceções.

Uma resposta RESTful não deve depender somente de uma mensagem textual. O cliente deve observar primeiro o status HTTP e depois interpretar o JSON.

### 5. Stateless, ou sem estado de sessão

Uma requisição REST deve conter todas as informações necessárias para ser processada. O servidor não deve depender de uma tela anterior ou de uma variável de sessão para descobrir o que o cliente deseja.

Por exemplo, esta requisição é independente:

```http
GET /api/pessoas/5
```

O servidor identifica a operação pelo método `GET`, localiza o recurso pelo ID `5` e retorna a resposta. Neste exercício, a classe `Controle` é criada durante o processamento da requisição; a API não precisa manter uma sessão do cliente para executar o CRUD.

### 6. Cache e segurança

Respostas `GET` podem ser armazenadas em cache quando isso for adequado, porque a operação apenas consulta dados. Operações `POST`, `PUT` e `DELETE` alteram o estado do banco e normalmente não devem ser tratadas como respostas de cache.

REST não significa que a API seja automaticamente segura. Uma aplicação real deve usar HTTPS, autenticação, autorização, validação de entrada, proteção de credenciais e controle de acesso ao banco. Este exercício mantém a string de conexão didática do projeto original e deve ser usado apenas em ambiente de estudo.

## Idempotência dos métodos

Uma operação é idempotente quando repetir a mesma requisição produz o mesmo estado final esperado. Isso não significa necessariamente que todas as respostas textuais sejam idênticas.

- `GET` é idempotente: consultar várias vezes não altera o banco;
- `PUT` é idempotente quando o mesmo conteúdo é enviado novamente para o mesmo ID;
- `DELETE` é conceitualmente idempotente: depois que o recurso foi removido, repetir a remoção não deve recriá-lo;
- `POST` normalmente não é idempotente: repetir o cadastro pode criar duas pessoas, seja via ADO.NET ou via EF Core.

Essa diferença é importante quando um cliente precisa repetir uma requisição após uma falha de rede. Repetir `GET` costuma ser seguro; repetir `POST` pode gerar duplicidade sem uma estratégia adicional, como uma chave de idempotência.

## ADO.NET e Entity Framework Core lado a lado

Este exercício é intencionalmente híbrido: ele mantém as duas formas de acesso a dados no mesmo `PessoaDAO` para comparação.

```csharp
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet tool install --global dotnet-ef
```

- **`CadastrarPessoa`** usa `ConexaoEntity`, um `DbContext` do EF Core, e persiste a pessoa com `contexto.Pessoas.Add(pessoa)` e `contexto.SaveChanges()`. O EF Core gera o SQL de `INSERT` automaticamente a partir do mapeamento da classe `Pessoa`.
- **`PesquisarPessoaPorId`, `EditarPessoa`, `ExcluirPessoa` e `PesquisarPessoaPorNome`** continuam usando `SqlConnection`/`SqlCommand` do ADO.NET puro, com SQL escrito manualmente, como no exercício 15.

```text
CadastrarPessoa   -> ConexaoEntity (DbContext) -> EF Core -> SQL Server
Demais operações  -> Conexao (SqlConnection)    -> ADO.NET -> SQL Server
```

| Aspecto | ADO.NET (`Conexao`) | Entity Framework Core (`ConexaoEntity`) |
| --- | --- | --- |
| SQL | Escrito manualmente em cada método | Gerado automaticamente pelo EF Core |
| Mapeamento | Leitura campo a campo do `SqlDataReader` | Mapeamento automático da entidade `Pessoa` |
| Conexão | Reaberta/fechada manualmente (`Conectar`/`Desconectar`) | Gerenciada pelo `DbContext` |
| Verbosidade | Mais código por operação | Menos código por operação |
| Controle fino do SQL | Total | Depende do provider/EF Core |

O restante da API (controller, models, regras de negócio em `Controle`) não muda entre as duas abordagens, pois o contrato HTTP e o JSON trocado com o cliente são os mesmos independentemente de como os dados são persistidos.

## Endpoint fora do padrão REST: `logar`

O endpoint `POST /api/pessoas/logar` não representa o recurso `pessoas`; ele existe apenas como exemplo de rota adicional dentro do mesmo controller, sem relação com o CRUD. Rotas assim precisam de um segmento de rota próprio (`[HttpPost("logar")]`) para não colidir com o `POST /api/pessoas` do cadastro. Sem esse segmento, os dois métodos `POST` ficam mapeados para a mesma rota base e o Swagger falha ao gerar o documento com o erro `Conflicting method/path combination`.

## API REST, Web API e RESTful não são sinônimos perfeitos

Os termos são relacionados, mas não significam exatamente a mesma coisa:

- **Web API**: API acessível pela web, geralmente usando HTTP. Pode seguir REST ou outro estilo.
- **API REST**: API web organizada segundo os princípios REST.
- **API RESTful**: API que aplica de forma consistente as convenções REST.
- **Swagger/OpenAPI**: ferramenta e especificação para documentar e testar o contrato da API. Swagger não transforma uma API em RESTful; ele apenas descreve e apresenta suas rotas.

Esta aplicação é uma Web API ASP.NET Core com características RESTful: possui o recurso `pessoas`, usa rotas orientadas a recursos, emprega os métodos HTTP do CRUD, recebe e devolve JSON e publica status HTTP. Ela é uma implementação didática e ainda pode evoluir com respostas `404` mais precisas, tratamento global de exceções, autenticação, paginação, migração completa para EF Core e configuração segura do banco.

## Fluxo das camadas

```text
Cliente HTTP
    |
    v
PessoasController
    |
    v
Controle
    |
    v
PessoaDAO -+-> Conexao (ADO.NET)      -> SQL Server
           `-> ConexaoEntity (EF Core) -> SQL Server
```

O arquivo `Controllers/PessoasController.cs` é a classe de API. `modelo/Controle.cs`, `modelo/Pessoa.cs`, `modelo/Validacao.cs` e os arquivos da pasta `DAL` preservam a separação do projeto original, agora com `ConexaoEntity.cs` para o acesso via EF Core.

## Estrutura

```text
17 - WebApi com DAL Entity/
|-- Controllers/
|   `-- PessoasController.cs
|-- DAL/
|   |-- Conexao.cs
|   |-- ConexaoEntity.cs
|   `-- PessoaDAO.cs
|-- modelo/
|   |-- Controle.cs
|   |-- Pessoa.cs
|   `-- Validacao.cs
|-- Models/
|   `-- PessoaRequest.cs
|-- Program.cs
|-- WebApiPessoas.csproj
`-- README.md
```
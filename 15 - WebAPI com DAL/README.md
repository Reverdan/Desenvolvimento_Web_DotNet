# ASP.NET Core Web API com DAL

Este exercício transforma a classe `Controle` do projeto [PAN_DS43A_2_SEM_26](https://github.com/Reverdan/PAN_DS43A_2_SEM_26) em uma API HTTP. A API não altera a regra da classe `Controle`: o controller recebe JSON, converte os dados para o formato esperado pela classe e devolve JSON ao cliente.

## Executar

Verifique se o SDK .NET 8 está instalado:

```powershell
dotnet --version
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

> A classe `DAL/Conexao.cs` mantém a configuração de conexão do projeto original: SQL Server local, banco `ds34a`, usuário `sa` e senha `unip`. Ajuste essa configuração no ambiente local antes de testar as operações no banco.

## Endpoints

| Método | Rota | Operação |
| --- | --- | --- |
| `GET` | `/api/pessoas?nome=ana` | Pesquisa pessoas pelo nome. Sem `nome`, retorna todas. |
| `GET` | `/api/pessoas/{id}` | Pesquisa uma pessoa pelo ID. |
| `POST` | `/api/pessoas` | Cadastra uma pessoa. |
| `PUT` | `/api/pessoas/{id}` | Edita uma pessoa. |
| `DELETE` | `/api/pessoas/{id}` | Exclui uma pessoa. |

Os endpoints `POST` e `PUT` recebem:

```json
{
  "nome": "Ana Silva",
  "rg": "123456789",
  "cpf": "12345678901"
}
```

As respostas incluem `mensagem` e, nas consultas, os dados em `dados`.

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
PessoaDAO -> Conexao -> SQL Server
```

O arquivo `Controllers/PessoasController.cs` é a classe de API. `modelo/Controle.cs`, `modelo/Pessoa.cs`, `modelo/Validacao.cs` e os arquivos da pasta `DAL` preservam a separação do projeto original.

## Estrutura

```text
15 - WebAPI com DAL/
|-- Controllers/
|   `-- PessoasController.cs
|-- DAL/
|   |-- Conexao.cs
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
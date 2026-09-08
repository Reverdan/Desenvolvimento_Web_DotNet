# Cliente HTML da Web API com DAL

Este exercício cria um cliente HTML/CSS/JavaScript para consumir a API do exercício 15. A interface é dividida em duas páginas:

- `cadastro.html`: contém somente o cadastro de pessoas, com o botão de cadastro alinhado à direita, e consome `POST /api/pessoas`.
- `operacoes.html`: usa um único formulário com ID, nome, RG e CPF. Os botões `Pesquisar` ficam ao lado dos campos; `Excluir` fica à esquerda e `Salvar` à direita.

## Executar

1. Inicie a API da pasta `15 - WebAPI com DAL`:

```powershell
Set-Location "..\15 - WebAPI com DAL"
dotnet run --urls http://localhost:5192
```

2. Abra `cadastro.html` no navegador para cadastrar uma pessoa.
3. Use o link `Consultas e operações` para acessar `operacoes.html`.

Na página de operações, pesquise pelo ID ou nome para carregar uma pessoa nos campos compartilhados. A pesquisa por nome apresenta um botão `Usar` para preencher o formulário. Depois, use `Salvar` para editar o registro ou `Excluir` para removê-lo.

A URL da API está definida no início dos arquivos `cadastro.js` e `operacoes.js`:

```javascript
const API_URL = 'http://localhost:5192/api/pessoas';
```

Se a API estiver em outra porta, altere essa constante nos dois arquivos. A API já habilita CORS para permitir que este cliente estático seja executado em outra origem.

## Endpoints consumidos

| Página | Método | Rota | Finalidade |
| --- | --- | --- | --- |
| `cadastro.html` | `POST` | `/api/pessoas` | Cadastrar pessoa |
| `operacoes.html` | `GET` | `/api/pessoas?nome=...` | Pesquisar por nome preenchido |
| `operacoes.html` | `GET` | `/api/pessoas/{id}` | Pesquisar por ID |
| `operacoes.html` | `PUT` | `/api/pessoas/{id}` | Editar pessoa |
| `operacoes.html` | `DELETE` | `/api/pessoas/{id}` | Excluir pessoa |

## Estrutura

```text
16 - Cliente WebAPI com DAL/
|-- cadastro.html
|-- cadastro.js
|-- operacoes.html
|-- operacoes.js
|-- styles.css
`-- README.md
```

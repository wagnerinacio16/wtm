# Benchmark de Bancos de Dados: Azure SQL Database vs Azure Cosmos DB

Este projeto tem como objetivo comparar e analisar o desempenho de bancos de dados relacionais (SQL) e não relacionais (NoSQL) na nuvem, utilizando o **Azure SQL Database** e o **Azure Cosmos DB**. O foco está na avaliação das operações básicas de **INSERT**, **UPDATE** e **DELETE**, com análise dos resultados por meio de tabelas e gráficos.

## Contexto do Projeto

O sistema simula um **e-commerce para venda de produtos**, permitindo o cadastro de fornecedores, produtos, endereços e vendas. A aplicação foi desenvolvida para servir de base para o benchmark entre bancos de dados, utilizando operações típicas de um sistema de vendas online.

## Relacionamento entre as Tabelas

O modelo de dados é composto pelas seguintes entidades:

- **Produto**: Representa um item à venda. Cada produto está vinculado a um fornecedor.
- **Fornecedor**: Responsável pelo fornecimento dos produtos. Pode ser pessoa física ou jurídica e possui um endereço.
- **Endereço**: Informações de localização do fornecedor.
- **Venda**: Registra a venda de um produto, associando fornecedor, produto e quantidade.
- **TipoFornecedor**: Enumeração que define se o fornecedor é pessoa física ou jurídica.

As relações principais são:
- Um **Fornecedor** pode fornecer vários **Produtos**.
- Um **Fornecedor** possui um **Endereço**.
- Uma **Venda** está associada a um **Produto** e a um **Fornecedor**.

Abaixo está o diagrama de classes da aplicação, ilustrando o relacionamento entre as entidades:

![Diagrama de Classes](https://github.com/wagnerinacio16/wtm/blob/main/docs/diagrama_de_classe.png)


## Tecnologias Utilizadas

- **.NET 6+ (ASP.NET MVC)**
- **Entity Framework Core**
- **Azure SQL Database**
- **Azure Cosmos DB**
- **Bogus** (geração de dados fake)
- **Visual Studio Code / Visual Studio**

## Estrutura do Projeto

```
wtm/
├── docs/
│   ├── Relatorio_BDII.pdf       # Relatório acadêmico do projeto
│   └── diagrama_classes.png     # Diagrama de classes da aplicação
├── mvc/
│   ├── Controllers/             # Controladores MVC (lógica das rotas e operações)
│   ├── Models/                  # Modelos de dados (entidades do domínio)
│   ├── Faker/                   # Geração de dados fake para testes/benchmarks
│   ├── Data/                    # Contextos de banco de dados (EF Core)
│   ├── Migrations/              # Migrações do Entity Framework
│   ├── Views/                   # Páginas da interface web
│   ├── appsettings.json         # Configurações do projeto (strings de conexão, etc.)
│   ├── wtm.csproj               # Arquivo de projeto .NET
│   └── Program.cs               # Ponto de entrada da aplicação
└── README.md                    # Documentação do projeto
```

## Como Executar

1. **Clone o repositório:**
   ```sh
   git clone https://github.com/seu-usuario/seu-repositorio.git
   ```

2. **Configure as strings de conexão** para Azure SQL Database e Azure Cosmos DB no arquivo `mvc/appsettings.json`.

3. **Acesse a pasta do projeto MVC:**
   ```sh
   cd wtm/mvc
   ```

4. **Restaure os pacotes e execute o projeto:**
   ```sh
   dotnet restore
   dotnet run
   ```

5. **Acesse a aplicação** pelo navegador no endereço exibido no terminal (exemplo: http://localhost:5000).

## Resultados

Os resultados do benchmark são apresentados em tabelas e gráficos, permitindo a análise comparativa do desempenho entre Azure SQL Database e Azure Cosmos DB para as operações testadas. Para detalhes completos, consulte o relatório em [`docs/Relatorio_BDII.pdf`](docs/Relatorio_BDII.pdf).

## Autoria

Projeto desenvolvido para fins acadêmicos na disciplina de Banco de Dados II.

---

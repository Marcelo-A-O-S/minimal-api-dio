# 🚀 Minimal API - Bootcamp Avanade & DIO

Bem-vindo ao **projeto Minimal API** desenvolvido durante o Bootcamp da **Avanade** em parceria com a **DIO**!  
Este projeto foi construído utilizando **.NET 8**, seguindo boas práticas de desenvolvimento de APIs minimalistas, incluindo **CRUD completo**, validações, autenticação e testes automatizados.

---
## 📺 Apresentação

https://github.com/user-attachments/assets/aed74d50-1c6c-41d5-a8b2-e2b46779a45f

---

## 📌 Sobre o Projeto

O projeto é uma **Minimal API** que gerencia entidades como **Administrador** e **Veículo**, utilizando **Entity Framework Core** com **MySQL**, autenticação via **JWT** e documentação via **Swagger**.  

Além disso, há uma camada de **teste unitário** implementada com **MSTest**, cobrindo operações de CRUD nos serviços e endpoints da aplicação.

---

## 🛠 Tecnologias e Bibliotecas Utilizadas

Aqui estão as principais bibliotecas e seus propósitos:

- **Microsoft.AspNetCore.Authentication.JwtBearer** 🛡️  
  Implementa autenticação via **JWT (JSON Web Tokens)**, permitindo proteger endpoints da API.

- **Microsoft.EntityFrameworkCore** 🗄️  
  Biblioteca principal do **Entity Framework Core**, utilizada para manipulação de dados via ORM.

- **Microsoft.EntityFrameworkCore.Design** 🛠️  
  Ferramentas para **design-time**, suporte a migrações e scaffolding.

- **Microsoft.EntityFrameworkCore.Tools** 🔧  
  Fornece **comandos CLI** para migrações e gerenciamento do banco de dados.

- **Pomelo.EntityFrameworkCore.MySql** 🐬  
  Provider **MySQL** para o Entity Framework Core, permitindo conexão e manipulação de banco MySQL.

- **Swashbuckle.AspNetCore** 📜  
  Gera **documentação automática da API** via Swagger, permitindo testes e visualização dos endpoints.

---

## 🏗 Estrutura do Projeto

O projeto segue a seguinte estrutura:

```
├───Api -> Projeto WebApi
│   ├───Dominios
│   │   ├───DTOs -> Data Transfer Object
│   │   ├───Entities -> Entidades de dados do dominio
│   │   ├───Enums -> Pasta de enums envolvendo as entidades
│   │   ├───Interfaces -> Contratos de serviços e funções utilitarias 
│   │   ├───ModelViews -> Objetos de modelo para visualização
│   │   ├───Services -> Pasta da lógica de negócios do projeto
│   │   └───Validators -> Pasta com funções de validações de objetos
│   ├───Extensions -> Mapeamento de rotas
│   │   └───Configs -> Configurações adicionais
│   ├───Infraestrutura
│   │   └───DB -> Configuração do contexto
│   ├───Migrations -> Migrações do banco de dados
│   └───Properties
└───Test -> Projeto MSTest
    ├───Dominio -> Pasta aonde se encontra a regra de dominio
    │   ├───Entities -> Pasta de testes de entidade
    │   └───Services -> Pasta de testes das regras de negócios
    │       └───Interfaces -> Pasta de constratos de regras de negócio para implementação
    ├───Helpers -> Configuração para testes da API
    ├───Infraestrutura -> Configuração do contexto para testes
    ├───Mocks -> Configuração para simulação e teste da api
    ├───Requests -> Testes de endpoints da aplicação
         └───Interfaces -> Contratos de testes das requisições
```

- A **camada de domínio** contém a lógica de negócio e validações.  
- A **camada de testes** utiliza MSTest para validar operações CRUD.  
- A **API** expõe endpoints seguros, utilizando autenticação JWT e Swagger para documentação interativa.

---

## ⚡ Funcionalidades

- CRUD completo de **Administradores** e **Veículos**  
- Validações de dados antes de persistir no banco  
- Paginação para listagens  
- Autenticação JWT para proteção de endpoints  
- Documentação Swagger pronta para uso  
- Testes unitários cobrindo operações de CRUD  

---

## 📖 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [MySQL](https://dev.mysql.com/downloads/) ou outro banco compatível  

### Passos

1. Clone o repositório:  
```
git clone <URL_DO_SEU_REPOSITORIO>
```
2. Configure a conexão com o banco de dados em `appsettings.json` para o projeto API e Test.
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "mysql":"adicione_a_connection_string_aqui"
  },
  "Jwt":"adicione_aqui_sua_chave_jwt"
}
```
3. Execute as migrações:  
```csharp
dotnet ef database update
```
4. Rode a API:
```bash
dotnet run --project Api
```
5. Acesse a documentação via `Swagger`:
```bash
https://localhost:{PORTA}/swagger
```
6. Para rodar os testes:
```bash
dotnet test
```

# UsuariosApi

API REST desenvolvida em ASP.NET Core para cadastro de usuários com operações completas de CRUD.
O projeto foi construído com controllers, camada de serviço e repositório, utilizando MongoDB como persistência
e Swagger/OpenAPI para documentação e testes da API.
O objetivo é disponibilizar uma solução simples e organizada para criação, leitura, atualização e exclusão de usuários.

---

## 🚧 Status do Projeto

Projeto acadêmico **concluído no escopo de CRUD de usuários com MongoDB**. O código fica disponível como exercício de organização de API e persistência. As limitações do exemplo estão descritas em [Estado e limites](#estado-e-limites).

![Status](https://img.shields.io/badge/status-conclu%C3%ADdo-success?style=for-the-badge)
![Backend](https://img.shields.io/badge/backend-ASP.NET%20Core-007ec6?style=for-the-badge)
![Database](https://img.shields.io/badge/database-MongoDB-007ec6?style=for-the-badge)
![License](https://img.shields.io/badge/license-MIT-007ec6?style=for-the-badge)

---

## 📚 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Variáveis de Ambiente](#-variáveis-de-ambiente)
- [Como Rodar Localmente](#-como-rodar-localmente)
- [Build](#-build)
- [Endpoints Principais](#-endpoints-principais)
- [Fluxos Principais](#-fluxos-principais)
- [Estrutura de Pastas](#-estrutura-de-pastas)
- [Documentação](#-documentação)
- [Troubleshooting](#-troubleshooting)
- [Autores](#-autores)
- [Licença](#licença)

---

## 📝 Sobre o Projeto

O **UsuariosApi** é uma API REST para gerenciamento de cadastros de usuários.
A solução permite registrar e consultar informações como nome, e-mail, senha, código de pessoa,
lembrete de senha, idade e sexo, centralizando o CRUD em uma aplicação backend simples.

O repositório reúne:

- API em ASP.NET Core;
- regras de negócio organizadas em camadas;
- integração com MongoDB;
- documentação interativa com Swagger.

---

## Estado e limites

`Program.cs` registra `MongoUsuarioRepository` como persistência ativa e cria índices únicos de e-mail e código de pessoa ao iniciar. O repositório InMemory existente não está selecionado. O MongoDB precisa estar acessível antes de a API iniciar.

Não há autenticação, autorização ou projeto de testes automatizados nesta base. Senhas são armazenadas e retornadas como campos da entidade, sem hash. Use dados fictícios neste exercício acadêmico; ele não implementa um serviço de identidade pronto para produção.

## ✨ Funcionalidades

### Funcionalidades principais

- Cadastro de usuários
- Listagem de todos os usuários
- Busca de usuário por e-mail
- Atualização completa com `PUT`
- Atualização parcial com `PATCH`
- Exclusão de usuário com `DELETE`

---

## 🛠 Tecnologias

### Backend

- **ASP.NET Core Web API**
- **C#**
- **Swagger / OpenAPI**
- **MongoDB.Driver**

### Banco de Dados

- **MongoDB**

### Documentação

- **Markdown**
- **Swagger UI**

---

## 🏗 Arquitetura

### Visão Geral

O projeto adota uma arquitetura em camadas, com separação entre entrada HTTP, regras de negócio e persistência.

### Organização por Camadas

- **Controllers** para exposição dos endpoints da API;
- **Services** para orquestração das regras de negócio;
- **Repositories** para acesso ao MongoDB;
- **Requests/Responses** para contratos de entrada e saída;
- **Entities** para representação do domínio;
- **Settings** para configuração da conexão com banco.

### Fluxo Geral da Aplicação

`Cliente -> Controller -> Service -> Repository -> MongoDB`

### Padrões e Convenções Adotados

- separação clara de responsabilidades;
- controllers enxutos;
- lógica concentrada em services;
- persistência isolada em repositories;
- uso de DTOs para entrada;
- organização simples, legível e de fácil manutenção.

---

## 🔐 Variáveis de Ambiente

Neste projeto, a configuração principal está em `appsettings.json`.

| Variável                            | Obrigatória | Contexto | Descrição                       | Exemplo                     |
| ----------------------------------- | ----------- | -------- | ------------------------------- | --------------------------- |
| `MongoDbSettings__ConnectionString` | Sim         | Backend  | String de conexão com o MongoDB | `mongodb://localhost:27017` |
| `MongoDbSettings__DatabaseName`     | Sim         | Backend  | Nome do banco MongoDB           | `cadastrodb`                |
| `MongoDbSettings__CollectionName`   | Sim         | Backend  | Nome da coleção de usuários     | `usuarios`                  |

As variáveis acima sobrescrevem os campos correspondentes de `usuarios-api/appsettings.json`. Não há carregamento de `.env`.

### Exemplo de configuração

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "cadastrodb",
    "CollectionName": "usuarios"
  }
}
```

---

## ▶ Como Rodar Localmente

### Pré-requisitos

- **.NET 8 SDK**
- **MongoDB Server**
- **mongosh** ou MongoDB Compass
- **Git**

### Passos

```bash
git clone https://github.com/PHnsilva/usuarios-api-crud.git
cd usuarios-api-crud/usuarios-api
dotnet restore
dotnet build
dotnet run
```

### MongoDB

Certifique-se de que o MongoDB esteja em execução e que a string `mongodb://localhost:27017` corresponda à sua instância. Essa string é uma configuração, não um comando de terminal. O banco padrão é `cadastrodb` e a coleção é `usuarios`.

O perfil `UsuariosApi` de `Properties/launchSettings.json` define o ambiente Development e as portas abaixo. `Program.cs` habilita Swagger somente nesse ambiente.

### Swagger

Após iniciar a aplicação, acesse:

```bash
http://localhost:5064/swagger
```

ou

```bash
https://localhost:7064/swagger
```

---

## 🧱 Build

```bash
dotnet build
```

Para executar:

```bash
dotnet run
```

---

## 🔌 Endpoints Principais

| Método   | Endpoint                      | Descrição                                 |
| -------- | ----------------------------- | ----------------------------------------- |
| `POST`   | `/api/usuarios`               | Cadastra um novo usuário                  |
| `GET`    | `/api/usuarios`               | Lista todos os usuários                   |
| `GET`    | `/api/usuarios/email/{email}` | Busca um usuário pelo e-mail              |
| `PUT`    | `/api/usuarios/email/{email}` | Atualiza todos os dados do usuário        |
| `PATCH`  | `/api/usuarios/email/{email}` | Atualiza parcialmente os dados do usuário |
| `DELETE` | `/api/usuarios/email/{email}` | Remove um usuário pelo e-mail             |

### Documentação da API

- **Swagger / OpenAPI:** `http://localhost:5064/swagger` ou `https://localhost:7064/swagger`

---

## 🔄 Fluxos Principais

### 1. Cadastro de usuário

1. Cliente envia os dados para a rota `POST /api/usuarios`
2. O controller recebe a requisição
3. O service valida as regras de negócio
4. O repository persiste o documento no MongoDB

### 2. Consulta de usuário

1. Cliente chama a rota `GET /api/usuarios` ou `GET /api/usuarios/email/{email}`
2. O controller encaminha a solicitação
3. O service consulta o repository
4. O MongoDB retorna os dados cadastrados

### 3. Atualização e exclusão

1. Cliente envia `PUT`, `PATCH` ou `DELETE`
2. O controller identifica o usuário pela chave e-mail
3. O service valida a operação
4. O repository atualiza ou remove o documento no MongoDB

---

## 📁 Estrutura de Pastas

```txt
.
├── Application/
│   ├── Contracts/
│   └── Services/
├── Controllers/
├── Domain/
│   ├── Contracts/
│   └── Entities/
├── Infrastructure/
│   ├── Persistence/
│   └── Repositories/
├── Properties/
├── Scripts/
├── appsettings.json
├── Program.cs
└── UsuariosApi.csproj
```

---

## 📖 Documentação

### Testes da API

Não há suíte automatizada versionada. A compilação é verificada com `dotnet build`; o comportamento depende de teste manual com MongoDB acessível. Confira criação (201), duplicata (409), busca inexistente (404), atualização completa e exclusão (204).

- Swagger UI para execução interativa dos endpoints
- Postman ou Swagger para evidências do CRUD no relatório final

### Banco de Dados

- MongoDB local para persistência dos documentos
- Coleção principal: `usuarios`

---

## 🛠 Troubleshooting

### Erro de certificado HTTPS no Swagger

Se o Swagger abrir, mas as requisições falharem com problema de HTTPS, rode:

```bash
dotnet dev-certs https --trust
```

### Erro de chave duplicada no MongoDB

Se ocorrer erro `DuplicateKey`, verifique se já existe documento com o mesmo:

- `email`
- `codigoPessoa`

Os índices são criados durante a inicialização. Se já existirem dados duplicados, revise os registros no banco de desenvolvimento antes de iniciar; apagar a coleção não é uma etapa normal de execução.

### API não conecta no MongoDB

Verifique:

- se o serviço do MongoDB está rodando;
- se a `ConnectionString` está correta;
- se o banco e a coleção estão configurados no `appsettings.json`.

---

## 👥 Autores

Projeto desenvolvido por:

- **Pedro Henrique**
  - GitHub: [PHnsilva](https://github.com/PHnsilva)
  - LinkedIn: [linkedin.com/in/PHnsilva1](https://linkedin.com/in/PHnsilva1)

## Contribuição

Abra uma branch e um pull request com escopo, alterações no contrato e validação. Não publique dados reais de usuários ou credenciais MongoDB.

## Licença

[MIT](LICENSE), conforme o arquivo existente no repositório.

# API de Gerenciamento de Plano de Contas

## Pré-requisitos

Antes de começar, garanta que você tem as seguintes ferramentas instaladas:

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* Um editor de código ou IDE, como [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/).

## Configuração e execução

Siga os passos abaixo para configurar e executar a aplicação localmente.

### 1. Clonar o repositório

```bash
git clone https://github.com/alexmachadodev/dotnet-plano-contas-handson.git
cd dotnet-plano-contas-handson
```

### 2. Iniciar o banco de dados com Docker
```bash
docker-compose up -d planocontadb
```

### 3. Instale a ferramenta dotnet-ef
```bash
dotnet tool install dotnet-ef
```

### 4. Aplicar as migrations (criar o banco de dados)
```bash
dotnet ef database update --project src/PlanoContaHandsOn.Infrastructure --startup-project src/PlanoContaHandsOn.API
```

### 5. Executar a API localmente
```bash
dotnet run --project src/PlanoContaHandsOn.API
```

## ⚠️ Nota importante sobre segurança e segredos

Para facilitar a clonagem e execução local deste projeto de demonstração, a senha do banco de dados foi incluída diretamente nos arquivos de configuração (`appsettings.json` e `docker-compose.override.yml`).

**🔴 Esta abordagem NUNCA deve ser usada em um ambiente de produção.**

🛡️ Versionar informações sensíveis (segredos) como senhas, chaves de API ou strings de conexão diretamente no código-fonte é uma falha de segurança grave.

### ✅ Alternativas recomendadas:
- **Ambientes reais:**  
  - Serviços de cofre de segredos como:  
    - Azure Key Vault  
    - AWS Secrets Manager  
    - HashiCorp Vault  

- **Desenvolvimento local:**  
  - Ferramenta .NET User Secrets (para evitar o versionamento de senhas)
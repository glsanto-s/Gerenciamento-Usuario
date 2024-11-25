# Gerenciamento de Usuário - Projeto com Arquitetura Hexagonal

Este repositório implementa um sistema de gerenciamento de usuários bem simples utilizando **Arquitetura Hexagonal (Ports and Adapters)**, permitindo maior modularidade, facilidade de testes e desacoplamento entre componentes centrais e periféricos.
O projeto teve mais como objetivo, aprendar sobre a arquitetura hexagonal na prática.

## Estrutura do Projeto

O projeto está dividido em três camadas principais, seguindo os princípios da Arquitetura Hexagonal:

1. **Core (Núcleo da Aplicação)**:
   - **Domain**: Contém as entidades, regras de negócio e contratos principais.
   - **Application**: Implementa os casos de uso da aplicação e orquestra o fluxo de dados entre os domínios e os adaptadores.

2. **Adapters**:
   - **Driven Adapters**: Inclui os componentes que a aplicação consome, como bancos de dados e mensageria (por exemplo, Kafka).
   - **Driving Adapters**: Abriga os controladores da API REST e outros serviços externos que interagem com o núcleo.

3. **Tests**:
   - Pastas dedicadas a testes unitários e de integração para validar os casos de uso e interações.

## Funcionalidades

- **Criação de usuários**: Permite adicionar novos usuários ao sistema.
- **Produção de mensagens Kafka**: Após a criação de um usuário, é gerada uma mensagem para um tópico Kafka.
- **Consumo de mensagens Kafka**: Um serviço consumidor processa as mensagens, enviando e-mails conforme necessário.

## Como Funciona

1. **Fluxo principal**:
   - O controlador recebe a requisição para criar um usuário.
   - A camada de **Application** valida e processa os dados utilizando as regras definidas na **Domain**.
   - Após a criação, o adaptador Kafka publica uma mensagem no tópico configurado.

2. **Worker**:
   - Um serviço consumidor processa as mensagens Kafka.
   - Dependendo do conteúdo, ele realiza ações como envio de e-mails ou atualização de status.

3. **Configuração**:
   - Projetos e configurações são gerenciados no arquivo `Gerenciamento-Usuario-Hexagonal.sln`, facilitando o desenvolvimento em Visual Studio.

## Tecnologias Utilizadas

- **ASP.NET Core**: Para a API.
- **Kafka**: Para mensageria.
- **Banco de Dados**: Para persistência de dados dos usuários.
- **Background Workers**: Para processar tarefas assíncronas como consumo de mensagens Kafka.

## Execução

1. Clone o repositório:
   ```bash
   git clone https://github.com/glsanto-s/Gerenciamento-Usuario.git
2. Configure o arquivo appsettings.json com as conexões de banco, detalhes do Kafka e configuração para o envio de e-mail.
3. Compile e execute o projeto no Visual Studio ou utilizando o dotnet CLI:
   ```bash
   dotnet build
   dotnet run

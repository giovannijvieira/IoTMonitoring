# IoT Monitoring Backend

Este projeto fornece uma API para monitoramento e controle de dispositivos IoT. A API permite que os usuários registrem dispositivos, enviem comandos e recebam respostas de dispositivos conectados.


- **Segurança Robusta**: Implementação de autenticação e autorização via JWT, garantindo que apenas usuários autenticados possam acessar certas rotas.
- **Arquitetura Modular**: Utilização de uma arquitetura em camadas e do padrão de repositório, facilitando a manutenção e escalabilidade do projeto.
- **Documentação Completa**: A API é totalmente documentada com Swagger, proporcionando uma interface interativa para explorar e testar as rotas disponíveis.
- **Flexibilidade de Comunicação**: Suporte para enviar comandos e receber respostas de dispositivos IoT.

## Decisões de Design e Implementação

### Arquitetura
- **Arquitetura em Camadas**: O projeto foi implementado utilizando uma arquitetura em camadas, separando as preocupações de lógica de negócios, acesso a dados e apresentação.
- **ASP.NET Core**: O framework escolhido para o backend é o ASP.NET Core devido à sua robustez, desempenho e flexibilidade para criar APIs modernas.
- **Entity Framework Core**: Utilizado para acesso e manipulação de dados, facilitando a interação com o banco de dados MySQL.
- **JWT para Autenticação**: Implementado JSON Web Token (JWT) para autenticação e autorização, garantindo que apenas usuários autenticados possam acessar certas rotas.

### Decisões de Implementação
- **Models**: Foram criados modelos para representar dispositivos, comandos e parâmetros, estruturando os dados de maneira clara e escalável.
- **Controllers**: Os controladores foram organizados para gerenciar as rotas da API, incluindo autenticação, registro de dispositivos e envio de comandos via Telnet.
- **Services**: Serviços foram implementados para abstrair a lógica de negócio, para enviar comandos e receber respostas de dispositivos.
- **Repository Pattern**: O padrão de repositório foi utilizado para abstrair a lógica de acesso a dados, facilitando a manutenção e testes do código.
- **Documentação com Swagger**: A API foi documentada utilizando Swagger, proporcionando uma interface interativa para explorar e testar as rotas disponíveis.

### Segurança
- **Proteção das Rotas com [Authorize]**: Todas as rotas críticas foram protegidas com a anotação `[Authorize]`, garantindo que apenas usuários autenticados possam acessá-las.
- **Validação de Entrada**: Validações foram implementadas para assegurar que os dados enviados pelo usuário sejam corretos e seguros.

## Sugestões de Melhorias e Avanços Futuros

1. **Melhorias na Segurança**:
   - Implementar políticas de segurança mais avançadas, como limitação de taxa de requisições (rate limiting) e proteção contra ataques de força bruta.

2. **Monitoramento e Logs**:
   - Adicionar ferramentas de monitoramento e logging para rastrear o desempenho e identificar problemas em tempo real.

3. **Escalabilidade**:
   - Considerar a implementação de microserviços para dividir a aplicação em componentes menores e mais gerenciáveis, permitindo uma escalabilidade horizontal mais eficiente.

4. **Testes Automatizados**:
   - Desenvolver uma suíte de testes automatizados abrangente, incluindo testes unitários, de integração e de desempenho, para garantir a qualidade e a estabilidade do código.

5. **Suporte a Múltiplos Protocolos**:
   - Expandir o suporte para outros protocolos de comunicação além de HTTP e MQTT, aumentando a versatilidade da API para diferentes tipos de dispositivos IoT.


6. **Internacionalização (i18n)**:
   - Implementar suporte a múltiplos idiomas para tornar a API acessível a uma base de usuários mais ampla.

## Como Executar

### Pré-requisitos
- .NET 8 SDK
- MySQL

### Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/giovannijvieira/IoTMonitoring.git
   cd IoTMonitoring
   ```

2. Configure as variáveis de ambiente:
   - As variáveis de ambiente necessárias serão enviadas por e-mail. Certifique-se de adicioná-las ao arquivo `.env` na raiz do projeto.

3. Restaure os pacotes e execute as migrações:
   ```bash
   dotnet restore
   dotnet ef database update
   ```

4. Execute a aplicação:
   ```bash
   dotnet run
   ```

   usuário teste:
"usuario1@wvblabs.org"
"SenhaForte!123"
Mysql Database:
CREATE DATABASE IoTMonitoring;
CREATE USER 'iotuser'@'localhost' IDENTIFIED BY 'M0qi8ebRoTh+StEpEHiW';
GRANT ALL PRIVILEGES ON IoTMonitoring.* TO 'iotuser'@'localhost';
FLUSH PRIVILEGES;
EXIT;
### Documentação da API

A documentação da API está disponível através do Swagger. Após iniciar a aplicação, acesse `http://localhost:5160/swagger` para visualizar e testar as rotas disponíveis.

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests com sugestões e melhorias.





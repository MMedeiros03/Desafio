# DesafioBenner

Desafio de criar uma API Rest de um Estacionamento proposto pela empresa Benner.

# Pré-requisitos
 Para rodar o projeto é necessário ter instalado:

    - SDK do .NET 6
    - SQL Server
    - Visual Studio 2022

# Instalação

Este projeto não exige instalar nenhuma dependencia antes de rodar. Basta abrir o projeto no Visual Studio e clicar em "Iniciar" para iniciar o projeto

# Uso
  O sistema de gerenciamento de estacionamento é uma aplicação que permite controlar as operações de um estabelecimento de estacionamento. Ele oferece funcionalidades essenciais, como o cadastro dos valores cobrados pelo estacionamento e a definição das datas de vigência dessas tarifas.
  Além disso, o sistema possibilita o registro das entradas e saídas dos veículos no estacionamento. Quando um veículo é registrado para sair, o sistema realiza o cálculo do valor a ser pago com base no tempo de permanência do veículo no estacionamento.
  Com essa solução, os operadores do estacionamento podem manter um controle preciso das informações, garantindo uma cobrança adequada e eficiente aos clientes. Isso permite uma gestão mais eficaz do estabelecimento, oferecendo um serviço de qualidade aos usuários do estacionamento.

# Estrutura do Projeto

O sistema esta dividido em três soluções: 
  - DesafioBenner -> Solução principal do projeto onde ficam as Controllers, Services e Repositories do projeto. 
      - Controllers -> Em Controllers ficam as classes responsáveis por receber, processar e responder às solicitações do cliente.
      - DTO -> Em DTO ficam as classes responsaveis por fornecer uma estrutura comum para a comunicação entre diferentes partes do sistema, evitando assim a exposição direta dos modelos de dados internos ou das entidades                  do banco de dados.
      - Services -> As Services são responsáveis por manipular os dados, aplicar regras de negocio e validar valores recebidos.
      - Repositories -> A Repositories são responsaveis por abstrair a camada de acesso a dados e fornecer uma interface para realizar operações de persistência e recuperação de dados.
  - Infrastructure -> Solução responsável por classes uteis utilizadas em todo o projeto. Nela ficam a conexão com o banco, middlewares, entidades que representam as tabelas do banco de dados, etc...
      - Entities -> Aqui ficam armazenadas as entidades que serão os modelos utilizados para gerar as tabelas do banco de dados.
      - DataBase -> Aqui é feita a conexão com o banco de dados e feita a modelagem de dados, definindo mapeamentos entre as entidades e as tabelas do banco de dados.
      - Utils -> Aqui ficam metodos utilitarios utilizados pelo projeto. Nessas classes são feitos os calculos de quanto tempo um veiculo ficou e quando deve pagar.
      - Middleware -> Aqui ficam os middlewares desenvolvidos que são responsáveis por executar uma lógica específica durante o pipeline de processamento de uma requisição.
  - Tests -> Solução responsável por fazer os testes do projeto. Nessa solução ficam os teste unitarios e testes de integração do projeto. Por meio dessa solução é possivel validar se metodos estão executando 
      da forma esperada no sistema.
      
# Agradecimentos
Agradeço desde já pela oportunidade :)

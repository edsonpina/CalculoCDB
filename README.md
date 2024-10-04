# CalculoCDB

Projeto CalculoCDB

## Sumário


[Visão Geral](#visão-geral)

[Pré-requisitos](#pré-requisitos)

[Instruções de Deploy no Docker](#instruções-de-deploy-no-docker)

[Relatório de Cobertura dos Testes](https://edsonpina.github.io/CalculoCDB/)

[Instruções sobre Testes](#instruções-sobre-testes)

[Abrir no Visual Studio](#abrir-no-visual-studio)




## Visão Geral


Esta aplicação é composta por duas partes:

Backend (API): Construído com .NET 8.
Frontend (Angular): Uma aplicação Angular que se comunica com a API.
O projeto foi configurado para rodar tanto localmente quanto em contêineres Docker para facilitar o deploy e a execução.

[Voltar](#sumario)




## Pré-requisitos


Antes de rodar a aplicação (tanto o backend quanto o frontend), certifique-se de que você tem os seguintes pré-requisitos instalados:

Backend (API .NET 8)

.NET 8 SDK.

Visual Studio 2022 (com a carga de trabalho ASP.NET e desenvolvimento web).

Docker: Certifique-se de que o Docker Desktop está instalado e em execução.

Frontend (Angular)

Node.js: Instale a versão 18 ou superior. 

Angular CLI: Instale globalmente com o comando:

npm install -g @angular/cli

[Voltar](#sumário)



## Instruções de Deploy no Docker


O deploy da aplicação pode ser feito utilizando Docker Compose para rodar o backend (.NET API) e o frontend (Angular) em contêineres Docker.

1. Clone o Repositório
   
Clone o repositório para a sua máquina local:

git clone https://github.com/edsonpina/CalculoCDB.git

cd CalculoCDB

2. Certifique-se de que o Docker está rodando
   
Verifique se o Docker Desktop está rodando no seu sistema:

docker --version

4. Build e Deploy dos Contêineres
   
Dentro do diretório raiz do projeto, execute o comando abaixo para construir e iniciar os contêineres:

docker-compose up --build

Esse comando irá:

Construir as imagens Docker para o backend (API .NET) e o frontend (Angular).

Inicializar ambos os contêineres e expor as portas necessárias.
4. Acessar a Aplicação

Uma vez que os contêineres estiverem rodando:

Acesse o frontend Angular no seu navegador em: http://localhost:4200

A API estará disponível em: http://localhost:44352

cURL de exemplo:
curl --location 'http://localhost:44352/CalculoCDB' \
--header 'valorInicial: 20000' \
--header 'meses: 25' \
--header 'accept: text/plain'

[Voltar](#sumário)



## Instruções sobre Testes

[Clique aqui para acessar Relatório de Cobertura dos Testes](https://edsonpina.github.io/CalculoCDB/)

Backend (API .NET)

Rodando os Testes Unitários

Para rodar os testes unitários da API .NET, use o comando:

dotnet test

Isso executará todos os testes configurados no projeto de testes da API.

Frontend (Angular)
Rodando os Testes Unitários
Para rodar os testes unitários do Angular, utilize o seguinte comando:

ng test

Isso abrirá um navegador e executará os testes, exibindo o resultado no terminal e na janela do navegador.

[Voltar](#sumário)



## Abrir no Visual Studio
Passos para abrir o projeto no Visual Studio:

Abra o Visual Studio 2022.

Clique em "Abrir um projeto ou solução".

Navegue até o diretório raiz do repositório clonado e selecione o arquivo CalculoCDB.sln.

O Visual Studio carregará tanto o projeto da API quanto os projetos de dependência.

Para iniciar o desenvolvimento:

Para rodar o backend (API): Certifique-se de que o projeto CalculoCDB.API está definido como o projeto de inicialização, e clique em Iniciar Depuração.

Para rodar o frontend (Angular): No terminal do Visual Studio, navegue até a pasta calculo-cdb e execute o comando ng serve.

Observações Finais

Siga as instruções acima para garantir que a aplicação funcione corretamente. Caso tenha dúvidas ou problemas durante a configuração, verifique os logs de erro no terminal ou entre em contato pelo email edsonpina@hotmail.com.

[Voltar](#sumário)

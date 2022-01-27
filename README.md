# eCommerce_VS2019
Projeto criado usando o Dapper para requisições

Dapper: Micro-ORM criado em 2011 durante o desenvolvimento do site StackOverflow para resolver problema de performance, leve, enxuto, focado em performance e simples de usar.
Utilizado na execução de queries e mapeamento de objetos e funciona com bancos de dados que suportem ADO.NET, ou seja, SQL Server, Oracle, MySQL etc.

Dapper é considerado Micro-ORM pois não utiliza o método tradicional(ADO.NET) que é mais oneroso para o programador digitando linha a linha sem ter nada automatizado e também não pode ser considerado um ORM que usa o EF Core(Entity Framework) que é mais pesado e onera o desempenho devido a praticidade e o leque de recursos.
ORMs mais populares EF ou NHibernate que geram queries automaticamente, Tracking (faz o acompanhamento do objeto na memória), Lazy Loading, Unit of Works etc.


Ambiente de desenvolvimento
- Instalar através do Visual Studio Installer os pacotes:
- ASP.NET e desenvomvimento Web, Processamento e armazenamento de dados com o SQL Server.
- Instalar o Postman;

API - Fornece Dados ao Cliente, contém regra de negócio e se comunica com o banco de dados.
API do Tipo REST(HTTP) : get(recebe dados), post(envia dados), put(atualiza dados), patch(atualiza apenas algumas informações), delete(eliminar dados) etc;

Criando o Projeto:
Para criar o projeto do Visual Studio 2019 com GitHub foi necessário:
- Abrir o VS2019 e instalar a extensão do GitHub e reiniciar o VS2019;
- Clicar em Create dentro de Team Explorer para criar um novo repositório e linkar no github com login e senha;
- Apontar a pasta onde será criado o projeto vs2019, neste caso D:\Arquivos Pessoais\Cursos\Dapper\eCommerce e definir um nome de repositório no GitHub, neste caso foi o eCommerce_VS2019 que conterá todos os projetos da API.
- O VS2019 irá criar o arquivo .gitignore já com as informações a desconsiderar e o README.md
- Com a extensão do Git para o VS2019 é possível fazer os o controle das alterações via (pull, push, commits etc);
- Abrir o VS219, criar um novo projeto e filtrar por ASP.NET API e selecionar o tipo de projeto ASP.NET Core Web API (A project template for creating an ASP.NET Core application with an example Controller for a RESTfull HTTP service. This template can also be used for ASP.NET Core MVC Views and Controllers).
- Nomear o projeto como eCommerce.API e selecionar a pasta onde estão os arquivos: .gitignore e readme.md;
- Especificar a versão do .Net Framework para 5.0, Habilitar o Configure for HTTPS e Enable OpenAPI(support para possibilitar o uso do swagger para ajudar na documentação da API).

Detalhamento das pastas do projeto:
- Em propriedades do projeto eCommerce.API, em depurar é especificado as configurações de inicialização do projeto através da variável de ambiente Development, Production etc que variam de acordo com o arquivo appsettings.json que contém inicialmente o appsettings.Development.json.
- Controllers é a pasta onde conterá as requisições para o projeto.
- Inicialmente foi criado a pasta Models que já é uma convenção adotada na maioria dos projetos MVC e arrastado o arquivo de exemplo WeatherForecast.cs para a mesma executado projeto para visualizarmos o swagger no browser.

Swagger é um interface gráfica, instalada durante a criação do projeto, que permite verificarmos as operações que a API fornece.

Criando as classes:
- Criar a classe Usuario.cs
- Criar o controlador para o Usuario( Novo Controller, API, API Controller-Empty) com o nome de UsuariosController.cs
- Em eCommerce.API criar uma nova pasta com o nome de Repositories.
- Dentro de Repositories criar uma nova classe com o nome de UsuarioRepository.cs
- Criar um nova pasta com o nome de Interfaces;
- Dentro de Interfaces criar uma nova Interface com o nome de IUsuarioRepository.cs que conterá os métodos a serem implementados na classe UsuarioRepository.cs;
- Dentro da classe UsuarioRepository.cs implementar a interface IUsuarioRepository.cs  e fazer os métodos necessários para adicionar, criar, atualizar e remover.
- Dentro da pasta Controller cria o controller API/ API Controller - Empty com o nome de UsuariosController.cs
- Dentro da classe UsuariosController.cs criar os métodos: get, post, put e delete.

- Executar a api e fazer os testes com o Swagger fazendo o Get, Post, Put e Delete.

POSTMAN:
- Iniciar o Postman e ao clicar em Workspaces criar um novo workspace;
- Informar nome, descrição e visibilidade;
- Clicar em Collections e adicionar a coleção com nom de API Dapper;
- Clicar com botão direito em cima de API Dapper e selecionar New Folder;
- Clicar em cima de New Folder e renomear com o nome da entidades: exemplo Usuários.
- Dentro de Usuários clicar com o botão direito e pedir para criar novas requisições, uma por vez, sendo elas: Pegar Usuários(Get), Pegar Usuário(Get(id)), Cadastrar Usuário(Post), Atualizar Usuários(Put) e Deletar Usuário(Delete).
- Clicar em cima de cada requisição e renomear conforme os nomes acima;
- Clicar em enviroments na lateral esquerda, nomear como Development e adicionar uma nova variável de ambiente com o nome de url e o valor inicial deverá conter a url para acessar o BackEnd(https://localhost:44372)
- Clicar no botão Save ou CTRL + S;

- Para fazer o GET(Pegar Usuários), onde está escrito No Environment alterar para o ambiente Development criado que contém a url do backend;
- Na frente do GET digitar: {{url}}/api/Usuarios e clicar em Send, o resultado será status 200 OK, retornando todos os Usuários cadastradoss no backend. 
ATENÇÃO: {{url}} é a variável criada em Environments, /api/Usuarios é o caminho contido na controllers de usuários.


- Para fazer o GET(Pegar apenas um Usuário), onde está escrito No Environment alterar para o ambiente Development criado que contém a url do backend;
- Na frente do GET digitar: {{url}}/api/Usuarios/2 e clicar em Send, o resultado será status 200 OK, retornando o Usuário cadastrado no backend. 
ATENÇÃO: {{url}} é a variável criada em Environments, /api/Usuarios é o caminho contido na controllers de usuários e /2 é o id do usuário.

- Para fazer o POST(Cadastrar Usuários), onde está escrito No Environment alterar para o ambiente Development criado que contém a url do backend;
- Na frente do POST digitar: {{url}}/api/Usuarios
- Clicar na guia Body pois no BackEnd está configurado com [FromBody]
- Selecionar raw,  e optar pelo formato JSON;
- Criar um json de inserção, clicar em Salvar e executar o Send:
```json
{
    "Nome": "Joana Ribeiro",
    "Email": "joana.ribeiro@gmail.com"
}
```

- Para fazer o PUT(Atualizar Usuários), onde está escrito No Environment alterar para o ambiente Development criado que contém a url do backend;
- Na frente do PUT digitar: {{url}}/api/Usuarios
- Clicar na guia Body pois no BackEnd está configurado com [FromBody]
- Selecionar raw,  e optar pelo formato JSON;
- Criar um json de atualização, clicar em Salvar e executar o Send:
```json
{
    "Id": "2",    	
    "Nome": "Josefina Ribeiro",
    "Email": "josefina.ribeiro@gmail.com"
}
```

- Para fazer o DELETE(Eliminar apenas um Usuário), onde está escrito No Environment alterar para o ambiente Development criado que contém a url do backend;
- Na frente do DELETE digitar: {{url}}/api/Usuarios/2, Salvar e clicar em Send, o resultado será status 200 OK, eliminando o Usuário cadastrado no backend. 
ATENÇÃO: {{url}} é a variável criada em Environments, /api/Usuarios é o caminho contido na controllers de usuários e /2 é o id do usuário.


DAPPER
Criando o banco de dados:
- View/ Sql Server Object Explorer
- Expandir e clicar com botão direito em Databases, add new database e cololar o nome do banco de dados com eCommerce.
- O banco está localizado na pasta do usuário local exemplo(C:\Users\gieds\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb)

- Para execução de querys clicar com botão direito no banco e acessar new query.
- Foi executado scripts para criar as tabelas e informações do bando de dados de exemplo.

Criando o modelos de acordo com as tabelas.
- Criar as classes Contato, Departamento e EnderecoEntrega conforme a estrutura de colunas criado no banco de dados.
- Inserir os relacionamentos(one to one, one to many e many to many) que serão baseados em composições e coleções.

Instalando o Dapper
- Clicar com o botão direito em Denpendecias/Nuget Package Manager
- Procurar por Dapper e instalar o pacote: Dapper por Sam Saffron escolher a versão mais recente.
- Utilizar a classe SqlConnection e instalar o pacote: System.Data.Sql.Client.
- Para pegar a string de conexão é só clicar em botão direito no banco de dados, escolher propriedades e localizar o item connection string que contém a string de conexão do banco de dados.
- Na classe de exemplo UsuarioRepository.cs adicionar a biblioteca Dapper para extender as funcionalidades do IDbConnnerction.
- Realizar as alterações necessárias para os Selects, Inserts, Updates e Deletes.

--------------------------

Dapper.Contrib
Biblioteca para trabalhar com apenas uma tabela não podendo efetuar a execução de vários Joins complexos.
Utilizado para fazer um CRUD simples.

Instalando o Dapper.Contrib
- Clicar com o botão direito em Denpendecias/Nuget Package Manager
- Procurar por Dapper.Contrib e instalar o pacote: Dapper.Contrib por Sam Saffron escolher a versão mais recente.

 



Sites Importantes: 
- Para facilitar podemos utilizar o site que contém a maior parte das urls de conexões de diversos tipos de bancos: 
> https://www.connectionstrings.com/


Atalhos:
- CTRL + . --> Utiliza o interlicense para verifcar as sugestões de adição de classes relacionadas;
- prop + tab + tab --> gera um escopo de declaração de métodos get e set;
- ctor + tab + tab --> gera um construtor para a classe;
- CTRL + R --> Executar duas vezes para renomear vários intes de forma mais rápida.
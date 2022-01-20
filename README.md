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


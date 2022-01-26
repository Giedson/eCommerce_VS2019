using eCommerce.API.Interfaces; // Adicionado para poder usar as interfaces.
using eCommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Data; // Adicionado para poder usar o IDbConnection
using System.Data.SqlClient;
using System.Linq;
using Dapper; // Adcionando o Dapper para estender a comunicação com o Banco de Dados da variável _connection.
using System.Threading.Tasks;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;
        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        // Conexão com Dapper: Micro-ORM.
        public List<Usuario> Get()
        {
            // Indica que irá executar uma query cujo terá um retorno do tipo Usuario e força retornar uma lista.
            // return _connection.Query<Usuario, Contato, Usuario>("SELECT * FROM Usuarios U LEFT JOIN Contatos C ON C.UsuarioId = U.Id", (usuario, contato) =>

            // Criando a lista de usuários para evitar a repetição dos múltiplos relacionamentos.
            // Obs.: Preferencialmente utilizar o Dictionary ao invés de lista.
            List<Usuario> usuarios = new List<Usuario>();

            // Atenção como UsuariosDepartamentos não está sendo Mapeado, deve ser desconsiderado da query no retorno da informação.
            string sql = "SELECT U.*, C.*, EE.*, D.* FROM Usuarios U " +
                "LEFT JOIN Contatos C ON C.UsuarioId = U.Id " +
                "LEFT JOIN EnderecosEntrega EE ON EE.UsuarioId = U.Id " +
                "LEFT JOIN UsuariosDepartamentos UD ON UD.UsuarioId = U.Id " +
                "LEFT JOIN Departamentos D ON UD.DepartamentoId = D.Id";

            // Mapeando as tabelas com o Dapper.
            _connection.Query<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(sql, (usuario, contato, enderecoEntrega, departamento) =>
            {
                //ATENÇÃO: a execução abaixo é efetuada uma vez para cada linha de retorno da consulta sql.

                // Verificação do usuário, se está adicionando a primeira vez
                // Necessário pois ao adicionar o Endereço de Entrega na query passou a existir uma duplicidade de Usuários.
                if (usuarios.SingleOrDefault(u => u.Id == usuario.Id) == null)
                {
                    usuario.Departamentos = new List<Departamento>();// Cria uma lista de Departamentos vazia.
                    usuario.EnderecosEntrega = new List<EnderecoEntrega>(); // Cria uma lista de endereços de entrega vazia.
                    usuario.Contato = contato;
                    usuarios.Add(usuario);
                } 
                else
                {
                    usuario = usuarios.SingleOrDefault(u => u.Id == usuario.Id); // Adiciona apenas a referencia.
                }

                // Verificação do Endereço de Entrega. Checa se já existe um endereço de entrega.
                // Necessário pois ao adicionar o Departamento na query passou a existir uma duplicidade de endereços de entrega
                if(usuario.EnderecosEntrega.SingleOrDefault(e => e.Id == enderecoEntrega.Id) == null)
                {
                    usuario.EnderecosEntrega.Add(enderecoEntrega); // Adicionando o endereço de entrega.
                }

                // Adicionando os Departamentos.
                // Necessário pois o Departamento se repete na query para o usuário sendo necessário criar um objeto livre de repetição.
                if (usuario.Departamentos.SingleOrDefault(d => d.Id == departamento.Id) == null)
                {
                    usuario.Departamentos.Add(departamento); // Adicionando o departamento.
                }

                return usuario;
            });

            return usuarios;
        }

        public Usuario Get(int id)
        {
            // Foi utilizado o QuerySingleOrDefault<> pois precisamos retornar apenas um usuário e se não existir retornará o default do tipo Usuario que é null.
            // Foi colocado via parâmetro (Id = @Id", new { Id = id }) - para evitar o sql injection sendo enviado como um objeto anônimo.
            // ATENÇÃO: O nome da propriedade do objeto anônimo deve ser igual ao parâmetro exemplo: Id = @TESTE", new { TESTE = id }
            // Os parâmetros informados em: Query<Usuario, Contato, Usuario> indicam as tabelas relacionadas na query e o ultimo parâmetro o tipo de objeto que deverá ser retornado.
            // É utilizado uma função anônima porque os dados do Contato devem ser adicionados na propriedade contato.


            // Criando a lista de usuários para evitar a repetição dos múltiplos relacionamentos com enderecoEntrega.
            // Obs.: Preferencialmente utilizar o Dictionary ao invés de lista.
            List<Usuario> usuarios = new List<Usuario>();

            // Atenção como UsuariosDepartamentos não está sendo Mapeado, deve ser desconsiderado da query no retorno da informação.
            string sql = "SELECT U.*, C.*, EE.*, D.* FROM Usuarios U " +
                "LEFT JOIN Contatos C ON C.UsuarioId = U.Id " +
                "LEFT JOIN EnderecosEntrega EE ON EE.UsuarioId = U.Id " +
                "LEFT JOIN UsuariosDepartamentos UD ON UD.UsuarioId = U.Id " +
                "LEFT JOIN Departamentos D ON UD.DepartamentoId = D.Id " +
                "WHERE U.Id = @Id";

            // Mapeando as tabelas com o Dapper.
            _connection.Query<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(sql, (usuario, contato, enderecoEntrega, departamento) =>
            {
                //ATENÇÃO: a execução abaixo é efetuada uma vez para cada linha de retorno da consulta sql.

                // Verificação do usuário, se está adicionando a primeira vez
                // Necessário pois ao adicionar o Endereço de Entrega na query passou a existir uma duplicidade de Usuários.
                if (usuarios.SingleOrDefault(u => u.Id == usuario.Id) == null)
                {
                    usuario.Departamentos = new List<Departamento>();// Cria uma lista de Departamentos vazia.
                    usuario.EnderecosEntrega = new List<EnderecoEntrega>(); // Cria uma lista de endereços de entrega vazia.
                    usuario.Contato = contato;
                    usuarios.Add(usuario);
                }
                else
                {
                    usuario = usuarios.SingleOrDefault(u => u.Id == usuario.Id); // Adiciona apenas a referencia.
                }

                // Verificação do Endereço de Entrega. Checa se já existe um endereço de entrega.
                // Necessário pois ao adicionar o Departamento na query passou a existir uma duplicidade de endereços de entrega
                if (usuario.EnderecosEntrega.SingleOrDefault(e => e.Id == enderecoEntrega.Id) == null)
                {
                    usuario.EnderecosEntrega.Add(enderecoEntrega); // Adicionando o endereço de entrega.
                }

                // Adicionando os Departamentos.
                // Necessário pois o Departamento se repete na query para o usuário sendo necessário criar um objeto livre de repetição.
                if (usuario.Departamentos.SingleOrDefault(d => d.Id == departamento.Id) == null)
                {
                    usuario.Departamentos.Add(departamento); // Adicionando o departamento.
                }

                return usuario;
            }, new
            {
                Id = id
            });

            return usuarios.SingleOrDefault(); // Retorna apenas um único registro.
        }

        public void Insert(Usuario usuario)
        {
            _connection.Open(); // Abrindo um conexão com o BD
            var transaction = _connection.BeginTransaction(); // Abrindo a transação com o BD

            try
            {
                // Cria o insert com parâmetros a serem passados e usa um select no final para retornar o id da inserção.
                string sql = "Insert Into Usuarios(Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) " +
                    "Values (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); Select CAST(SCOPE_IDENTITY() AS INT);";

                // Poderia ser utilizado um objeto anônimo igual o exemplo: new { Nome = "jose"} etc, mas neste caso é melhor o objeto que já tem as propriedade equivalentes.
                // Faz o retorno do id do usuário inserido.
                // _connection.Query<int>(sqlContato, usuario.Contato, transaction) - Executa a inserção passando a query de inserção, o objeto que contém as informações e a transação que está aberta.
                usuario.Id = _connection.Query<int>(sql, usuario, transaction).Single(); // Retorna o Id do Usuario.

                // Fazendo a inserção do Contato.
                if (usuario.Contato != null)
                {
                    usuario.Contato.UsuarioId = usuario.Id; // Adiciona no objeto o usuário que acabou de ser inserido no banco de dados
                    string sqlContato = "Insert Into Contatos(UsuarioId, Telefone, Celular) Values (@UsuarioId, @Telefone, @Celular); Select CAST(SCOPE_IDENTITY() AS INT);";
                    usuario.Contato.Id = _connection.Query<int>(sqlContato, usuario.Contato, transaction).Single(); // Retorna o Id do Contato
                }

                // Fazendo a inserção do Endereco de Entrega
                if (usuario.EnderecosEntrega != null && usuario.EnderecosEntrega.Count > 0)
                {
                    foreach(var enderecoEntrega in usuario.EnderecosEntrega)
                    {
                        enderecoEntrega.UsuarioId = usuario.Id;
                        string sqlEndereco = "INSERT INTO EnderecosEntrega(UsuarioId, NomeEndereco, CEP, Estado, Cidade, Bairro, Endereco, Numero, Complemento) VALUES (@UsuarioId, @NomeEndereco, @CEP, @Estado, @Cidade, @Bairro, @Endereco, @Numero, @Complemento); Select CAST(SCOPE_IDENTITY() AS INT);";
                        enderecoEntrega.Id = _connection.Query<int>(sqlEndereco, enderecoEntrega, transaction).Single(); // Retorna o Id do EnderecoEntrega.
                    }
                }

                // Fazendo a inserção do Departamento - Apenas os vínculos.
                if (usuario.Departamentos != null && usuario.Departamentos.Count > 0)
                {
                    foreach (var departamento in usuario.Departamentos)
                    {
                        string sqlUsuariosDepartamentos = "INSERT INTO UsuariosDepartamentos(UsuarioId, DepartamentoId) VALUES (@UsuarioId, @DepartamentoId)";
                        // Não retorna o Id pois a tabela UsuariosDepartamentos não está sendo mapeada no Dapper.
                        // Portanto pode ser apenas executado.
                        _connection.Execute(sqlUsuariosDepartamentos, new { UsuarioId = usuario.Id, DepartamentoId = departamento.Id}, transaction); 
                    }
                }


                transaction.Commit(); // Se as duas inserções transações ocorrerem com sucesso efetuamos o commit no banco de dados.

            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback(); // se ocorrer algum tipo de problema. Está dentro de um try catch pois rollback pode gerar uma excessao
                }
                catch (Exception e)
                {
                    throw e;
                }

                throw ex;

            }
            finally
            {
                _connection.Close(); // Força fechar a conexão sempre.
            }


        }

        public void Update(Usuario usuario)
        {
            _connection.Open(); // Abrindo um conexão com o BD
            var transaction = _connection.BeginTransaction(); // Abrindo a transação com o BD

            try
            {
                // Atualizando apenas o usuário
                string sql = "Update Usuarios Set Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro" +
                    " Where Usuarios.Id = @Id";
                // _connection.Query<int>(sql, usuario, transaction) - Executa a inserção passando a query de inserção, o objeto que contém as informações e a transação que está aberta.
                _connection.Execute(sql, usuario, transaction);

                if (usuario.Contato != null)
                {
                    // Atualizando apenas o contato.
                    string sqlContato = "Update Contatos Set Telefone = @Telefone, Celular = @Celular Where Id = @Id";
                    // _connection.Query<int>(sqlContato, usuario.Contato, transaction) - Executa a inserção passando a query de inserção, o objeto que contém as informações e a transação que está aberta.
                    _connection.Execute(sqlContato, usuario.Contato, transaction);
                }

                // Por se tratar de um relacionamento muitos para muitos foi optado por eliminar as informações.
                string sqlDeletarEnderecosEntrega = "DELETE FROM EnderecosEntrega WHERE UsuarioId = @Id";
                _connection.Execute(sqlDeletarEnderecosEntrega, usuario, transaction);

                // Fazendo novamente a inserção do Endereco de Entrega
                if (usuario.EnderecosEntrega != null && usuario.EnderecosEntrega.Count > 0)
                {
                    foreach (var enderecoEntrega in usuario.EnderecosEntrega)
                    {
                        enderecoEntrega.UsuarioId = usuario.Id;
                        string sqlEndereco = "INSERT INTO EnderecosEntrega(UsuarioId, NomeEndereco, CEP, Estado, Cidade, Bairro, Endereco, Numero, Complemento) VALUES (@UsuarioId, @NomeEndereco, @CEP, @Estado, @Cidade, @Bairro, @Endereco, @Numero, @Complemento); Select CAST(SCOPE_IDENTITY() AS INT);";
                        enderecoEntrega.Id = _connection.Query<int>(sqlEndereco, enderecoEntrega, transaction).Single(); // Retorna o Id do EnderecoEntrega.
                    }
                }

                // Por se tratar de um relacionamento muitos para muitos foi optado por eliminar as informações.
                string sqlDeletarUsuariosDepartamentos = "DELETE FROM UsuariosDepartamentos WHERE UsuarioId = @Id";
                _connection.Execute(sqlDeletarUsuariosDepartamentos, usuario, transaction);

                // Fazendo a inserção do Departamento - Apenas os vínculos.
                if (usuario.Departamentos != null && usuario.Departamentos.Count > 0)
                {
                    foreach (var departamento in usuario.Departamentos)
                    {
                        string sqlUsuariosDepartamentos = "INSERT INTO UsuariosDepartamentos(UsuarioId, DepartamentoId) VALUES (@UsuarioId, @DepartamentoId)";
                        // Não retorna o Id pois a tabela UsuariosDepartamentos não está sendo mapeada no Dapper.
                        // Portanto pode ser apenas executado.
                        _connection.Execute(sqlUsuariosDepartamentos, new { UsuarioId = usuario.Id, DepartamentoId = departamento.Id }, transaction);
                    }
                }

                transaction.Commit(); // Se as duas inserções transações ocorrerem com sucesso efetuamos o commit no banco de dados.
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback(); // Dentro de um try catch pois rollback pode gerar uma excessao
                }
                catch (Exception e)
                {
                    throw e;
                }
                throw ex;
            }
            finally
            {
                _connection.Close(); // // Força fechar a conexão sempre.
            }


        }

        public void Delete(int id)
        {
            // Como as FKs foram criadas com ON DELETE CASCADE os registros filhos serão também eliminados
            // Isto significa que ao Eliminar o usuário por exemplo o seu contato também será excluído automaticamente.
            // Sem On Delete Cascade teríamos que fazer:
            // Abrir a transação, eliminar primeiramente os registros filhos(fks) e por último eliminar o registro Pai.
            _connection.Execute("Delete from Usuarios Where Usuarios.Id = @Id", new { Id = id });
        }
    }
}

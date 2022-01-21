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
            return _connection.Query<Usuario>("Select * from Usuarios").ToList();
        }

        public Usuario Get(int id)
        {
            // Foi utilizado o QuerySingleOrDefault<> pois precisamos retornar apenas um usuário e se não existir retornará o default do tipo Usuario que é null.
            // Foi colocado via parâmetro (Id = @Id", new { Id = id }) - para evitar o sql injection sendo enviado como um objeto anônimo.
            // ATENÇÃO: O nome da propriedade do objeto anônimo deve ser igual ao parâmetro exemplo: Id = @TESTE", new { TESTE = id }
            return _connection.QuerySingleOrDefault<Usuario>("Select * from Usuarios Where Usuarios.Id = @Id", new { Id = id });
        }

        public void Insert(Usuario usuario)
        {
            // Cria o insert com parâmetros a serem passados e usa um select no final para retornar o id da inserção.
            string sql = "Insert Into Usuarios(Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) " +
                "Values (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); Select CAST(SCOPE_IDENTITY() AS INT);";

            // Poderia ser utilizado um objeto anônimo igual o exemplo: new { Nome = "jose"} etc, mas neste caso é melhor o objeto que já tem as propriedade equivalentes.
            // Faz o retorno do id do usuário inserido.
            usuario.Id = _connection.Query<int>(sql, usuario).Single();
        }

        public void Update(Usuario usuario)
        {
            string sql = "Update Usuarios Set Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro" +
                " Where Usuarios.Id = @Id";

            _connection.Execute(sql, usuario);
        }

        public void Delete(int id)
        {
            _connection.Execute("Delete from Usuarios Where Usuarios.Id = @Id", new { Id = id });
        }
    }
}

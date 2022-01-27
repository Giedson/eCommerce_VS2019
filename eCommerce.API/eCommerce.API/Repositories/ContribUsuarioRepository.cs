using eCommerce.API.Interfaces;
using eCommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Data; // Adicionado para poder usar o IDbConnection
using System.Data.SqlClient; // Adicionado para poder usar o SqlConnection
using Dapper.Contrib.Extensions; //  Adicionado para poder extender as opções de IDbConnection.
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Repositories
{
    public class ContribUsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;
        public ContribUsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public List<Usuario> Get()
        {
            return _connection.GetAll<Usuario>().ToList();
        }

        public Usuario Get(int id)
        {
            return _connection.Get<Usuario>(id);
        }

        public void Insert(Usuario usuario)
        {
            // Tendo em vista que o Insert já retorna um long com o Identity da inserção não é necessário fazer um select para sobrecarregar.
            usuario.Id = Convert.ToInt32(_connection.Insert<Usuario>(usuario)); 
        }

        public void Update(Usuario usuario)
        {
            _connection.Update<Usuario>(usuario);
        }
        public void Delete(int id)
        {
            var usuario = Get(id); // Chamando o Get da Classe que retorna o objeto Ususario.
            _connection.Delete<Usuario>(usuario);
        }
    }
}

using eCommerce.API.Interfaces; // Adicionado para poder usar as interfaces.
using eCommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {


        // Criando um db Fake.
        private static List<Usuario> _db = new List<Usuario>()
        {
            new Usuario(){ Id = 1, Nome = "Felipe Rodrigues", Email = "felipe.rodrigues@gmail.com" },
            new Usuario() { Id = 2, Nome = "Marcelo Rodrigues", Email = "marcelo.rodrigues@gmail.com" },
            new Usuario() { Id = 3, Nome = "Jessica Rodrigues", Email = "jessica.rodrigues@gmail.com"}
        };

        public List<Usuario> Get()
        {
            return _db;
        }

        public Usuario Get(int id)
        {
            return _db.FirstOrDefault(u => u.Id == id); // retornando um usuario com o Linq
        }

        public void Insert(Usuario usuario)
        {
            var ultimoUsuario = _db.LastOrDefault();

            // criando ids para novos usuarios.
            if (ultimoUsuario == null)
                usuario.Id = 1;
            else
                usuario.Id = ultimoUsuario.Id + 1;


            _db.Add(usuario); // adicionando um novo usuario
        }

        public void Update(Usuario usuario)
        {
            _db.Remove(_db.FirstOrDefault(u => u.Id == usuario.Id)); // removendo o usuario 
            _db.Add(usuario);
        }

        public void Delete(int id)
        {
            _db.Remove(_db.FirstOrDefault(u => u.Id == id)); // removendo o usuario por id.
        }
    }
}

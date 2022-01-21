using eCommerce.API.Models; // Adicionado para poder usar a classe Usuario.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Interfaces
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Get();
        public Usuario Get(int id);
        public void Insert(Usuario usuario);
        public void Update(Usuario usuario);
        public void Delete(int id);  
    }
}

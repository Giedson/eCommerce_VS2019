using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        // Representa uma Composição. Indica o relacionamento(um pra um), isto é, um contato percente a um usuário.
        public Usuario Usuario { get; set; }
    }
}

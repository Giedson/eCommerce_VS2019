using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //Many-To-Many - Um departamento tem vários usuários e um usuario tem vários departamentos.
        //Este tipo de relacionamento foi possível sem a criação da classe UsuariosDepartamentos porque
        //a tabela UsuariosDepartamentos possui apenas a pk, chave fk do usuario e chave fk do departamento
        //se houve-se qualquer outro tipo de campo teria se ser criada uma nova classe com o nome UsuariosDepartamentos e fazer de forma diferente.
        public ICollection<Usuario> Usuarios { get; set; }
    }
}

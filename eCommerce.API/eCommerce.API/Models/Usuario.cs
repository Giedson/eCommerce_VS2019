using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string NomeMae { get; set; }
        public string SituacaoCadastro { get; set; }
        public DateTimeOffset DataCadastro { get; set; } // Recomendado quando se tratar de um banco de dados pois utiliza o fuso horário.

        //One-To-One - Representa uma Composição. Indica o relacionamento(um pra um) do usuário com o contato, isto é, um usuário possui um contato.
        public Contato Contato { get; set; }

        //One-To-Many - Um usuário tem vários endereços podendo ser representado por uma coleção de endereços.Indica o relacionamento(um pra muitos)
        public ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }

        //Many-To-Many - Um usuário tem vários departamentos e um departamento tem vários usuários.
        //Este tipo de relacionamento foi possível sem a criação da classe UsuariosDepartamentos porque
        //a tabela UsuariosDepartamentos possui apenas a pk, chave fk do usuario e chave fk do departamento
        //se houve qualquer outro tipo de campo teria se ser criada uma nova classe com o nome UsuariosDepartamentos.
        public ICollection<Departamento> Departamentos { get; set; }
    }
}

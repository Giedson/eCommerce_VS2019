using eCommerce.API.Interfaces; // Adicionado para poder usar a interface IUsuarioRepository
using eCommerce.API.Models;
using eCommerce.API.Repositories; // Adicionado para poder usar a classe UsuarioRepository
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * CRUD
 * - GET -> Obter a lista de usuários.
 * - GET -> Obter o usuário passando o Id.
 * - POST -> Inserir ou cadastrar um usuário.
 * - PUT -> Atualizar um usuário.
 * - DELETE -> Remover um usuário.
 * 
 * METHOD HTTP: 
 * www.minhaapi.com.br/api/Usuarios (Via Get retorna todos usuários)
 * www.minhaapi.com.br/api/Usuarios/ 2  (Via Get retorna apenas o usuário 2)
 * 
 * Código de retorno HTTP por faixa: 
 * 200 - OK
 * 300 - Redirecionamento para outro endereço.
 * 400 - Erro em relação aos dados enviados por parte do usuário. Exemplo 404 (página não encontrada)
 * 500 - Erro quando está associado ao servidor.
 * 
 * [FromBody] - Data Annotation Indica que usuário virá de um local específico da requisição que contém um corpo.
 */


namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _repository;

        public UsuariosController()
        {
            _repository = new UsuarioRepository();          
        }

        // Retornando todos os usuários com IActionResult que retorna vários tipos até uma página html por exemplo.
        [HttpGet] 
        public IActionResult Get()
        {
            // Utilizando o método Ok para transformar o objeto em JSON e colocando em formato aceito por IActionResult.
            return Ok(_repository.Get()); // Retornará HTTP - 200(OK)
        }

        // Retornando apenas um usuário
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Usuario usuario = _repository.Get(id);

            if(usuario != null) 
                return Ok(usuario);

            return NotFound(); // ERRO HTTP: 404 - Página não encontrada.
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Usuario usuario)
        {
            _repository.Insert(usuario);

            return Ok(usuario);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            _repository.Update(usuario);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);

            return Ok();
        }

    }
}

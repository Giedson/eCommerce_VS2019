using eCommerce.API.Interfaces;
using eCommerce.API.Models;
using eCommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Controllers
{
    [Route("api/Contrib/Usuarios")]
    [ApiController]
    public class ContribUsuariosController : ControllerBase
    {
        private IUsuarioRepository _repository;

        public ContribUsuariosController()
        {
            _repository = new ContribUsuarioRepository();
        }

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

            if (usuario != null)
                return Ok(usuario);

            return NotFound(); // ERRO HTTP: 404 - Página não encontrada.
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Usuario usuario)
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
            Usuario usuario = _repository.Get(id);

            if (usuario != null)
            {
                _repository.Delete(id);

                return Ok(new
                {
                    message = $"Usuario {usuario.Nome} excluído com sucesso."
                });
            }

            return BadRequest();
        }
    }
}

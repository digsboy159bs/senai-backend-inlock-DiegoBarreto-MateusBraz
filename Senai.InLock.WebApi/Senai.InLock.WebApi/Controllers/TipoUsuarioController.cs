using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipousuarioRepository { get; set; }
        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public TipoUsuarioController()
        {
            _tipousuarioRepository = new TipoUsuarioRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            // Faz a chamada para o método .Listar()
            // Retorna a lista e um status code 200 - Ok
            return Ok(_tipousuarioRepository.Listar());
        }
        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain novoTipoUsuario)
        {
            // Faz a chamada para o método .Cadastrar();
            _tipousuarioRepository.Cadastrar(novoTipoUsuario);
            // Retorna o status code 201 - Created com a URI e o objeto cadastrado
            return Created("http://localhost:5000/api/Jogos", novoTipoUsuario);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto tipousuarioBuscado que irá receber o funcionário buscado no banco de dados
            TipoUsuarioDomain tipousuarioBuscado = _tipousuarioRepository.BuscarPorId(id);
            // Verifica se algum funcionário foi encontrado
            if (tipousuarioBuscado != null)
            {
                // Caso seja, retorna os dados buscados e um status code 200 - Ok
                return Ok(tipousuarioBuscado);
            }
            // Caso não seja, retorna um status code 404 - NotFound com a mensagem
            return NotFound("Nenhum Estudio encontrado para o identificador informado");
        }
    }
}

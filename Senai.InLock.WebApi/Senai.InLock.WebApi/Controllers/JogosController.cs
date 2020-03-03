using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Controllers
{
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class JogosController : ControllerBase
    {
        private IJogosRepository _jogoRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public JogosController()
        {
            _jogoRepository = new JogosRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Faz a chamada para o método .Listar()
            // Retorna a lista e um status code 200 - Ok
            return Ok(_jogoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto jogoBuscado que irá receber o funcionário buscado no banco de dados
            JogosDomain jogoBuscado = _jogoRepository.BuscarPorId(id);

            // Verifica se algum funcionário foi encontrado
            if (jogoBuscado != null)
            {
                // Caso seja, retorna os dados buscados e um status code 200 - Ok
                return Ok(jogoBuscado);
            }

            // Caso não seja, retorna um status code 404 - NotFound com a mensagem
            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, JogosDomain jogoAtualizado)
        {
            // Cria um objeto jogoBuscado que irá receber o funcionário buscado no banco de dados
            JogosDomain jogoBuscado = _jogoRepository.BuscarPorId(id);

            // Verifica se algum funcionário foi encontrado
            if (jogoBuscado != null)
            {
                // Tenta atualizar o registro
                try
                {
                    // Faz a chamada para o método .Atualizar();
                    _jogoRepository.Atualizar(id, jogoAtualizado);

                    // Retorna um status code 204 - No Content
                    return NoContent();
                }
                // Caso ocorra algum erro
                catch (Exception erro)
                {
                    // Retorna BadRequest e o erro
                    return BadRequest(erro);
                }

            }


            // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
            // e um bool para representar que houve erro
            return NotFound
                (
                    new
                    {
                        mensagem = "Jogo não encontrado",
                        erro = true
                    }
                );
        }

        [HttpPost]
        public IActionResult Post(JogosDomain novoJogo)
        {
            // Faz a chamada para o método .Cadastrar();
            _jogoRepository.Cadastrar(novoJogo);
            // Retorna o status code 201 - Created com a URI e o objeto cadastrado
            return Created("http://localhost:5000/api/Jogos", novoJogo);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Cria um objeto jogoBuscado que irá receber o funcionário buscado no banco de dados
            JogosDomain jogoBuscado = _jogoRepository.BuscarPorId(id);

            // Verifica se o funcionário foi encontrado
            if (jogoBuscado != null)
            {
                // Caso seja, faz a chamada para o método .Deletar()
                _jogoRepository.Deletar(id);

                // e retorna um status code 200 - Ok com uma mensagem de sucesso
                return Ok($"O jogo {id} foi deletado com sucesso!");
            }

            // Caso não seja, retorna um status code 404 - NotFound com a mensagem
            return NotFound("Nenhum jogo encontrado para o identificador informado");
        }
    }
}

using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IConsultaRepository repo;

        public ConsultasController(IConsultaRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Cadastra uma consulta na aplicação
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Dados da consulta cadastrada</returns>
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {
            try
            {
                var retorno = repo.Inserir(consulta);
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Lista todas as consultas cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repo.ListarTodos();
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Faz a busca de uma consulta pelo ID
        /// </summary>
        /// <param name="id">Busca uma consulta por ID</param>
        /// <returns>Consulta cadastrada</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada" });
                }
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Altera uma consulata na aplicação
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="consulta">Consulta cadastrada</param>
        /// <returns>Consulta alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta consulta)
        {
            try
            {
                // Verificar se os IDs estão de acordo um com o outro
                if (id != consulta.Id)
                {
                    return BadRequest();
                }

                // Verificar se ID existe no banco
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada" });
                }

                // Altera efetivamente o usuário
                repo.Alterar(consulta);
                return NotFound();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Faz mudança parcial na consulta cadastrada
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="patchConsulta">Consulta cadastrada</param>
        /// <returns>Consulta alterada parcialmente</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchConsulta)
        {
            if (patchConsulta == null)
            {
                return BadRequest();
            }

            var consulta = repo.BuscarPorId(id);
            if (consulta == null)
            {
                return NotFound(new { Message = "Consulta não encontrada" });
            }

            repo.AlterarParcialmente(patchConsulta, consulta);
            return Ok(consulta);
        }

        /// <summary>
        /// Exclui uma consulta da aplicação
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <returns>Consulta excluída</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repo.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new { Message = "Consulta não encontrada" });
                }

                repo.Excluir(busca);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }
    }
}

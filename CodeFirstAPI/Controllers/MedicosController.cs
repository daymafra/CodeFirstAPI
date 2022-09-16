using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IMedicoRepository repo;

        public MedicosController(IMedicoRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Cadastra o médico na aplicação
        /// </summary>
        /// <param name="medico">Dados do médico</param>
        /// <returns>Dados do médico cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {
            try
            {
                var retorno = repo.Inserir(medico);
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
        /// Lista todos os médicos cadastrados
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
        /// Faz a busca por ID da clase médico
        /// </summary>
        /// <param name="id">Busca ID</param>
        /// <returns>Médico cadastrado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Médico não encontrado" });
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
        /// Altera o médico na aplicação
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <param name="medico">Médico cadastrado</param>
        /// <returns>Médico alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
        {
            try
            {
                // Verificar se os IDs estão de acordo um com o outro
                if (id != medico.Id)
                {
                    return BadRequest();
                }

                // Verificar se ID existe no banco
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Medico não encontrado" });
                }

                // Altera efetivamente o usuário
                repo.Alterar(medico);
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
        /// Faz uma mudança parcial no médico cadastrado
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <param name="patchMedico">Médico cadastrado</param>
        /// <returns>Médico parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchMedico)
        {
            if (patchMedico == null)
            {
                return BadRequest();
            }

            var medico = repo.BuscarPorId(id);
            if (medico == null)
            {
                return NotFound(new { Message = "Médico não encontrado" });
            }

            repo.AlterarParcialmente(patchMedico, medico);
            return Ok(medico);
        }

        /// <summary>
        /// Exclui médico da aplicação
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <returns>Médico excluído</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repo.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new { Message = "Médico não encontrado" });
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

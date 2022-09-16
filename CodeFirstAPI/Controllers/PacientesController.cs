using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IPacienteRepository repo;

        public PacientesController(IPacienteRepository _repo)
        {
            repo = _repo;
        }
        
        /// <summary>
        /// Cadastra um paciente na aplicação
        /// </summary>
        /// <param name="paciente">Dados do paciente</param>
        /// <returns>Dados do paciente cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Paciente paciente)
        {
            try
            {
                var retorno = repo.Inserir(paciente);
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
        /// Lista todos os paciente cadastrados
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
        /// Faz a busca por ID do paciente
        /// </summary>
        /// <param name="id">Busca ID</param>
        /// <returns>Paciente cadastrado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado" });
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
        /// Altera o paciente na aplicação
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <param name="paciente">Paciente cadastrado</param>
        /// <returns>Paciente alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Paciente paciente)
        {
            try
            {
                // Verificar se os IDs estão de acordo um com o outro
                if (id != paciente.Id)
                {
                    return BadRequest();
                }

                // Verificar se ID existe no banco
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado" });
                }

                // Altera efetivamente o usuário
                repo.Alterar(paciente);
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
        /// Faz aluma mudança parcial no paciente da aplicação
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <param name="patchPaciente">Paciente cadastrado</param>
        /// <returns>Paciente alterado parcialmente</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchPaciente)
        {
            if (patchPaciente == null)
            {
                return BadRequest();
            }

            var paciente = repo.BuscarPorId(id);
            if (paciente == null)
            {
                return NotFound(new { Message = "Paciente não encontrado" });
            }

            repo.AlterarParcialmente(patchPaciente, paciente);
            return Ok(paciente);
        }

        /// <summary>
        /// Exclui paciente cadastrado na aplicação
        /// </summary>
        /// <param name="id">Id do paciente</param>
        /// <returns>Paciente excluído</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repo.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado" });
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

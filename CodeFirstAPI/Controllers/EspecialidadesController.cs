using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IEspecialidadeRepository repo;

        public EspecialidadesController(IEspecialidadeRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Cadastra a especialidade do médico na aplicação
        /// </summary>
        /// <param name="especialidade">Dados da especialidade</param>
        /// <returns>Dados da especialidade cadastrada</returns>
        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {
            try
            {
                var retorno = repo.Inserir(especialidade);
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
        /// Lista todas as especialidades cadastradas
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
        /// Faz a busca de uma especialidade pelo ID
        /// </summary>
        /// <param name="id">Busca especialidade por ID</param>
        /// <returns>Especialidade cadastrada</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada" });
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
        /// Altera uma especialidade médica na aplicação
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <param name="especialidade">Especilidade cadastrada</param>
        /// <returns>Especialidade alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
        {
            try
            {
                // Verificar se os IDs estão de acordo um com o outro
                if (id != especialidade.Id)
                {
                    return BadRequest();
                }

                // Verificar se ID existe no banco
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada" });
                }

                // Altera efetivamente o usuário
                repo.Alterar(especialidade);
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
        /// Faz mudança parcial na especialidade do médico cadastrado
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <param name="patchEspecialidade">Médico cadastrado</param>
        /// <returns>Médico alterado parcialmente</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchEspecialidade)
        {
            if (patchEspecialidade == null)
            {
                return BadRequest();
            }

            var especialidade = repo.BuscarPorId(id);
            if (especialidade == null)
            {
                return NotFound(new { Message = "Especialidade não encontrada" });
            }

            repo.AlterarParcialmente(patchEspecialidade, especialidade);
            return Ok(especialidade);
        }

        /// <summary>
        /// Exclui uma especialidade da aplicação
        /// </summary>
        /// <param name="id">Id da especialidade</param>
        /// <returns>Especialidade excluída</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repo.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new { Message = "Especialidade não encontrada" });
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

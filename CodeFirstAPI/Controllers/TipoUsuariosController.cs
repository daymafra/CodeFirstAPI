using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly ITipoUsuarioRepository repo;

        public TipoUsuariosController(ITipoUsuarioRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Cadastra um tipo de usuário na aplicação (Médico ou Paciente)
        /// </summary>
        /// <param name="tipoUsuario">Dados do tipo de usuário</param>
        /// <returns>Dados do tipo de usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {
            try
            {
                var retorno = repo.Inserir(tipoUsuario);
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
        /// Lista todos os tipos de usuários cadastrados
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
        /// Faz a busca de um tipo de usuário pelo ID
        /// </summary>
        /// <param name="id">Busca tipo de usuário por ID</param>
        /// <returns>Tipo de usuário cadastrado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Tipo de usuário não encontrado" });
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
        /// Altera um tipo de usuário na aplicação
        /// </summary>
        /// <param name="id">Id do tipo de usuário</param>
        /// <param name="tipoUsuario">Tipo de usuário cadastrado</param>
        /// <returns>Tipo de usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, TipoUsuario tipoUsuario)
        {
            try
            {
                // Verificar se os IDs estão de acordo um com o outro
                if (id != tipoUsuario.Id)
                {
                    return BadRequest();
                }

                // Verificar se ID existe no banco
                var retorno = repo.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new { Message = "Tipo de usuário não encontrado" });
                }

                // Altera efetivamente o usuário
                repo.Alterar(tipoUsuario);
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
        /// Faz uma mudança parcial no tipo de usuário cadastrado
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="patchTipoUsuario">Tipo de usuário cadastrado</param>
        /// <returns>Tipo de usuário alterado parcialmente</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchTipoUsuario)
        {
            if (patchTipoUsuario == null)
            {
                return BadRequest();
            }

            var tipoUsuario = repo.BuscarPorId(id);
            if (tipoUsuario == null)
            {
                return NotFound(new { Message = "Tipo de usuário não encontrado" });
            }

            repo.AlterarParcialmente(patchTipoUsuario, tipoUsuario);
            return Ok(tipoUsuario);
        }

        /// <summary>
        /// Exclui um tipo de usuário da aplicação
        /// </summary>
        /// <param name="id">Id do tipo de usuário</param>
        /// <returns>Tipo de usuário excluído</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repo.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new { Message = "Tipo de usuário não encontrado" });
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

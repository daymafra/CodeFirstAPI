using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace CodeFirstAPI.Interface
{
    public interface ITipoUsuarioRepository
    {
        // CRUD
        TipoUsuario Inserir(TipoUsuario tipoUsuario);
        ICollection<TipoUsuario> ListarTodos();
        TipoUsuario BuscarPorId(int id);
        void Alterar(TipoUsuario tipoUsuario);
        void Excluir(TipoUsuario tipoUsuario);
        void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario);
    }
}

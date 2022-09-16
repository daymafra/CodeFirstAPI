using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace CodeFirstAPI.Interface
{
    public interface IUsuarioRepository
    {
        // CRUD
        Usuario Inserir(Usuario usuario);
        ICollection<Usuario> ListarTodos();
        Usuario BuscarPorId(int id);
        void Alterar(Usuario usuario);
        void Excluir(Usuario usuario);
        void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario);
    }
}

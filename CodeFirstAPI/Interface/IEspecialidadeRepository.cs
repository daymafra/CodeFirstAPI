using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace CodeFirstAPI.Interface
{
    public interface IEspecialidadeRepository
    {
        // CRUD
        Especialidade Inserir(Especialidade especialidade);
        ICollection<Especialidade> ListarTodos();
        Especialidade BuscarPorId(int id);
        void Alterar(Especialidade especialidade);
        void Excluir(Especialidade especialidade);
        void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade);
    }
}

using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace CodeFirstAPI.Interface
{
    public interface IConsultaRepository
    {
        // CRUD
        Consulta Inserir(Consulta consulta);
        ICollection<Consulta> ListarTodos();
        Consulta BuscarPorId(int id);
        void Alterar(Consulta consulta);
        void Excluir(Consulta consulta);
        void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consulta);
    }
}

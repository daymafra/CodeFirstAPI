using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace CodeFirstAPI.Interface
{
    public interface IPacienteRepository
    {
        // CRUD
        Paciente Inserir(Paciente paciente);
        ICollection<Paciente> ListarTodos();
        Paciente BuscarPorId(int id);
        void Alterar(Paciente paciente);
        void Excluir(Paciente paciente);
        void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente);
    }
}

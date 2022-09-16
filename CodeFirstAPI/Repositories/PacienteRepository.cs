using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public PacienteRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(Paciente paciente)
        {
            context.Entry(paciente).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchPaciente, Paciente paciente)
        {
            patchPaciente.ApplyTo(paciente);
            context.Entry(paciente).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Paciente BuscarPorId(int id)
        {
            return context.Paciente.Find(id);
        }

        public void Excluir(Paciente paciente)
        {
            context.Paciente.Remove(paciente);
        }

        public Paciente Inserir(Paciente paciente)
        {
            context.Paciente.Add(paciente);
            context.SaveChanges();
            return paciente;
        }

        public ICollection<Paciente> ListarTodos()
        {
            return context.Paciente.ToList();
        }
    }
}

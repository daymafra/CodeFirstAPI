using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public MedicoRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(Medico medico)
        {
            context.Entry(medico).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchMedico, Medico medico)
        {
            patchMedico.ApplyTo(medico);
            context.Entry(medico).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Medico BuscarPorId(int id)
        {
            return context.Medico.Find(id);
        }

        public void Excluir(Medico medico)
        {
            context.Medico.Remove(medico);
        }

        public Medico Inserir(Medico medico)
        {
            context.Medico.Add(medico);
            context.SaveChanges();
            return medico;
        }

        public ICollection<Medico> ListarTodos()
        {
            return context.Medico.ToList();
        }
    }
}

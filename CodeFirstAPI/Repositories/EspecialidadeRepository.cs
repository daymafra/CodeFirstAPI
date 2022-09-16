using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public EspecialidadeRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(Especialidade especialidade)
        {
            context.Entry(especialidade).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchEspecialidade, Especialidade especialidade)
        {
            patchEspecialidade.ApplyTo(especialidade);
            context.Entry(especialidade).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Especialidade BuscarPorId(int id)
        {
            return context.Especialidade.Find(id);
        }

        public void Excluir(Especialidade especialidade)
        {
            context.Especialidade.Remove(especialidade);
        }

        public Especialidade Inserir(Especialidade especialidade)
        {
            context.Especialidade.Add(especialidade);
            context.SaveChanges();
            return especialidade;
        }

        public ICollection<Especialidade> ListarTodos()
        {
            return context.Especialidade.ToList();
        }
    }
}

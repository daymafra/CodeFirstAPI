using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public ConsultaRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(Consulta consulta)
        {
            context.Entry(consulta).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchConsulta, Consulta consulta)
        {
            patchConsulta.ApplyTo(consulta);
            context.Entry(consulta).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Consulta BuscarPorId(int id)
        {
            return context.Consulta.Find(id);
        }

        public void Excluir(Consulta consulta)
        {
            context.Consulta.Remove(consulta);
        }

        public Consulta Inserir(Consulta consulta)
        {
            context.Consulta.Add(consulta);
            context.SaveChanges();
            return consulta;
        }

        public ICollection<Consulta> ListarTodos()
        {
            return context.Consulta.ToList();
        }
    }
}

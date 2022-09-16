using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public TipoUsuarioRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(TipoUsuario tipoUsuario)
        {
            context.Entry(tipoUsuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario)
        {
            patchTipoUsuario.ApplyTo(tipoUsuario);
            context.Entry(tipoUsuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public TipoUsuario BuscarPorId(int id)
        {
            return context.TipoUsuario.Find(id);
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            context.TipoUsuario.Remove(tipoUsuario);
        }

        public TipoUsuario Inserir(TipoUsuario tipoUsuario)
        {
            context.TipoUsuario.Add(tipoUsuario);
            context.SaveChanges();
            return tipoUsuario;
        }

        public ICollection<TipoUsuario> ListarTodos()
        {
            return context.TipoUsuario.ToList();
        }
    }
}

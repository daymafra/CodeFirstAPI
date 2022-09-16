using CodeFirstAPI.Context;
using CodeFirstAPI.Interface;
using CodeFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de Dependência
        CodeFirstContext context;
        public UsuarioRepository(CodeFirstContext _context)
        {
            context = _context;
        }
        public void Alterar(Usuario usuario)
        {
            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AlterarParcialmente(JsonPatchDocument patchUsuario, Usuario usuario)
        {
            patchUsuario.ApplyTo(usuario);
            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            return context.Usuario.Find(id);
        }

        public void Excluir(Usuario usuario)
        {
            context.Usuario.Remove(usuario);
        }

        public Usuario Inserir(Usuario usuario)
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();
            return usuario;
        }

        public ICollection<Usuario> ListarTodos()
        {
            return context.Usuario.ToList();
        }
    }
}

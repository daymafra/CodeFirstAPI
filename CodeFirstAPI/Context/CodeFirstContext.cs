using CodeFirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CodeFirstAPI.Context
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
    }
}

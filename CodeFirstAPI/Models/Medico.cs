using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstAPI.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o CRM do médico")]
        public string CRM { get; set; }

        [ForeignKey("Especialidade")]
        public int IdEspecialidade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Especialidade Especialidade { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Consulta> Consultas { get; set; }
    }
}

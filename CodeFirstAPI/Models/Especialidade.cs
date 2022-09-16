using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeFirstAPI.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a categoria da especialidade do médico")]
        public string Categoria { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Medico> Medicos { get; set; }
    }
}

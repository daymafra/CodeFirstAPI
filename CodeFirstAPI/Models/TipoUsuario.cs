using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeFirstAPI.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe se o usuário é médico ou paciente")]
        public string Tipo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<Usuario> Usuarios { get; set; }
    }
}

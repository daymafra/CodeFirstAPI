using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstAPI.Models
{
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataHora { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Medico Medico { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Paciente Paciente { get; set; }
    }
}

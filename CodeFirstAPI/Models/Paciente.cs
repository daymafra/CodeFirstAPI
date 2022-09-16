using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstAPI.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o número da carteirinha do paciente")]
        public string Carteirinha { get; set; }
        [Required(ErrorMessage = "Informe a data de nascimento do paciente")]
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Consulta> Consultas { get; set; }
    }
}

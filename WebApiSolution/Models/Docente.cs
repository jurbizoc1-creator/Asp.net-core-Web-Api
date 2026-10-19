using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApiSolution.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50)]
        public string Apellidos { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}



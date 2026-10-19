using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace WebApiSolution.Models
{
    public class Estudiante
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(50)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage ="El apellido es requerido")]
        [StringLength(50)]
        public string Apellidos { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Direccion { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}



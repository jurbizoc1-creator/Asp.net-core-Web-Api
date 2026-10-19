using System.Text.Json.Serialization;

using System.ComponentModel.DataAnnotations;
namespace WebApiSolution.Models
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "El nombre del curso es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}



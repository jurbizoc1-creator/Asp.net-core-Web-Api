using System.ComponentModel.DataAnnotations;
namespace WebApiSolution.Models


{
    public class Asignacion
    {
        [Key]
        public int AsignacionId { get; set; }

        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public int DocenteId { get; set; }
        public Estudiante? Estudiante { get; set; }
        public Curso? Curso { get; set; }
        public Docente? Docente { get; set; }
    }
}



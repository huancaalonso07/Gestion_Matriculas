namespace GestionMatriuclasAPI.DTOs
{
    public class MatriculaDto
    {
        public int IdMatricula { get; set; }
        public int IdEstudiante { get; set; } 
        public int IdCurso { get; set; }
        public string NombreEstudiante { get; set; }
        public string NombreCurso { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Estado { get; set; }
    }
}

namespace GestionMatriuclasAPI.DTOs
{
    public class MatriculaCreateDto
    {
        public int IdEstudiante {  get; set; }
        public int IdCurso { get; set; }
        public DateTime FechaMatricula { get; set; }
    }
}

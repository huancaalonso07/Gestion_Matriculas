using System;
using System.Collections.Generic;

namespace GestionMatriuclasAPI.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}

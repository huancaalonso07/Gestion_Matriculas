using System;
using System.Collections.Generic;

namespace GestionMatriuclasAPI.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string NombreCurso { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}

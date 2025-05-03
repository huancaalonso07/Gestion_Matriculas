using GestionMatriuclasAPI.Controllers;
using GestionMatriuclasAPI.DTOs;
using GestionMatriuclasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionMatriuclasAPI.Repository
{
    public class MatriculaRepository : IMatriculaRepository
    {

        private readonly BdMatriculasContext _context;

        public MatriculaRepository(BdMatriculasContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(MatriculaCreateDto dto)
        {
            if (await Exists(dto.IdEstudiante, dto.IdCurso))
                throw new MatriculaException("El estudiante ya está matriculado en este curso.");

            if (dto.FechaMatricula > DateTime.Now)
                throw new MatriculaException("La fecha de matrícula no puede ser futura.");

            var matricula = new Matricula
            {
                IdEstudiante = dto.IdEstudiante,
                IdCurso = dto.IdCurso,
                FechaMatricula = dto.FechaMatricula,
                Estado = "Activa"
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null || matricula.Estado != "Cancelada") return false;

            _context.Matriculas.Remove(matricula);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Exists(int idEstudiante, int idCurso)
        {
            return await _context.Matriculas.AnyAsync(m => m.IdEstudiante == idEstudiante && m.IdCurso == idCurso);
        }

        public async Task<IEnumerable<MatriculaDto>> GetAll()
        {
            return await _context.Matriculas
            .Include(m => m.IdEstudianteNavigation)
            .Include(m => m.IdCursoNavigation)
            .Select(m => new MatriculaDto
            {
                IdMatricula = m.IdMatricula,
                IdEstudiante = m.IdEstudiante, 
                IdCurso = m.IdCurso,           
                NombreEstudiante = m.IdEstudianteNavigation.Nombres + " " + m.IdEstudianteNavigation.Apellidos,
                NombreCurso = m.IdCursoNavigation.NombreCurso,
                FechaMatricula = m.FechaMatricula,
                Estado = m.Estado
             }).ToListAsync();
        }

        public async Task<IEnumerable<MatriculaDto>> GetByCurso(int idCurso)
        {
            var resultado = await _context
            .Set<MatriculaDto>()
            .FromSqlRaw("EXEC sp_listar_matriculas_por_curso @p0", idCurso)
            .ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<MatriculaDto>> GetByEstado(string estado)
        {
            var resultado = await _context
            .Set<MatriculaDto>()
            .FromSqlRaw("EXEC sp_listar_matriculas_por_estado @p0", estado)
            .ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<MatriculaDto>> GetByEstudiante(int idEstudiante)
        {
            var resultado = await _context
            .Set<MatriculaDto>()
            .FromSqlRaw("EXEC sp_listar_matriculas_por_estudiante @p0", idEstudiante)
            .ToListAsync();

            return resultado;
        }

        public async Task<MatriculaDto?> GetById(int id)
        {
            var m = await _context.Matriculas
            .Include(m => m.IdEstudianteNavigation)
            .Include(m => m.IdCursoNavigation)
            .FirstOrDefaultAsync(m => m.IdMatricula == id);

            if (m == null) return null;

            return new MatriculaDto
            {
                IdMatricula = m.IdMatricula,
                NombreEstudiante = m.IdEstudianteNavigation.Nombres + " " + m.IdEstudianteNavigation.Apellidos,
                NombreCurso = m.IdCursoNavigation.NombreCurso,
                FechaMatricula = m.FechaMatricula,
                Estado = m.Estado
            };
        }

        public async Task<bool> UpdateEstado(int id, string nuevoEstado)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null) return false;

            if (matricula.Estado == "Finalizada" && nuevoEstado == "Cancelada") return false;

            matricula.Estado = nuevoEstado;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

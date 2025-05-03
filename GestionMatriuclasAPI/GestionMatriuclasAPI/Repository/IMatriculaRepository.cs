using GestionMatriuclasAPI.DTOs;

namespace GestionMatriuclasAPI.Repository
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<MatriculaDto>> GetAll();
        Task<MatriculaDto?> GetById(int id);
        Task<IEnumerable<MatriculaDto>> GetByEstudiante(int idEstudiante);
        Task<IEnumerable<MatriculaDto>> GetByCurso(int idCurso);
        Task<IEnumerable<MatriculaDto>> GetByEstado(string estado);
        Task<bool> Exists(int idEstudiante, int idCurso);
        Task<bool> Create(MatriculaCreateDto dto);
        Task<bool> UpdateEstado(int id, string nuevoEstado);
        Task<bool> Delete(int id);
    }
}

using GestionMatriuclasAPI.DTOs;
using GestionMatriuclasAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GestionMatriuclasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaRepository _repo;

        public MatriculaController(IMatriculaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        
        [HttpGet("curso/{idCurso}")]
        public async Task<IActionResult> GetByCurso(int idCurso)
        {
            try
            {
                var matrículas = await _repo.GetByCurso(idCurso);
                return Ok(matrículas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        
        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<IActionResult> GetByEstudiante(int idEstudiante)
        {
            try
            {
                var matrículas = await _repo.GetByEstudiante(idEstudiante);
                return Ok(matrículas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        
        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            try
            {
                var matrículas = await _repo.GetByEstado(estado);
                return Ok(matrículas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatriculaCreateDto dto)
        {
            try
            {
                var result = await _repo.Create(dto);
                return Ok("Matrícula creada exitosamente.");
            }
            catch (MatriculaException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}"); 
            }
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] MatriculaUpdateDto dto)
        {
            try
            {
                var result = await _repo.UpdateEstado(id, dto.Estado);
                return result ? Ok("Estado actualizado correctamente.") : BadRequest("No se pudo actualizar el estado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _repo.Delete(id);
                return result ? Ok("Matrícula eliminada exitosamente.") : BadRequest("No se pudo eliminar la matrícula.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}

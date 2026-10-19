using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSolution.Contexts;
using WebApiSolution.Models;

namespace WebApiSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsignacionesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AsignacionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignaciones()
    {
        return await _context.Asignaciones
            .AsNoTracking()
            .Include(a => a.Estudiante)
            .Include(a => a.Curso)
            .Include(a => a.Docente)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
    {
        var asignacion = await _context.Asignaciones
            .AsNoTracking()
            .Include(a => a.Estudiante)
            .Include(a => a.Curso)
            .Include(a => a.Docente)
            .FirstOrDefaultAsync(a => a.AsignacionId == id);

        return asignacion is null ? NotFound() : asignacion;
    }

    [HttpPost]
    public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
    {
        var error = await ValidateForeignKeys(asignacion);
        if (error is not null)
        {
            return BadRequest(new { mensaje = error });
        }

        asignacion.AsignacionId = 0;
        asignacion.Estudiante = null;
        asignacion.Curso = null;
        asignacion.Docente = null;
        _context.Asignaciones.Add(asignacion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsignacion), new { id = asignacion.AsignacionId }, asignacion);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
    {
        if (id != asignacion.AsignacionId)
        {
            return BadRequest(new { mensaje = "El ID de la ruta no coincide con la asignación." });
        }

        var actual = await _context.Asignaciones.FindAsync(id);
        if (actual is null)
        {
            return NotFound();
        }

        var error = await ValidateForeignKeys(asignacion);
        if (error is not null)
        {
            return BadRequest(new { mensaje = error });
        }

        actual.EstudianteId = asignacion.EstudianteId;
        actual.CursoId = asignacion.CursoId;
        actual.DocenteId = asignacion.DocenteId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsignacion(int id)
    {
        var asignacion = await _context.Asignaciones.FindAsync(id);
        if (asignacion is null)
        {
            return NotFound();
        }

        _context.Asignaciones.Remove(asignacion);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private async Task<string?> ValidateForeignKeys(Asignacion asignacion)
    {
        if (!await _context.Estudiantes.AnyAsync(e => e.EstudianteId == asignacion.EstudianteId))
        {
            return "El estudiante indicado no existe.";
        }

        if (!await _context.Cursos.AnyAsync(c => c.CursoId == asignacion.CursoId))
        {
            return "El curso indicado no existe.";
        }

        if (!await _context.Docentes.AnyAsync(d => d.DocenteId == asignacion.DocenteId))
        {
            return "El docente indicado no existe.";
        }

        return null;
    }
}


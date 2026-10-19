using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSolution.Contexts;
using WebApiSolution.Models;

namespace WebApiSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EstudiantesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
    {
        return await _context.Estudiantes.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        return estudiante is null ? NotFound() : estudiante;
    }

    [HttpPost]
    public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
    {
        estudiante.EstudianteId = 0;
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.EstudianteId }, estudiante);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.EstudianteId)
        {
            return BadRequest(new { mensaje = "El ID de la ruta no coincide con el estudiante." });
        }

        if (!await _context.Estudiantes.AnyAsync(e => e.EstudianteId == id))
        {
            return NotFound();
        }

        _context.Entry(estudiante).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null)
        {
            return NotFound();
        }

        _context.Estudiantes.Remove(estudiante);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "El estudiante tiene asignaciones relacionadas." });
        }

        return NoContent();
    }
}


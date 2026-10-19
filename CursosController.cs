using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSolution.Contexts;
using WebApiSolution.Models;

namespace WebApiSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
        return await _context.Cursos.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        return curso is null ? NotFound() : curso;
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(Curso curso)
    {
        curso.CursoId = 0;
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCurso), new { id = curso.CursoId }, curso);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCurso(int id, Curso curso)
    {
        if (id != curso.CursoId)
        {
            return BadRequest(new { mensaje = "El ID de la ruta no coincide con el curso." });
        }

        if (!await _context.Cursos.AnyAsync(c => c.CursoId == id))
        {
            return NotFound();
        }

        _context.Entry(curso).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso is null)
        {
            return NotFound();
        }

        _context.Cursos.Remove(curso);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "El curso tiene asignaciones relacionadas." });
        }

        return NoContent();
    }
}


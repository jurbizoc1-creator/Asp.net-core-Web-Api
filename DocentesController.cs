using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSolution.Contexts;
using WebApiSolution.Models;

namespace WebApiSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocentesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DocentesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes()
    {
        return await _context.Docentes.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Docente>> GetDocente(int id)
    {
        var docente = await _context.Docentes.FindAsync(id);
        return docente is null ? NotFound() : docente;
    }

    [HttpPost]
    public async Task<ActionResult<Docente>> PostDocente(Docente docente)
    {
        docente.DocenteId = 0;
        _context.Docentes.Add(docente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocente), new { id = docente.DocenteId }, docente);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutDocente(int id, Docente docente)
    {
        if (id != docente.DocenteId)
        {
            return BadRequest(new { mensaje = "El ID de la ruta no coincide con el docente." });
        }

        if (!await _context.Docentes.AnyAsync(d => d.DocenteId == id))
        {
            return NotFound();
        }

        _context.Entry(docente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDocente(int id)
    {
        var docente = await _context.Docentes.FindAsync(id);
        if (docente is null)
        {
            return NotFound();
        }

        _context.Docentes.Remove(docente);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { mensaje = "El docente tiene asignaciones relacionadas." });
        }

        return NoContent();
    }
}


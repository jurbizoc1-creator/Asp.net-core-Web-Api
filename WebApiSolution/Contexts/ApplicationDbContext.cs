using Microsoft.EntityFrameworkCore;
using WebApiSolution.Models;

namespace WebApiSolution.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Docente> Docentes => Set<Docente>();
    public DbSet<Asignacion> Asignaciones => Set<Asignacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.ToTable("Estudiante");
            entity.HasKey(e => e.EstudianteId);
            entity.Property(e => e.EstudianteId).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Direccion).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.ToTable("Cursos");
            entity.HasKey(c => c.CursoId);
            entity.Property(c => c.CursoId).ValueGeneratedOnAdd();
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.ToTable("Docentes");
            entity.HasKey(d => d.DocenteId);
            entity.Property(d => d.DocenteId).ValueGeneratedOnAdd();
            entity.Property(d => d.Nombres).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Apellidos).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.ToTable("Asignaciones");
            entity.HasKey(a => a.AsignacionId);
            entity.Property(a => a.AsignacionId).ValueGeneratedOnAdd();

            entity.HasOne(a => a.Estudiante)
                .WithMany(e => e.Asignaciones)
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Estudiante");

            entity.HasOne(a => a.Curso)
                .WithMany(c => c.Asignaciones)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Curso");

            entity.HasOne(a => a.Docente)
                .WithMany(d => d.Asignaciones)
                .HasForeignKey(a => a.DocenteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asignacion_Docente");
        });
    }
}


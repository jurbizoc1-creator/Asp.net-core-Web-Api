using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSolution.Migrations
{
    /// <inheritdoc />
    public partial class CompletarRelacionesYDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Estudiante",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_CursoId",
                table: "Asignaciones",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_DocenteId",
                table: "Asignaciones",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_EstudianteId",
                table: "Asignaciones",
                column: "EstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignacion_Curso",
                table: "Asignaciones",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignacion_Docente",
                table: "Asignaciones",
                column: "DocenteId",
                principalTable: "Docentes",
                principalColumn: "DocenteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignacion_Estudiante",
                table: "Asignaciones",
                column: "EstudianteId",
                principalTable: "Estudiante",
                principalColumn: "EstudianteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignacion_Curso",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignacion_Docente",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignacion_Estudiante",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_CursoId",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_DocenteId",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_EstudianteId",
                table: "Asignaciones");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Estudiante");
        }
    }
}

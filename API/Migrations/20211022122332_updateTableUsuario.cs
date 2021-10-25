using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateTableUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Estudiante_EstudianteId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_EstudianteId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "EstudianteId",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EstudianteId",
                table: "Usuario",
                column: "EstudianteId",
                unique: true,
                filter: "[EstudianteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Estudiante_EstudianteId",
                table: "Usuario",
                column: "EstudianteId",
                principalTable: "Estudiante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Estudiante_EstudianteId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_EstudianteId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "EstudianteId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EstudianteId",
                table: "Usuario",
                column: "EstudianteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Estudiante_EstudianteId",
                table: "Usuario",
                column: "EstudianteId",
                principalTable: "Estudiante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

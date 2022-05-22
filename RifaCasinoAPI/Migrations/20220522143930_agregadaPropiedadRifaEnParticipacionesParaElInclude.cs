using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class agregadaPropiedadRifaEnParticipacionesParaElInclude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participaciones_Rifas_Rifaid",
                table: "Participaciones");

            migrationBuilder.RenameColumn(
                name: "Rifaid",
                table: "Participaciones",
                newName: "rifaid");

            migrationBuilder.RenameIndex(
                name: "IX_Participaciones_Rifaid",
                table: "Participaciones",
                newName: "IX_Participaciones_rifaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Participaciones_Rifas_rifaid",
                table: "Participaciones",
                column: "rifaid",
                principalTable: "Rifas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participaciones_Rifas_rifaid",
                table: "Participaciones");

            migrationBuilder.RenameColumn(
                name: "rifaid",
                table: "Participaciones",
                newName: "Rifaid");

            migrationBuilder.RenameIndex(
                name: "IX_Participaciones_rifaid",
                table: "Participaciones",
                newName: "IX_Participaciones_Rifaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Participaciones_Rifas_Rifaid",
                table: "Participaciones",
                column: "Rifaid",
                principalTable: "Rifas",
                principalColumn: "id");
        }
    }
}

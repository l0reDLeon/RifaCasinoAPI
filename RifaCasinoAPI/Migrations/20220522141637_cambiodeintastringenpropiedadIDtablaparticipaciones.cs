using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class cambiodeintastringenpropiedadIDtablaparticipaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "idParticipante",
                table: "Participaciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "idParticipante",
                table: "Participaciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

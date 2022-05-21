using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class EdicionPremio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Premios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Premios");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaCasinoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rifas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    vigente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Participaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idParticipante = table.Column<int>(type: "int", nullable: false),
                    participanteid = table.Column<int>(type: "int", nullable: true),
                    idRifa = table.Column<int>(type: "int", nullable: false),
                    rifaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Participaciones_Participantes_participanteid",
                        column: x => x.participanteid,
                        principalTable: "Participantes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Participaciones_Rifas_rifaid",
                        column: x => x.rifaid,
                        principalTable: "Rifas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRifa = table.Column<int>(type: "int", nullable: false),
                    rifaid = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Premios_Rifas_rifaid",
                        column: x => x.rifaid,
                        principalTable: "Rifas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participaciones_participanteid",
                table: "Participaciones",
                column: "participanteid");

            migrationBuilder.CreateIndex(
                name: "IX_Participaciones_rifaid",
                table: "Participaciones",
                column: "rifaid");

            migrationBuilder.CreateIndex(
                name: "IX_Premios_rifaid",
                table: "Premios",
                column: "rifaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participaciones");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Rifas");
        }
    }
}

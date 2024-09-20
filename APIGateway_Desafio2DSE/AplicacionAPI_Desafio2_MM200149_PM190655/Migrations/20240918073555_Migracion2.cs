using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplicacionAPI_Desafio2_MM200149_PM190655.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizadores_Eventos_EventoId",
                table: "Organizadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Eventos_EventoId",
                table: "Participantes");

            migrationBuilder.DropIndex(
                name: "IX_Participantes_EventoId",
                table: "Participantes");

            migrationBuilder.DropIndex(
                name: "IX_Organizadores_EventoId",
                table: "Organizadores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Participantes_EventoId",
                table: "Participantes",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizadores_EventoId",
                table: "Organizadores",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizadores_Eventos_EventoId",
                table: "Organizadores",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Eventos_EventoId",
                table: "Participantes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoIDS.Migrations
{
    public partial class AggiuntaRelazioneOrdineUtente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUtente",
                table: "Ordini",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_IdUtente",
                table: "Ordini",
                column: "IdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Utenti_IdUtente",
                table: "Ordini",
                column: "IdUtente",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Utenti_IdUtente",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_IdUtente",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "IdUtente",
                table: "Ordini");
        }
    }
}

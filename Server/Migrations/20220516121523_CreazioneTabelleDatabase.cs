using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoIDS.Migrations
{
    public partial class CreazioneTabelleDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Totale = table.Column<double>(type: "REAL", nullable: false),
                    Created_At = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted_At = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    Prezzo = table.Column<double>(type: "REAL", nullable: false),
                    Deleted_At = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdineProdotto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdProdotto = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOrdine = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantità = table.Column<int>(type: "INTEGER", nullable: false),
                    Totale = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdineProdotto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdineProdotto_Ordini_IdOrdine",
                        column: x => x.IdOrdine,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdineProdotto_Prodotti_IdProdotto",
                        column: x => x.IdProdotto,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdineProdotto_IdOrdine",
                table: "OrdineProdotto",
                column: "IdOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_OrdineProdotto_IdProdotto",
                table: "OrdineProdotto",
                column: "IdProdotto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdineProdotto");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Prodotti");
        }
    }
}

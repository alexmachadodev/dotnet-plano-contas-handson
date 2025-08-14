using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanoContaHandsOn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanoConta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AceitaLancamento = table.Column<bool>(type: "bit", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    IdPai = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoConta_PlanoConta_IdPai",
                        column: x => x.IdPai,
                        principalTable: "PlanoConta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PlanoConta",
                columns: new[] { "Id", "AceitaLancamento", "Codigo", "IdPai", "Nome", "Tipo" },
                values: new object[,]
                {
                    { new Guid("c8b512f4-9478-4222-811d-487b3b8a3e4e"), false, "1", null, "Receitas", 1 },
                    { new Guid("d6e1b2f8-2c39-4f7a-8f6a-3e21b1e9b8a1"), false, "2", null, "Despesas", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoConta_Codigo",
                table: "PlanoConta",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanoConta_IdPai",
                table: "PlanoConta",
                column: "IdPai");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoConta_Nome_Codigo",
                table: "PlanoConta",
                columns: new[] { "Nome", "Codigo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanoConta");
        }
    }
}

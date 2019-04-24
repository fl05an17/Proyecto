using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaAC.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    ActividadesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.ActividadesID);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    HorarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActividadesID = table.Column<int>(nullable: false),
                    Dia = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.HorarioID);
                    table.ForeignKey(
                        name: "FK_Horario_Actividades_ActividadesID",
                        column: x => x.ActividadesID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maquinaria",
                columns: table => new
                {
                    MaquinariaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActividadesID = table.Column<int>(nullable: false),
                    Cantidad = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinaria", x => x.MaquinariaID);
                    table.ForeignKey(
                        name: "FK_Maquinaria_Actividades_ActividadesID",
                        column: x => x.ActividadesID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    TarifaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActividadesID = table.Column<int>(nullable: false),
                    ValorEmp = table.Column<double>(nullable: false),
                    ValorEst = table.Column<double>(nullable: false),
                    ValorFam = table.Column<double>(nullable: false),
                    ValorGrad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.TarifaID);
                    table.ForeignKey(
                        name: "FK_Tarifas_Actividades_ActividadesID",
                        column: x => x.ActividadesID,
                        principalTable: "Actividades",
                        principalColumn: "ActividadesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horario_ActividadesID",
                table: "Horario",
                column: "ActividadesID");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinaria_ActividadesID",
                table: "Maquinaria",
                column: "ActividadesID");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifas_ActividadesID",
                table: "Tarifas",
                column: "ActividadesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Maquinaria");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "Actividades");
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercicios.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    FK_Cardapio_Empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardapio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cardapio_Empresa_FK_Cardapio_Empresa",
                        column: x => x.FK_Cardapio_Empresa,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    FK_Localizacao_Empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizacao_Empresa_FK_Localizacao_Empresa",
                        column: x => x.FK_Localizacao_Empresa,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cardapio_FK_Cardapio_Empresa",
                table: "Cardapio",
                column: "FK_Cardapio_Empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_FK_Localizacao_Empresa",
                table: "Localizacao",
                column: "FK_Localizacao_Empresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cardapio");

            migrationBuilder.DropTable(
                name: "Localizacao");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}

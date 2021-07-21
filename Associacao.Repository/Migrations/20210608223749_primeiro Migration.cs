using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Associacao.Repository.Migrations
{
    public partial class primeiroMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero_cadastro = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rg = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    data_nascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    telefone1 = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    telefone2 = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    bairro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    logradouro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    observacao = table.Column<string>(type: "text", nullable: false),
                    quantidade_casas = table.Column<int>(type: "integer", nullable: false),
                    isento = table.Column<bool>(type: "boolean", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mensalidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_vencimento = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: false),
                    data_pagamento = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 11, nullable: true),
                    pago = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    valor = table.Column<float>(type: "real", maxLength: 100, nullable: false),
                    id_pessoa = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensalidade", x => x.id);
                    table.ForeignKey(
                        name: "FK_mensalidade_pessoa_id_pessoa",
                        column: x => x.id_pessoa,
                        principalTable: "pessoa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mensalidade_id_pessoa",
                table: "mensalidade",
                column: "id_pessoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mensalidade");

            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}

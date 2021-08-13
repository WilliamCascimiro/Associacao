using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Associacao.Repository.Migrations
{
    public partial class configuraçãoaddcampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "criado_em",
                table: "configuracao",
                type: "timestamp without time zone",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "criado_em",
                table: "configuracao");
        }
    }
}

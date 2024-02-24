using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdminPersonAndCity.Migrations
{
    public partial class CreatePersonAndRelationshipWithCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    PersonType = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    CpfCnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Compl = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    District = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BirthDateFoundation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RegStatus = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_persons_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_CityId",
                table: "persons",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSi_IT_Demo_New.Services.Ordini.Migrations
{
    public partial class RimossoRecapitoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecapitoId",
                table: "Ordini");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecapitoId",
                table: "Ordini",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSi_IT_Demo_New.Services.Prodotti.Migrations
{
    public partial class AggiuntoQuantita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantita",
                table: "ProdottiOrdinati",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantita",
                table: "ProdottiOrdinati");
        }
    }
}

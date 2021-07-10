using Microsoft.EntityFrameworkCore.Migrations;

namespace ProspectosAPI.Migrations
{
    public partial class Sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Prospectos",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Prospectos");
        }
    }
}

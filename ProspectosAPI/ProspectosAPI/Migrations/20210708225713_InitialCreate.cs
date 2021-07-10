using Microsoft.EntityFrameworkCore.Migrations;

namespace ProspectosAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prospectos",
                columns: table => new
                {
                    IdProspecto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    NumeroCalle = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    Colonia = table.Column<string>(type: "varchar(50)", nullable: true),
                    CP = table.Column<string>(type: "varchar(5)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(10)", nullable: true),
                    Rfc = table.Column<string>(type: "varchar(13)", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospectos", x => x.IdProspecto);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prospectos");
        }
    }
}

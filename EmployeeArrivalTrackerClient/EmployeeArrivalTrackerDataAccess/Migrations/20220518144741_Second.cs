using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeArrivalTrackerDataAccess.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamsNomenclatureTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsNomenclatureTables", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TeamsNomenclatureTables",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Platform" },
                    { 2, "Sales" },
                    { 3, "Billing" },
                    { 4, "Mirage" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamsNomenclatureTables");
        }
    }
}

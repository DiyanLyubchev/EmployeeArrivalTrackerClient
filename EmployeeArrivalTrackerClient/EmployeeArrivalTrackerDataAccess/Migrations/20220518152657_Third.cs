using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeArrivalTrackerDataAccess.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesNomenclatureTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesNomenclatureTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    RolesNomenclatureTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesTables_RolesNomenclatureTables_RolesNomenclatureTableId",
                        column: x => x.RolesNomenclatureTableId,
                        principalTable: "RolesNomenclatureTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesTableTeamsNomenclatureTable",
                columns: table => new
                {
                    EmployeesTablesId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesTableTeamsNomenclatureTable", x => new { x.EmployeesTablesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_EmployeesTableTeamsNomenclatureTable_EmployeesTables_EmployeesTablesId",
                        column: x => x.EmployeesTablesId,
                        principalTable: "EmployeesTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesTableTeamsNomenclatureTable_TeamsNomenclatureTables_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "TeamsNomenclatureTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTeamsNomenclatureTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeesTableId = table.Column<int>(type: "int", nullable: false),
                    TeamsNomenclatureTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTeamsNomenclatureTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTeamsNomenclatureTables_EmployeesTables_EmployeesTableId",
                        column: x => x.EmployeesTableId,
                        principalTable: "EmployeesTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTeamsNomenclatureTables_TeamsNomenclatureTables_TeamsNomenclatureTableId",
                        column: x => x.TeamsNomenclatureTableId,
                        principalTable: "TeamsNomenclatureTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RolesNomenclatureTables",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Junior Developer" },
                    { 2, "Semi Senior Developer" },
                    { 3, "Senior Developer" },
                    { 4, "Principal" },
                    { 5, "Team Leader" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTables_RolesNomenclatureTableId",
                table: "EmployeesTables",
                column: "RolesNomenclatureTableId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesTableTeamsNomenclatureTable_TeamsId",
                table: "EmployeesTableTeamsNomenclatureTable",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeamsNomenclatureTables_EmployeesTableId",
                table: "EmployeeTeamsNomenclatureTables",
                column: "EmployeesTableId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeamsNomenclatureTables_TeamsNomenclatureTableId",
                table: "EmployeeTeamsNomenclatureTables",
                column: "TeamsNomenclatureTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesTableTeamsNomenclatureTable");

            migrationBuilder.DropTable(
                name: "EmployeeTeamsNomenclatureTables");

            migrationBuilder.DropTable(
                name: "EmployeesTables");

            migrationBuilder.DropTable(
                name: "RolesNomenclatureTables");
        }
    }
}

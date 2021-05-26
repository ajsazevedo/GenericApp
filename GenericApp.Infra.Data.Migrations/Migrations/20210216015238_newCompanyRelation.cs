using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    public partial class newCompanyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCompany");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_CompanyId",
                table: "Person",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_JuridicalPerson_CompanyId",
                table: "Person",
                column: "CompanyId",
                principalTable: "JuridicalPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_JuridicalPerson_CompanyId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_CompanyId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "EmployeeCompany",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCompany_JuridicalPerson_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JuridicalPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCompany_Person_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompany_CompanyId",
                table: "EmployeeCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompany_EmployeeId",
                table: "EmployeeCompany",
                column: "EmployeeId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<long>(nullable: true),
                    UpdaterId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    Password = table.Column<string>(maxLength: 12, nullable: true),
                    active = table.Column<bool>(nullable: false),
                    role = table.Column<int>(nullable: false),
                    password_valid_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JuridicalPerson",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<long>(nullable: true),
                    UpdaterId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    PublicName = table.Column<string>(maxLength: 40, nullable: true),
                    Cnpj = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuridicalPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JuridicalPerson_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JuridicalPerson_User_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<long>(nullable: true),
                    UpdaterId = table.Column<long>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Cpf = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Code = table.Column<string>(maxLength: 8, nullable: true),
                    Admission = table.Column<DateTime>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    CompanyId = table.Column<long>(nullable: true),
                    Active = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_JuridicalPerson_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JuridicalPerson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_User_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<long>(nullable: true),
                    UpdaterId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Person_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_User_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_Cnpj",
                table: "JuridicalPerson",
                column: "Cnpj",
                unique: true,
                filter: "[Cnpj] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_CreatorId",
                table: "JuridicalPerson",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_UpdaterId",
                table: "JuridicalPerson",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatorId",
                table: "Order",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdaterId",
                table: "Order",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Cpf",
                table: "Person",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CompanyId",
                table: "Person",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Cpf1",
                table: "Person",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CreatorId",
                table: "Person",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UpdaterId",
                table: "Person",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatorId",
                table: "User",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdaterId",
                table: "User",
                column: "UpdaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "JuridicalPerson");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

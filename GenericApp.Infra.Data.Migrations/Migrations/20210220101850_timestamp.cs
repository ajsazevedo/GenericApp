using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    public partial class timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Updater",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "JuridicalPerson",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "JuridicalPerson",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "JuridicalPerson",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Updater",
                table: "JuridicalPerson",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Updater",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "Updater",
                table: "JuridicalPerson");
        }
    }
}

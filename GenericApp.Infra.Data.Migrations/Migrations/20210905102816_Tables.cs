using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuridicalPerson_User_CreatorId",
                table: "JuridicalPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_JuridicalPerson_User_UpdaterId",
                table: "JuridicalPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_CreatorId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UpdaterId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_User_CreatorId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_User_UpdaterId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatorId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdaterId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Person_CreatorId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_UpdaterId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Order_CreatorId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UpdaterId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_JuridicalPerson_CreatorId",
                table: "JuridicalPerson");

            migrationBuilder.DropIndex(
                name: "IX_JuridicalPerson_UpdaterId",
                table: "JuridicalPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatorId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdaterId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Person",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Person",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Person",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Order",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Order",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JuridicalPerson",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "JuridicalPerson",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "JuridicalPerson",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.AddColumn<long>(
                name: "creator_id",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updater_id",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "creator_id",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updater_id",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "creator_id",
                table: "JuridicalPerson",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updater_id",
                table: "JuridicalPerson",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "creator_id",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "updater_id",
                table: "users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_creator_id",
                table: "Person",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_updater_id",
                table: "Person",
                column: "updater_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_creator_id",
                table: "Order",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_updater_id",
                table: "Order",
                column: "updater_id");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_creator_id",
                table: "JuridicalPerson",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_updater_id",
                table: "JuridicalPerson",
                column: "updater_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_creator_id",
                table: "users",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_updater_id",
                table: "users",
                column: "updater_id");

            migrationBuilder.AddForeignKey(
                name: "FK_JuridicalPerson_users_creator_id",
                table: "JuridicalPerson",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JuridicalPerson_users_updater_id",
                table: "JuridicalPerson",
                column: "updater_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_users_creator_id",
                table: "Order",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_users_updater_id",
                table: "Order",
                column: "updater_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_users_creator_id",
                table: "Person",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_users_updater_id",
                table: "Person",
                column: "updater_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_creator_id",
                table: "users",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_updater_id",
                table: "users",
                column: "updater_id",
                principalTable: "users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuridicalPerson_users_creator_id",
                table: "JuridicalPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_JuridicalPerson_users_updater_id",
                table: "JuridicalPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_users_creator_id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_users_updater_id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_users_creator_id",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_users_updater_id",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_users_users_creator_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_users_updater_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_Person_creator_id",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_updater_id",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Order_creator_id",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_updater_id",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_JuridicalPerson_creator_id",
                table: "JuridicalPerson");

            migrationBuilder.DropIndex(
                name: "IX_JuridicalPerson_updater_id",
                table: "JuridicalPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_creator_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_updater_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "updater_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "updater_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "updater_id",
                table: "JuridicalPerson");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updater_id",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Person",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Person",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Person",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Order",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Order",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Order",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "JuridicalPerson",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "JuridicalPerson",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "JuridicalPerson",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "User",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "User",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Person",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                table: "Person",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                table: "Order",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "JuridicalPerson",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                table: "JuridicalPerson",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdaterId",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CreatorId",
                table: "Person",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UpdaterId",
                table: "Person",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatorId",
                table: "Order",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdaterId",
                table: "Order",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_CreatorId",
                table: "JuridicalPerson",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_JuridicalPerson_UpdaterId",
                table: "JuridicalPerson",
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

            migrationBuilder.AddForeignKey(
                name: "FK_JuridicalPerson_User_CreatorId",
                table: "JuridicalPerson",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JuridicalPerson_User_UpdaterId",
                table: "JuridicalPerson",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_CreatorId",
                table: "Order",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UpdaterId",
                table: "Order",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_User_CreatorId",
                table: "Person",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_User_UpdaterId",
                table: "Person",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatorId",
                table: "User",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdaterId",
                table: "User",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

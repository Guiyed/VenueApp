using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VenueApp.Migrations
{
    public partial class FirstUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Types",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "Types",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Memberships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "Memberships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "Categories",
                nullable: false,


                defaultValue: false);
                                 
            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "ID", "Deleted", "Name", "Protected" },
                values: new object[] { 1, false, "none", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "ID", "Deleted", "Name", "Protected" },
                values: new object[] { 1, false, "admin", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "ID", "Deleted", "Name", "Protected" },
                values: new object[] { 2, false, "user", true });

            migrationBuilder.InsertData(
             table: "Users",
             columns: new[] { "ID", "Username", "Password", "MembershipID", "TypeID", "Created", "Deleted", "Protected" },
             values: new object[] { 1, "admin", "password", 1, 1, DateTime.Now, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
              table: "Users",
              keyColumn: "ID",
              keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Protected",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

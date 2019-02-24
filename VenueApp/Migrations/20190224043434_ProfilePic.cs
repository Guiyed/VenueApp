using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VenueApp.Migrations
{
    public partial class ProfilePic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "CategoryID", "Created", "Date", "Deleted", "Description", "Location", "Name", "Price", "Protected" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 7, 28, 18, 35, 5, 0, DateTimeKind.Unspecified), false, "Description of Test Event 1.", "Launchcode. Miami, Florida", "Test Event 1", 9.99, true },
                    { 2, 1, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), false, "Description of Test Event 2.", "Launchcode. Miami, Florida", "Test Event 2", 10.5, false },
                    { 3, 2, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 3, 1, 18, 10, 0, 0, DateTimeKind.Unspecified), false, "Description of Music Event", "Miami, Florida", "Music Event", 10.99, false },
                    { 4, 3, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 3, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), false, "Description of Art Event", "Ft. Lauderdale, Florida", "Art Event", 20.49, false },
                    { 5, 4, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 3, 5, 8, 15, 0, 0, DateTimeKind.Unspecified), false, "Description of Business Event", "Miramar, Florida", "Business Event", 17.0, false },
                    { 6, 5, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 3, 12, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "Description of Party Event", "Coral Gables, Florida", "Party Event", 90.0, false },
                    { 7, 6, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 3, 28, 10, 25, 10, 0, DateTimeKind.Unspecified), false, "Description of Class Event", "Kendall, Florida", "Classes Event", 35.0, false },
                    { 8, 7, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 4, 15, 21, 27, 10, 0, DateTimeKind.Unspecified), false, "Description of Sport Event", "Weston. Miami, Florida", "Sport Event", 49.98, false },
                    { 9, 8, new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), new DateTime(2019, 8, 1, 18, 35, 30, 0, DateTimeKind.Unspecified), false, "Description of Food & Drink Event", "Sawgrass, Florida", "Food & Drink Event", 12.0, false }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Created", "ProfilePicture" },
                values: new object[] { new DateTime(2019, 2, 23, 23, 34, 34, 635, DateTimeKind.Local), "/images/Avatar3.svg" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Created", "ProfilePicture" },
                values: new object[] { new DateTime(2019, 2, 23, 23, 34, 34, 636, DateTimeKind.Local), "/images/Avatar3.svg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Created",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2,
                column: "Created",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

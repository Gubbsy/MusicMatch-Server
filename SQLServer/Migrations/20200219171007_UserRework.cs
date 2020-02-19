using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class UserRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea99095c-6172-4c3a-85bd-9c0af5c34884");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fac56ee1-8884-483b-99cf-4a9149c1dc79");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a07eb6fe-df35-41e5-b7f8-d63f22e6365e", "cae661e1-864a-4154-a6c2-3b27e6968ccc", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60f0da79-9345-4790-98dc-81d942d35a37", "ddaf3baf-9e16-41e7-89be-17855ad57db5", "eventsManager", "EVENTSMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f0da79-9345-4790-98dc-81d942d35a37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a07eb6fe-df35-41e5-b7f8-d63f22e6365e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea99095c-6172-4c3a-85bd-9c0af5c34884", "a86040ca-b6e4-490e-ae5a-ae6406f234ec", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fac56ee1-8884-483b-99cf-4a9149c1dc79", "2524884e-6af2-449b-8cb2-6151a0b20afd", "eventsManager", "EVENTSMANAGER" });
        }
    }
}

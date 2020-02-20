using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class UserRework2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "077eae59-37e0-4b63-99ea-f176babd86aa", "1b8d4580-62ab-4e9d-ba8a-08e1dc5b5510", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f61c6910-02c1-4a10-a787-2ec4d5a1a774", "33d8b334-1bca-4fde-9d24-5dcbc34bd6ef", "eventsManager", "EVENTSMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "077eae59-37e0-4b63-99ea-f176babd86aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f61c6910-02c1-4a10-a787-2ec4d5a1a774");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a07eb6fe-df35-41e5-b7f8-d63f22e6365e", "cae661e1-864a-4154-a6c2-3b27e6968ccc", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60f0da79-9345-4790-98dc-81d942d35a37", "ddaf3baf-9e16-41e7-89be-17855ad57db5", "eventsManager", "EVENTSMANAGER" });
        }
    }
}

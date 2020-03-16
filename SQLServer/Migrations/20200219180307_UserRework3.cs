using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class UserRework3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "077e6e45-0f4e-4015-83ae-fbd73c626c66", "968968ba-07af-4c21-a56a-18cffe2d90bf", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a68b8b19-d1dd-4090-8368-a82dfda1a4fb", "2036ddd5-a4e4-430b-9775-7a21e56d4c83", "events manager", "EVENTS MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "077e6e45-0f4e-4015-83ae-fbd73c626c66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a68b8b19-d1dd-4090-8368-a82dfda1a4fb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "077eae59-37e0-4b63-99ea-f176babd86aa", "1b8d4580-62ab-4e9d-ba8a-08e1dc5b5510", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f61c6910-02c1-4a10-a787-2ec4d5a1a774", "33d8b334-1bca-4fde-9d24-5dcbc34bd6ef", "eventsManager", "EVENTSMANAGER" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "717a18c6-be57-405f-96cc-6af1b3525c4c", "8b555601-79ee-41f3-82cf-2fe38268cb70", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65b663ea-6573-4b67-b2a6-c4be8c7437eb", "76003797-423a-456f-8a46-72b68616e357", "eventsManager", "EVENTSMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65b663ea-6573-4b67-b2a6-c4be8c7437eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "717a18c6-be57-405f-96cc-6af1b3525c4c");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class RenameMatchColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "352a0d74-6e35-4677-8254-2aadf63ca607");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9092e051-8648-44e0-a2e7-27987010d6c3");

            migrationBuilder.DropColumn(
                name: "UId1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UId2",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UId1",
                table: "Introductions");

            migrationBuilder.DropColumn(
                name: "UId2",
                table: "Introductions");

            migrationBuilder.AddColumn<string>(
                name: "Matchie",
                table: "Matches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Matches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "Introductions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Introductions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7d8c964-e677-4e39-a0b6-cd7a358a09c8", "e7121819-e53b-4039-8f9e-fae1c7cec768", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fcf779b-5444-45fc-95f3-cac1c60edcb0", "cad605fe-1f95-4e8e-ab8b-e647f0467fa1", "events manager", "EVENTS MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fcf779b-5444-45fc-95f3-cac1c60edcb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7d8c964-e677-4e39-a0b6-cd7a358a09c8");

            migrationBuilder.DropColumn(
                name: "Matchie",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Recipient",
                table: "Introductions");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Introductions");

            migrationBuilder.AddColumn<string>(
                name: "UId1",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UId2",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UId1",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UId2",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "352a0d74-6e35-4677-8254-2aadf63ca607", "7300fb90-b638-437d-8a18-153186bfa587", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9092e051-8648-44e0-a2e7-27987010d6c3", "e0969ec3-d0c8-4323-8397-dd55c1cc56da", "events manager", "EVENTS MANAGER" });
        }
    }
}

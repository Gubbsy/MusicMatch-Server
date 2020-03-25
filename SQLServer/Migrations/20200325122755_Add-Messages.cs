using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class AddMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fcf779b-5444-45fc-95f3-cac1c60edcb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7d8c964-e677-4e39-a0b6-cd7a358a09c8");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    Msg = table.Column<string>(nullable: false),
                    Date = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "414991ba-ffd5-47fe-ae4a-3fc339a861d7", "7354f1a2-57aa-4e79-9bfd-e6475c218a31", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63f0bd17-d734-4288-8457-4b9daae29191", "3a8aa95b-62c0-404d-80af-fee6ae405539", "events manager", "EVENTS MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "414991ba-ffd5-47fe-ae4a-3fc339a861d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63f0bd17-d734-4288-8457-4b9daae29191");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7d8c964-e677-4e39-a0b6-cd7a358a09c8", "e7121819-e53b-4039-8f9e-fae1c7cec768", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fcf779b-5444-45fc-95f3-cac1c60edcb0", "cad605fe-1f95-4e8e-ab8b-e647f0467fa1", "events manager", "EVENTS MANAGER" });
        }
    }
}

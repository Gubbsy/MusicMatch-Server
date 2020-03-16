using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class Venues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b225f899-effa-4fff-a5a0-ca38cba839ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca5f7a0a-3cf0-4dbb-9744-d4e8530bb47c");

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserVenue",
                columns: table => new
                {
                    VenueId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVenue", x => new { x.UserId, x.VenueId });
                    table.ForeignKey(
                        name: "FK_UserVenue_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVenue_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea99095c-6172-4c3a-85bd-9c0af5c34884", "a86040ca-b6e4-490e-ae5a-ae6406f234ec", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fac56ee1-8884-483b-99cf-4a9149c1dc79", "2524884e-6af2-449b-8cb2-6151a0b20afd", "eventsManager", "EVENTSMANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_UserVenue_VenueId",
                table: "UserVenue",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVenue");

            migrationBuilder.DropTable(
                name: "Venues");

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
                values: new object[] { "ca5f7a0a-3cf0-4dbb-9744-d4e8530bb47c", "16360e40-3048-403a-b858-1a958b8707e2", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b225f899-effa-4fff-a5a0-ca38cba839ba", "5ab162b9-9070-4f2a-8546-f8de091bad3a", "eventsManager", "EVENTSMANAGER" });
        }
    }
}

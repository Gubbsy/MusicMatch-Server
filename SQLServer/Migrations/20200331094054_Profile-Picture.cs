using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLServer.Migrations
{
    public partial class ProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "414991ba-ffd5-47fe-ae4a-3fc339a861d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63f0bd17-d734-4288-8457-4b9daae29191");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e0e4bc6-f2f5-4087-8d1f-a873fa5706c8", "7a183380-c00f-4c45-8217-7ae43ede5449", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "96e465bc-b2c8-4ba8-bd83-4fb901cb5935", "a55f16c2-5438-419a-9534-6d56d56f77ef", "events manager", "EVENTS MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e0e4bc6-f2f5-4087-8d1f-a873fa5706c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96e465bc-b2c8-4ba8-bd83-4fb901cb5935");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "414991ba-ffd5-47fe-ae4a-3fc339a861d7", "7354f1a2-57aa-4e79-9bfd-e6475c218a31", "artist", "ARTIST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63f0bd17-d734-4288-8457-4b9daae29191", "3a8aa95b-62c0-404d-80af-fee6ae405539", "events manager", "EVENTS MANAGER" });
        }
    }
}

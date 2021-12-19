using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candle.InfraStructure.Migrations
{
    public partial class Ten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileStatus",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileStatusDef",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileStatusDef", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfileStatus",
                table: "User",
                column: "ProfileStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ProfileStatusDef_ProfileStatus",
                table: "User",
                column: "ProfileStatus",
                principalTable: "ProfileStatusDef",
                principalColumn: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ProfileStatusDef_ProfileStatus",
                table: "User");

            migrationBuilder.DropTable(
                name: "ProfileStatusDef");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfileStatus",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfileStatus",
                table: "User");
        }
    }
}

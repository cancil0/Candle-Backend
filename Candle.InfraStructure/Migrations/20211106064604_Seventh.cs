using Microsoft.EntityFrameworkCore.Migrations;

namespace Candle.InfraStructure.Migrations
{
    public partial class Seventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Follower_UserId",
                table: "Follower");

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserId_FollowerId",
                table: "Follower",
                columns: new[] { "UserId", "FollowerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Follower_UserId_FollowerId",
                table: "Follower");

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserId",
                table: "Follower",
                column: "UserId");
        }
    }
}

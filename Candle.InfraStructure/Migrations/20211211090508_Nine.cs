using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candle.InfraStructure.Migrations
{
    public partial class Nine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentUserId_ParentCommentPostId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ParentCommentUserId_ParentCommentPostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ParentCommentPostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ParentCommentUserId",
                table: "Comment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId",
                principalTable: "Comment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId",
                table: "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCommentPostId",
                table: "Comment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCommentUserId",
                table: "Comment",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentUserId_ParentCommentPostId",
                table: "Comment",
                columns: new[] { "ParentCommentUserId", "ParentCommentPostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentUserId_ParentCommentPostId",
                table: "Comment",
                columns: new[] { "ParentCommentUserId", "ParentCommentPostId" },
                principalTable: "Comment",
                principalColumns: new[] { "UserId", "PostId" });
        }
    }
}

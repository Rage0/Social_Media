using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_CreaterId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Massages_User_CreaterId",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_CreaterId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "MemberAndMemberChats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_CreaterId",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_User_CreaterId",
                table: "Massages",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_CreaterId",
                table: "Posts",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_User_CreaterId",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_CreaterId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "MemberAndMemberChats",
                columns: table => new
                {
                    MemberChatsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberAndMemberChats", x => new { x.MemberChatsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_MemberAndMemberChats_Chats_MemberChatsId",
                        column: x => x.MemberChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberAndMemberChats_User_MembersId",
                        column: x => x.MembersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CreaterId",
                table: "Chats",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAndMemberChats_MembersId",
                table: "MemberAndMemberChats",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_CreaterId",
                table: "Chats",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_User_CreaterId",
                table: "Massages",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_CreaterId",
                table: "Posts",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

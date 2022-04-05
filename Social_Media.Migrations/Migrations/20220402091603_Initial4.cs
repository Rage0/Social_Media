using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_Users_CreaterId",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreaterId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_FollowingUserId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_UserFriendsId",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser");

            migrationBuilder.RenameTable(
                name: "UserUser",
                newName: "FriendsAndFollowingUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_UserFriendsId",
                table: "FriendsAndFollowingUser",
                newName: "IX_FriendsAndFollowingUser_UserFriendsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendsAndFollowingUser",
                table: "FriendsAndFollowingUser",
                columns: new[] { "FollowingUserId", "UserFriendsId" });

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
                        name: "FK_MemberAndMemberChats_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
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
                name: "FK_Chats_Users_CreaterId",
                table: "Chats",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsAndFollowingUser_Users_FollowingUserId",
                table: "FriendsAndFollowingUser",
                column: "FollowingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsAndFollowingUser_Users_UserFriendsId",
                table: "FriendsAndFollowingUser",
                column: "UserFriendsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_Users_CreaterId",
                table: "Massages",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreaterId",
                table: "Posts",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_CreaterId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_Users_FollowingUserId",
                table: "FriendsAndFollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_Users_UserFriendsId",
                table: "FriendsAndFollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Massages_Users_CreaterId",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreaterId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "MemberAndMemberChats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_CreaterId",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendsAndFollowingUser",
                table: "FriendsAndFollowingUser");

            migrationBuilder.RenameTable(
                name: "FriendsAndFollowingUser",
                newName: "UserUser");

            migrationBuilder.RenameIndex(
                name: "IX_FriendsAndFollowingUser_UserFriendsId",
                table: "UserUser",
                newName: "IX_UserUser_UserFriendsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser",
                columns: new[] { "FollowingUserId", "UserFriendsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_Users_CreaterId",
                table: "Massages",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreaterId",
                table: "Posts",
                column: "CreaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_UserFriendsId",
                table: "UserUser",
                column: "UserFriendsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

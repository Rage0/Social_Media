using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massage_AspNetUsers_CreaterId",
                table: "Massage");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_CreaterId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingUserId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_UserFriendsId",
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
                        name: "FK_MemberAndMemberChats_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberAndMemberChats_Chat_MemberChatsId",
                        column: x => x.MemberChatsId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_CreaterId",
                table: "Chat",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAndMemberChats_MembersId",
                table: "MemberAndMemberChats",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_CreaterId",
                table: "Chat",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_FollowingUserId",
                table: "FriendsAndFollowingUser",
                column: "FollowingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_UserFriendsId",
                table: "FriendsAndFollowingUser",
                column: "UserFriendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Massage_AspNetUsers_CreaterId",
                table: "Massage",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_CreaterId",
                table: "Post",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_CreaterId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_FollowingUserId",
                table: "FriendsAndFollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_UserFriendsId",
                table: "FriendsAndFollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Massage_AspNetUsers_CreaterId",
                table: "Massage");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_CreaterId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "MemberAndMemberChats");

            migrationBuilder.DropIndex(
                name: "IX_Chat_CreaterId",
                table: "Chat");

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
                name: "FK_Massage_AspNetUsers_CreaterId",
                table: "Massage",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_CreaterId",
                table: "Post",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_UserFriendsId",
                table: "UserUser",
                column: "UserFriendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

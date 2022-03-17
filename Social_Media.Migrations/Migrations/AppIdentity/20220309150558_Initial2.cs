using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_Chat_CommentsId",
                table: "BaseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingFriendsId",
                table: "UserUser");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_CommentsId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "CommentsId",
                table: "BaseEntity");

            migrationBuilder.RenameColumn(
                name: "FollowingFriendsId",
                table: "UserUser",
                newName: "FollowingUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Chat",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    MemberChatsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.MemberChatsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_ChatUser_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_Chat_MemberChatsId",
                        column: x => x.MemberChatsId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_MembersId",
                table: "ChatUser",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingUserId",
                table: "UserUser");

            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "FollowingUserId",
                table: "UserUser",
                newName: "FollowingFriendsId");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentsId",
                table: "BaseEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_CommentsId",
                table: "BaseEntity",
                column: "CommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_Chat_CommentsId",
                table: "BaseEntity",
                column: "CommentsId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_FollowingFriendsId",
                table: "UserUser",
                column: "FollowingFriendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

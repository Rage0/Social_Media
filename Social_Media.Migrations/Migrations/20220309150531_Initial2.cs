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
                name: "FK_UserUser_User_FollowingFriendsId",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "FollowingFriendsId",
                table: "UserUser",
                newName: "FollowingUserId");

            migrationBuilder.RenameColumn(
                name: "CreaterId",
                table: "Chats",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_CreaterId",
                table: "Chats",
                newName: "IX_Chats_UserId1");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_UserId1",
                table: "Chats",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_UserId1",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FollowingUserId",
                table: "UserUser");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "FollowingUserId",
                table: "UserUser",
                newName: "FollowingFriendsId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Chats",
                newName: "CreaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId1",
                table: "Chats",
                newName: "IX_Chats_CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_CreaterId",
                table: "Chats",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowingFriendsId",
                table: "UserUser",
                column: "FollowingFriendsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_User_CreaterId",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_CreaterId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FollowingUserId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_UserFriendsId",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_UserFriendsId",
                table: "UserUser",
                column: "UserFriendsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

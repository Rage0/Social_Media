using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_FollowingUserId",
                table: "FriendsAndFollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendsAndFollowingUser_AspNetUsers_UserFriendsId",
                table: "FriendsAndFollowingUser");

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

using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoProfileRoute",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowingUserId = table.Column<string>(type: "text", nullable: false),
                    UserFriendsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowingUserId, x.UserFriendsId });
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_UserFriendsId",
                        column: x => x.UserFriendsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_UserFriendsId",
                table: "UserUser",
                column: "UserFriendsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropColumn(
                name: "PhotoProfileRoute",
                table: "AspNetUsers");
        }
    }
}

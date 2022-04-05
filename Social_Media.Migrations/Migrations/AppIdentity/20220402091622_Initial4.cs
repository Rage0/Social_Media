using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendsAndFollowingUser");

            migrationBuilder.DropTable(
                name: "Massage");

            migrationBuilder.DropTable(
                name: "MemberAndMemberChats");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropColumn(
                name: "PhotoProfileRoute",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoProfileRoute",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FriendsAndFollowingUser",
                columns: table => new
                {
                    FollowingUserId = table.Column<string>(type: "text", nullable: false),
                    UserFriendsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendsAndFollowingUser", x => new { x.FollowingUserId, x.UserFriendsId });
                    table.ForeignKey(
                        name: "FK_FriendsAndFollowingUser_AspNetUsers_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendsAndFollowingUser_AspNetUsers_UserFriendsId",
                        column: x => x.UserFriendsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreaterId = table.Column<string>(type: "text", nullable: true),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    Liked = table.Column<int>(type: "integer", nullable: false),
                    RouteToPhoto = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreaterId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Massage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreaterId = table.Column<string>(type: "text", nullable: true),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UsingChatId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Massage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Massage_AspNetUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Massage_Chat_UsingChatId",
                        column: x => x.UsingChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Chat_PostId",
                table: "Chat",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendsAndFollowingUser_UserFriendsId",
                table: "FriendsAndFollowingUser",
                column: "UserFriendsId");

            migrationBuilder.CreateIndex(
                name: "IX_Massage_CreaterId",
                table: "Massage",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Massage_UsingChatId",
                table: "Massage",
                column: "UsingChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberAndMemberChats_MembersId",
                table: "MemberAndMemberChats",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreaterId",
                table: "Post",
                column: "CreaterId");
        }
    }
}

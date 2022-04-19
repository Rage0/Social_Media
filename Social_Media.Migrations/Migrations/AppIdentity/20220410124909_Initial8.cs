using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations.AppIdentity
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreaterId = table.Column<string>(type: "text", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsOnlyFriends = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Massage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UsingChatId = table.Column<Guid>(type: "uuid", nullable: true),
                    PrivateChatId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreaterId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Massage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Massage_AspNetUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Massage_Chat_UsingChatId",
                        column: x => x.UsingChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Massage_PrivateChat_PrivateChatId",
                        column: x => x.PrivateChatId,
                        principalTable: "PrivateChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrivateChatUser",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "text", nullable: false),
                    PrivateChatsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChatUser", x => new { x.MembersId, x.PrivateChatsId });
                    table.ForeignKey(
                        name: "FK_PrivateChatUser_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateChatUser_PrivateChat_PrivateChatsId",
                        column: x => x.PrivateChatsId,
                        principalTable: "PrivateChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Massage_CreaterId",
                table: "Massage",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Massage_PrivateChatId",
                table: "Massage",
                column: "PrivateChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Massage_UsingChatId",
                table: "Massage",
                column: "UsingChatId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChatUser_PrivateChatsId",
                table: "PrivateChatUser",
                column: "PrivateChatsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Massage");

            migrationBuilder.DropTable(
                name: "PrivateChatUser");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "PrivateChat");
        }
    }
}

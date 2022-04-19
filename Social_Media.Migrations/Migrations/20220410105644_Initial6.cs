using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberAndMemberChats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_MemberAndMemberChats_MembersId",
                table: "MemberAndMemberChats",
                column: "MembersId");
        }
    }
}

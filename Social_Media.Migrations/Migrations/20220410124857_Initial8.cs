using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrivateChatId",
                table: "Massages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrivateChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembersToPrivateChat",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "text", nullable: false),
                    PrivateChatsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersToPrivateChat", x => new { x.MembersId, x.PrivateChatsId });
                    table.ForeignKey(
                        name: "FK_MembersToPrivateChat_PrivateChats_PrivateChatsId",
                        column: x => x.PrivateChatsId,
                        principalTable: "PrivateChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersToPrivateChat_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Massages_PrivateChatId",
                table: "Massages",
                column: "PrivateChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToPrivateChat_PrivateChatsId",
                table: "MembersToPrivateChat",
                column: "PrivateChatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_PrivateChats_PrivateChatId",
                table: "Massages",
                column: "PrivateChatId",
                principalTable: "PrivateChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_PrivateChats_PrivateChatId",
                table: "Massages");

            migrationBuilder.DropTable(
                name: "MembersToPrivateChat");

            migrationBuilder.DropTable(
                name: "PrivateChats");

            migrationBuilder.DropIndex(
                name: "IX_Massages_PrivateChatId",
                table: "Massages");

            migrationBuilder.DropColumn(
                name: "PrivateChatId",
                table: "Massages");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media.Migrations.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_BaseEntity_Id",
                table: "Massages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BaseEntity_Id",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "BaseEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Liked",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RouteToPhoto",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Massages",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Massages",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Massages",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsingChatId",
                table: "Massages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Massages_UsingChatId",
                table: "Massages",
                column: "UsingChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_PostId",
                table: "Chats",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Posts_PostId",
                table: "Chats",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_Chats_UsingChatId",
                table: "Massages",
                column: "UsingChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Posts_PostId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Massages_Chats_UsingChatId",
                table: "Massages");

            migrationBuilder.DropIndex(
                name: "IX_Massages_UsingChatId",
                table: "Massages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_PostId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Liked",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RouteToPhoto",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Massages");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Massages");

            migrationBuilder.DropColumn(
                name: "UsingChatId",
                table: "Massages");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Chats");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Posts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Massages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Liked = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UsingChatId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseEntity_Chats_UsingChatId",
                        column: x => x.UsingChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_UsingChatId",
                table: "BaseEntity",
                column: "UsingChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_BaseEntity_Id",
                table: "Massages",
                column: "Id",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BaseEntity_Id",
                table: "Posts",
                column: "Id",
                principalTable: "BaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

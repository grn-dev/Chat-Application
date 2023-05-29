using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class adddirectuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Messages_MessageId",
                table: "Direct");

            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Users_RecipientId",
                table: "Direct");

            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Users_SenderId",
                table: "Direct");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Direct_RecipientId",
                table: "Direct");

            migrationBuilder.DropIndex(
                name: "IX_Direct_SenderId",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "AfkNote",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Flag",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAfk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastNudged",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RawPreferences",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RequestPasswordResetId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RequestPasswordResetValidThrough",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TicketcategoriId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "IsForwarded",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Direct");

            migrationBuilder.AlterColumn<int>(
                name: "TicketType",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DirectId",
                table: "DirectUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Direct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Direct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Direct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Direct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DirectMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectId = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    IsForwarded = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectMessage_Direct_DirectId",
                        column: x => x.DirectId,
                        principalTable: "Direct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DirectMessage_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectMessage_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_DirectId",
                table: "DirectUser",
                column: "DirectId");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_UserId",
                table: "Direct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_UserId1",
                table: "Direct",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_DirectId",
                table: "DirectMessage",
                column: "DirectId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_MessageId",
                table: "DirectMessage",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_SenderId",
                table: "DirectMessage",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Messages_MessageId",
                table: "Direct",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Users_UserId",
                table: "Direct",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Users_UserId1",
                table: "Direct",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Messages_MessageId",
                table: "Direct");

            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Users_UserId",
                table: "Direct");

            migrationBuilder.DropForeignKey(
                name: "FK_Direct_Users_UserId1",
                table: "Direct");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser");

            migrationBuilder.DropTable(
                name: "DirectMessage");

            migrationBuilder.DropIndex(
                name: "IX_DirectUser_DirectId",
                table: "DirectUser");

            migrationBuilder.DropIndex(
                name: "IX_Direct_UserId",
                table: "Direct");

            migrationBuilder.DropIndex(
                name: "IX_Direct_UserId1",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "DirectId",
                table: "DirectUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Direct");

            migrationBuilder.AddColumn<string>(
                name: "AfkNote",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flag",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAfk",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastNudged",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawPreferences",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestPasswordResetId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RequestPasswordResetValidThrough",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TicketType",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketcategoriId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Direct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForwarded",
                table: "Direct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Direct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Direct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Direct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_ChatRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direct_RecipientId",
                table: "Direct",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_SenderId",
                table: "Direct",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MessageId",
                table: "Notifications",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RoomId",
                table: "Notifications",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Messages_MessageId",
                table: "Direct",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Users_RecipientId",
                table: "Direct",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direct_Users_SenderId",
                table: "Direct",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

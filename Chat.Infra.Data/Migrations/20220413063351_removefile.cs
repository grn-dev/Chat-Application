using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class removefile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessage_Direct_DirectId",
                table: "DirectMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessage_Messages_MessageId",
                table: "DirectMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessage_Users_SenderId",
                table: "DirectMessage");

            migrationBuilder.DropTable(
                name: "ChatClients");

            migrationBuilder.DropTable(
                name: "ChatUserIdentities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectMessage",
                table: "DirectMessage");

            migrationBuilder.RenameTable(
                name: "DirectMessage",
                newName: "DirectMessages");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessage_SenderId",
                table: "DirectMessages",
                newName: "IX_DirectMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessage_MessageId",
                table: "DirectMessages",
                newName: "IX_DirectMessages_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessage_DirectId",
                table: "DirectMessages",
                newName: "IX_DirectMessages_DirectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectMessages",
                table: "DirectMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Direct_DirectId",
                table: "DirectMessages",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Messages_MessageId",
                table: "DirectMessages",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Users_SenderId",
                table: "DirectMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Direct_DirectId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Messages_MessageId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Users_SenderId",
                table: "DirectMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectMessages",
                table: "DirectMessages");

            migrationBuilder.RenameTable(
                name: "DirectMessages",
                newName: "DirectMessage");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessages_SenderId",
                table: "DirectMessage",
                newName: "IX_DirectMessage_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessages_MessageId",
                table: "DirectMessage",
                newName: "IX_DirectMessage_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessages_DirectId",
                table: "DirectMessage",
                newName: "IX_DirectMessage_DirectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectMessage",
                table: "DirectMessage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastActivity = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastClientActivity = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatClients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatUserIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUserIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUserIdentities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatClients_UserId",
                table: "ChatClients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserIdentities_UserId",
                table: "ChatUserIdentities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessage_Direct_DirectId",
                table: "DirectMessage",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessage_Messages_MessageId",
                table: "DirectMessage",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessage_Users_SenderId",
                table: "DirectMessage",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

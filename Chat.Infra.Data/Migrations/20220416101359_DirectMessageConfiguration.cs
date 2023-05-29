using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class DirectMessageConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_DirectMessages_Messages_MessageId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Users_SenderId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Users_UserId1",
                table: "DirectUser");

            migrationBuilder.DropIndex(
                name: "IX_DirectUser_UserId1",
                table: "DirectUser");

            migrationBuilder.DropIndex(
                name: "IX_Direct_MessageId",
                table: "Direct");

            migrationBuilder.DropIndex(
                name: "IX_Direct_UserId",
                table: "Direct");

            migrationBuilder.DropIndex(
                name: "IX_Direct_UserId1",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DirectUser");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Direct");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Direct");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DirectMessages",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Messages_MessageId",
                table: "DirectMessages",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Users_SenderId",
                table: "DirectMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Messages_MessageId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Users_SenderId",
                table: "DirectMessages");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "DirectUser",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "DirectMessages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Direct",
                type: "int",
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

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_UserId1",
                table: "DirectUser",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_MessageId",
                table: "Direct",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_UserId",
                table: "Direct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Direct_UserId1",
                table: "Direct",
                column: "UserId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Users_UserId1",
                table: "DirectUser",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

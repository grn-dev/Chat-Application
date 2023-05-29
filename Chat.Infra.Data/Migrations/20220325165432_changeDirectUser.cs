using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class changeDirectUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Users_UserId",
                table: "DirectUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DirectUser",
                newName: "SourceUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectUser_UserId",
                table: "DirectUser",
                newName: "IX_DirectUser_SourceUserId");

            migrationBuilder.AlterColumn<int>(
                name: "DirectId",
                table: "DirectUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DestinationUserId",
                table: "DirectUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_DestinationUserId",
                table: "DirectUser",
                column: "DestinationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Users_DestinationUserId",
                table: "DirectUser",
                column: "DestinationUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Users_SourceUserId",
                table: "DirectUser",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Users_DestinationUserId",
                table: "DirectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Users_SourceUserId",
                table: "DirectUser");

            migrationBuilder.DropIndex(
                name: "IX_DirectUser_DestinationUserId",
                table: "DirectUser");

            migrationBuilder.DropColumn(
                name: "DestinationUserId",
                table: "DirectUser");

            migrationBuilder.RenameColumn(
                name: "SourceUserId",
                table: "DirectUser",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectUser_SourceUserId",
                table: "DirectUser",
                newName: "IX_DirectUser_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "DirectId",
                table: "DirectUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Users_UserId",
                table: "DirectUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

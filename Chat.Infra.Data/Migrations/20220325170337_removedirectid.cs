using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class removedirectid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser");

            migrationBuilder.DropIndex(
                name: "IX_DirectUser_DirectId",
                table: "DirectUser");

            migrationBuilder.DropColumn(
                name: "DirectId",
                table: "DirectUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectId",
                table: "DirectUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_DirectId",
                table: "DirectUser",
                column: "DirectId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectUser_Direct_DirectId",
                table: "DirectUser",
                column: "DirectId",
                principalTable: "Direct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

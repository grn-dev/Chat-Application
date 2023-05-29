using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class directuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Show = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DirectId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectUser_Direct_DirectId",
                        column: x => x.DirectId,
                        principalTable: "Direct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DirectUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_DirectId",
                table: "DirectUser",
                column: "DirectId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectUser_UserId",
                table: "DirectUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectUser");
        }
    }
}

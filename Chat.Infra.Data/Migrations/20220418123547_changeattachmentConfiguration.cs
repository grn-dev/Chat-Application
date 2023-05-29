using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Infra.Data.Migrations
{
    public partial class changeattachmentConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Messages_MessageId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_MessageId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Attachments");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AttachmentId",
                table: "Messages",
                column: "AttachmentId",
                unique: true,
                filter: "[AttachmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Attachments_AttachmentId",
                table: "Messages",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Attachments_AttachmentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AttachmentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageId",
                table: "Attachments",
                column: "MessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Messages_MessageId",
                table: "Attachments",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}

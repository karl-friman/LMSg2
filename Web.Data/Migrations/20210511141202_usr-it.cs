using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class usrit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_LMSUserId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_LMSUserId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "LMSUserId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LMSUserId1",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LMSUserId1",
                table: "Documents",
                column: "LMSUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_LMSUserId1",
                table: "Documents",
                column: "LMSUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_LMSUserId1",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_LMSUserId1",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LMSUserId1",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "LMSUserId",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LMSUserId",
                table: "Documents",
                column: "LMSUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_LMSUserId",
                table: "Documents",
                column: "LMSUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

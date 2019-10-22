using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class UserIdFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Copies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserId",
                table: "Copies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");
        }
    }
}

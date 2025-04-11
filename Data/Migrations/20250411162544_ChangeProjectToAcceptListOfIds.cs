using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProjectToAcceptListOfIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_MemberId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_MemberId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectEntityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectEntityId",
                table: "AspNetUsers",
                column: "ProjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectEntityId",
                table: "AspNetUsers",
                column: "ProjectEntityId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectEntityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_MemberId",
                table: "Projects",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_MemberId",
                table: "Projects",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

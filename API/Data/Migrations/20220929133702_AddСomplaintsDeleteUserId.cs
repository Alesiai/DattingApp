using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddСomplaintsDeleteUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Сomplaints_AspNetUsers_AppUserId",
                table: "Сomplaints");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Сomplaints",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Сomplaints_AspNetUsers_AppUserId",
                table: "Сomplaints",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Сomplaints_AspNetUsers_AppUserId",
                table: "Сomplaints");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Сomplaints",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Сomplaints_AspNetUsers_AppUserId",
                table: "Сomplaints",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

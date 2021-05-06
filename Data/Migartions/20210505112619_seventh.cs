using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Data.Migartions
{
    public partial class seventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TaskLists",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskLists");
        }
    }
}

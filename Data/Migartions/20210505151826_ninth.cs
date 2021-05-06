using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Data.Migartions
{
    public partial class ninth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskLists",
                newName: "ListsUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskLists",
                newName: "IX_TaskLists_ListsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_ListsUserId",
                table: "TaskLists",
                column: "ListsUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Users_ListsUserId",
                table: "TaskLists");

            migrationBuilder.RenameColumn(
                name: "ListsUserId",
                table: "TaskLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLists_ListsUserId",
                table: "TaskLists",
                newName: "IX_TaskLists_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Users_UserId",
                table: "TaskLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H3DatabaseProg.Migrations
{
    /// <inheritdoc />
    public partial class todotilworker2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskTaskId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Todos_CurrentTodoTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_CurrentTodoTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentTaskTaskId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "CurrentTodoTodoId",
                table: "Workers",
                newName: "CurrentTodoId");

            migrationBuilder.RenameColumn(
                name: "CurrentTaskTaskId",
                table: "Teams",
                newName: "CurrentTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentTodoId",
                table: "Workers",
                newName: "CurrentTodoTodoId");

            migrationBuilder.RenameColumn(
                name: "CurrentTaskId",
                table: "Teams",
                newName: "CurrentTaskTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CurrentTodoTodoId",
                table: "Workers",
                column: "CurrentTodoTodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTaskTaskId",
                table: "Teams",
                column: "CurrentTaskTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskTaskId",
                table: "Teams",
                column: "CurrentTaskTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Todos_CurrentTodoTodoId",
                table: "Workers",
                column: "CurrentTodoTodoId",
                principalTable: "Todos",
                principalColumn: "TodoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

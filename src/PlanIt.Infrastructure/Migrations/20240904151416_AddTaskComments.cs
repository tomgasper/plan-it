using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskCommentIds");

            migrationBuilder.DropTable(
                name: "TaskWorkerIds");

            migrationBuilder.CreateTable(
                name: "TaskComment",
                columns: table => new
                {
                    TaskCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComment", x => new { x.ProjectTaskId, x.ProjectId, x.TaskCommentId });
                    table.ForeignKey(
                        name: "FK_TaskComment_ProjectTasks_ProjectTaskId_ProjectId",
                        columns: x => new { x.ProjectTaskId, x.ProjectId },
                        principalTable: "ProjectTasks",
                        principalColumns: new[] { "ProjectTaskId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskWorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskWorker_ProjectTasks_ProjectTaskId_ProjectId",
                        columns: x => new { x.ProjectTaskId, x.ProjectId },
                        principalTable: "ProjectTasks",
                        principalColumns: new[] { "ProjectTaskId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskWorker_ProjectTaskId_ProjectId",
                table: "TaskWorker",
                columns: new[] { "ProjectTaskId", "ProjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComment");

            migrationBuilder.DropTable(
                name: "TaskWorker");

            migrationBuilder.CreateTable(
                name: "TaskCommentIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCommentIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCommentIds_ProjectTasks_ProjectTaskId_ProjectId",
                        columns: x => new { x.ProjectTaskId, x.ProjectId },
                        principalTable: "ProjectTasks",
                        principalColumns: new[] { "ProjectTaskId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskWorkerIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskWorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskWorkerIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskWorkerIds_ProjectTasks_ProjectTaskId_ProjectId",
                        columns: x => new { x.ProjectTaskId, x.ProjectId },
                        principalTable: "ProjectTasks",
                        principalColumns: new[] { "ProjectTaskId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskCommentIds_ProjectTaskId_ProjectId",
                table: "TaskCommentIds",
                columns: new[] { "ProjectTaskId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskWorkerIds_ProjectTaskId_ProjectId",
                table: "TaskWorkerIds",
                columns: new[] { "ProjectTaskId", "ProjectId" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackerPlanerMVC.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    catid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    catname = table.Column<string>(nullable: true),
                    projectid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.catid);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    projectid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    projectname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.projectid);
                });

            migrationBuilder.CreateTable(
                name: "TaskNames",
                columns: table => new
                {
                    taskid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    taskname = table.Column<string>(nullable: true),
                    categoryid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNames", x => x.taskid);
                });

            migrationBuilder.CreateTable(
                name: "TasksDoing",
                columns: table => new
                {
                    workid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    taskid = table.Column<int>(nullable: false),
                    starttime = table.Column<DateTime>(nullable: false),
                    duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksDoing", x => x.workid);
                });

            migrationBuilder.CreateTable(
                name: "TasksPlanned",
                columns: table => new
                {
                    planid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    taskid = table.Column<int>(nullable: false),
                    TasksDoingworkid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksPlanned", x => x.planid);
                    table.ForeignKey(
                        name: "FK_TasksPlanned_TasksDoing_TasksDoingworkid",
                        column: x => x.TasksDoingworkid,
                        principalTable: "TasksDoing",
                        principalColumn: "workid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksPlanned_TasksDoingworkid",
                table: "TasksPlanned",
                column: "TasksDoingworkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TaskNames");

            migrationBuilder.DropTable(
                name: "TasksPlanned");

            migrationBuilder.DropTable(
                name: "TasksDoing");
        }
    }
}

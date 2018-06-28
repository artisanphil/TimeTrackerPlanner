using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TimeTrackerPlanerWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TasksDoing",
                columns: table => new
                {
                    workid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    duration = table.Column<int>(nullable: false),
                    starttime = table.Column<DateTime>(nullable: false),
                    taskid = table.Column<int>(nullable: false)
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
                    TasksDoingworkid = table.Column<int>(nullable: true),
                    taskid = table.Column<int>(nullable: false)
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
                name: "TasksPlanned");

            migrationBuilder.DropTable(
                name: "TasksDoing");
        }
    }
}

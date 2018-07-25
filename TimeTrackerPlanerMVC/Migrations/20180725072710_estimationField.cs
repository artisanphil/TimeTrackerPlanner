using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackerPlanerMVC.Migrations
{
    public partial class estimationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estimationInMinutes",
                table: "TasksPlanned",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "week",
                table: "TasksPlanned",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estimationInMinutes",
                table: "TasksPlanned");

            migrationBuilder.DropColumn(
                name: "week",
                table: "TasksPlanned");
        }
    }
}

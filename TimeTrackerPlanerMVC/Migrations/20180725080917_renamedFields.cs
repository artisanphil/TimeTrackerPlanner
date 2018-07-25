using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackerPlanerMVC.Migrations
{
    public partial class renamedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "week",
                table: "TasksPlanned",
                newName: "planneddate");

            migrationBuilder.RenameColumn(
                name: "estimationInMinutes",
                table: "TasksPlanned",
                newName: "estimation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "planneddate",
                table: "TasksPlanned",
                newName: "week");

            migrationBuilder.RenameColumn(
                name: "estimation",
                table: "TasksPlanned",
                newName: "estimationInMinutes");
        }
    }
}

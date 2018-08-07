using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTrackerPlanerMVC.Migrations
{
    public partial class completedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "TasksPlanned",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed",
                table: "TasksPlanned");
        }
    }
}

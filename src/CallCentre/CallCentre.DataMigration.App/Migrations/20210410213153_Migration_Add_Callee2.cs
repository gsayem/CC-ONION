using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CallCentre.DataMigration.App.Migrations
{
    public partial class Migration_Add_Callee2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "CostingTimeSlots");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "CostingTimeSlots");

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "CostingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndMinute",
                table: "CostingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "CostingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartMinute",
                table: "CostingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "CostingTimeSlots");

            migrationBuilder.DropColumn(
                name: "EndMinute",
                table: "CostingTimeSlots");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "CostingTimeSlots");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "CostingTimeSlots");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "CostingTimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "CostingTimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

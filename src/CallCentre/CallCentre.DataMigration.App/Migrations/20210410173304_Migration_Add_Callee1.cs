using Microsoft.EntityFrameworkCore.Migrations;

namespace CallCentre.DataMigration.App.Migrations
{
    public partial class Migration_Add_Callee1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallDuration",
                table: "CalleeCallDurations");

            migrationBuilder.AddColumn<long>(
                name: "CallDurationInSecond",
                table: "CalleeCallDurations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallDurationInSecond",
                table: "CalleeCallDurations");

            migrationBuilder.AddColumn<string>(
                name: "CallDuration",
                table: "CalleeCallDurations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

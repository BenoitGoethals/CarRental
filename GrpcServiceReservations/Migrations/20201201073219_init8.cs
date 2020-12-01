using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrpcServiceReservations.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Car_CarId",
                table: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_CarId",
                table: "TimeSlots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InCirculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Km = table.Column<int>(type: "int", nullable: false),
                    LastMaintenace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMaintenace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Operational = table.Column<bool>(type: "bit", nullable: false),
                    PlateNbr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_CarId",
                table: "TimeSlots",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Car_CarId",
                table: "TimeSlots",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

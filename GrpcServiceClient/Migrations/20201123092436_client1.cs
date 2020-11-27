using Microsoft.EntityFrameworkCore.Migrations;

namespace GrpcServiceClient.Migrations
{
    public partial class client1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nbr",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nbr",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Clients");
        }
    }
}

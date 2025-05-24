using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mobile_api.Migrations
{
    /// <inheritdoc />
    public partial class FixService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "TourServices",
                columns: table => new
                {
                    ServicesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToursId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourServices", x => new { x.ServicesId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_TourServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourServices_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourServices_ToursId",
                table: "TourServices",
                column: "ToursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourServices");

            migrationBuilder.AddColumn<string>(
                name: "TourId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

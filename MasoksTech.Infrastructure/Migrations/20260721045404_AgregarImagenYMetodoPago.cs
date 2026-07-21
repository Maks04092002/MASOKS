using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasoksTech.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarImagenYMetodoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "Ordenes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "Ordenes");
        }
    }
}

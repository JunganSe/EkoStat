using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkoStatApi.data.migrations
{
    /// <inheritdoc />
    public partial class RemovedNameFromEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Entries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Entries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

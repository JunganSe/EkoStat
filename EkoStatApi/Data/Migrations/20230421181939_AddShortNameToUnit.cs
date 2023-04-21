using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkoStatApi.data.migrations
{
    /// <inheritdoc />
    public partial class AddShortNameToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Units",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Units");
        }
    }
}

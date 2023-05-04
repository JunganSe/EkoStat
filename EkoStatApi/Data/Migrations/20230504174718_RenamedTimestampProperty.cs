using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkoStatApi.data.migrations
{
    /// <inheritdoc />
    public partial class RenamedTimestampProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Entries",
                newName: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Entries",
                newName: "TimeStamp");
        }
    }
}

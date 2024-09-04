using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateLink : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Links",
            columns: table => new
            {
                Id = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Links", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Links");
    }
}

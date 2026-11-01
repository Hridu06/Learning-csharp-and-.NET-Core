using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalExpenseTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAppearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Existing user-created categories get the neutral default appearance.
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ExpenseCategories",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "#6366F1");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "ExpenseCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "tag");

            migrationBuilder.UpdateData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Icon" },
                values: new object[] { "#F97316", "basket" });

            migrationBuilder.UpdateData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "Icon" },
                values: new object[] { "#0EA5E9", "bus-front" });

            migrationBuilder.UpdateData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "Icon" },
                values: new object[] { "#EAB308", "lightning-charge" });

            migrationBuilder.UpdateData(
                table: "ExpenseCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "Icon" },
                values: new object[] { "#A855F7", "film" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "ExpenseCategories");
        }
    }
}

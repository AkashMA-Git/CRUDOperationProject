using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDOperationProject.Migrations
{
    /// <inheritdoc />
    public partial class DummyDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Department", "Description", "Email", "Gender", "Name" },
                values: new object[] { 1, "1993-09-02", "IT", "Junior Developer", "abiakash1993@gmail.com", 0, "Akash" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class AddedNewRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01985a37-f9c5-7676-93c6-fd7259f4b646", "01985a37-f9c5-7676-93c6-fd73c880f710", false, false, "Teacher", "TEACHER" },
                    { "01985a37-f9c5-7676-93c6-fd740ea964e2", "01985a37-f9c5-7676-93c6-fd75e1ce1428", false, false, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01985a37-f9c5-7676-93c6-fd7259f4b646");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01985a37-f9c5-7676-93c6-fd740ea964e2");
        }
    }
}

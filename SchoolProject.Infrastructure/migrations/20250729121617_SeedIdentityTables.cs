using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019855ea-fef8-708d-ab80-da9e81d2325c", "019855ea-fef8-708d-ab80-da9f845c911f", false, false, "Admin", "ADMIN" },
                    { "019855ea-fef8-708d-ab80-daa013145d98", "019855ea-fef8-708d-ab80-daa156960d15", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019855ea-fef6-7aea-a2ee-4e83500e2169", 0, "019855ea-fef8-708d-ab80-da9da4ab679d", "Admin@School_System.com", true, "School System", false, "Admin", false, null, "ADMIN@SCHOOL_SYSTEM.COM", "ADMIN@SCHOOL_SYSTEM.COM", "AQAAAAIAAYagAAAAEA1BaaCL1VGAUJ790IV54viXXybSzkIIQvPPHk+pmTl2JYBiq/iD8UPMntaZL4Xvpw==", null, false, "0D4D736497244D628D7C32045E26BA51", false, "Admin@School_System.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019855ea-fef8-708d-ab80-da9e81d2325c", "019855ea-fef6-7aea-a2ee-4e83500e2169" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019855ea-fef8-708d-ab80-daa013145d98");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019855ea-fef8-708d-ab80-da9e81d2325c", "019855ea-fef6-7aea-a2ee-4e83500e2169" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019855ea-fef8-708d-ab80-da9e81d2325c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019855ea-fef6-7aea-a2ee-4e83500e2169");
        }
    }
}

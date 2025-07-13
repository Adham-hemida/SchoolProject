using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToDepartmentSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DepartmentId_SubjectId",
                table: "DepartmentSubjects",
                columns: new[] { "DepartmentId", "SubjectId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DepartmentId_SubjectId",
                table: "DepartmentSubjects");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects",
                column: "DepartmentId");
        }
    }
}

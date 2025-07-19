using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentSubjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SubjectId1",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SubjectId1",
                table: "Assignments",
                column: "SubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_SubjectId1",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "Assignments");
        }
    }
}

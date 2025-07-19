using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentFileAttachmentsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_FileAttachments_FileAttachmentId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSubjects_Department_DepartmentId",
                table: "DepartmentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSubjects_Subjects_SubjectId",
                table: "DepartmentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_Assignments_AssignmentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_FileAttachments_FileAttachmentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_Students_StudentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_FileAttachmentId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "FileAttachmentId",
                table: "Assignments");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_AssignmentId",
                table: "FileAttachments",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSubjects_Department_DepartmentId",
                table: "DepartmentSubjects",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSubjects_Subjects_SubjectId",
                table: "DepartmentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Assignments_AssignmentId",
                table: "FileAttachments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_Assignments_AssignmentId",
                table: "StudentSubmissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_FileAttachments_FileAttachmentId",
                table: "StudentSubmissions",
                column: "FileAttachmentId",
                principalTable: "FileAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_Students_StudentId",
                table: "StudentSubmissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSubjects_Department_DepartmentId",
                table: "DepartmentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSubjects_Subjects_SubjectId",
                table: "DepartmentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Assignments_AssignmentId",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_Assignments_AssignmentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_FileAttachments_FileAttachmentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubmissions_Students_StudentId",
                table: "StudentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_AssignmentId",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "FileAttachments");

            migrationBuilder.AddColumn<Guid>(
                name: "FileAttachmentId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_FileAttachmentId",
                table: "Assignments",
                column: "FileAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_FileAttachments_FileAttachmentId",
                table: "Assignments",
                column: "FileAttachmentId",
                principalTable: "FileAttachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Subjects_SubjectId1",
                table: "Assignments",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSubjects_Department_DepartmentId",
                table: "DepartmentSubjects",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSubjects_Subjects_SubjectId",
                table: "DepartmentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentId",
                table: "StudentSubjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_Assignments_AssignmentId",
                table: "StudentSubmissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_FileAttachments_FileAttachmentId",
                table: "StudentSubmissions",
                column: "FileAttachmentId",
                principalTable: "FileAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubmissions_Students_StudentId",
                table: "StudentSubmissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

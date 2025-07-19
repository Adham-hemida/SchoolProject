using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.migrations
{
    /// <inheritdoc />
    public partial class FixSubjectAssignmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Assignments_Subjects_SubjectId1",
            //    table: "Assignments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Assignments_SubjectId1",
            //    table: "Assignments");

            //migrationBuilder.DropColumn(
            //    name: "SubjectId1",
            //    table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Assignments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SubjectId",
                table: "Assignments",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Subjects_SubjectId",
                table: "Assignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Assignments_Subjects_SubjectId",
            //    table: "Assignments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Assignments_SubjectId",
            //    table: "Assignments");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "SubjectId",
            //    table: "Assignments",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}

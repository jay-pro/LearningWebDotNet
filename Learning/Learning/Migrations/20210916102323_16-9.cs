using Microsoft.EntityFrameworkCore.Migrations;

namespace lms.Migrations
{
    public partial class _169 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassIDClass",
                table: "Mentor",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "CourseDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "IDClass",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "IDTeacher",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mentor_ClassIDClass",
                table: "Mentor",
                column: "ClassIDClass");

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_LessonId",
                table: "CourseDetails",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_ClassId",
                table: "Lesson",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TeacherId",
                table: "Lesson",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDetails_Lesson_LessonId",
                table: "CourseDetails",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mentor_Class_ClassIDClass",
                table: "Mentor",
                column: "ClassIDClass",
                principalTable: "Class",
                principalColumn: "IDClass",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDetails_Lesson_LessonId",
                table: "CourseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Mentor_Class_ClassIDClass",
                table: "Mentor");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Mentor_ClassIDClass",
                table: "Mentor");

            migrationBuilder.DropIndex(
                name: "IX_CourseDetails_LessonId",
                table: "CourseDetails");

            migrationBuilder.DropColumn(
                name: "ClassIDClass",
                table: "Mentor");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "CourseDetails");
        }
    }
}

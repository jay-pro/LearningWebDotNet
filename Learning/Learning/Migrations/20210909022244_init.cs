using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lms.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notify",
                columns: table => new
                {
                    IDNotify = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CretatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notify", x => x.IDNotify);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoreBroad",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Final = table.Column<int>(type: "int", nullable: false),
                    TotalQuizz = table.Column<int>(type: "int", nullable: false),
                    Process = table.Column<int>(type: "int", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreBroad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Notify_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Notify",
                        principalColumn: "IDNotify",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    IDAttendance = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isChecked = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.IDAttendance);
                    table.ForeignKey(
                        name: "FK_Attendance_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassAdmin",
                columns: table => new
                {
                    IDClassAdmin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAdmin", x => x.IDClassAdmin);
                    table.ForeignKey(
                        name: "FK_ClassAdmin_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    IDComment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.IDComment);
                    table.ForeignKey(
                        name: "FK_Comment_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    IDInstuctor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.IDInstuctor);
                    table.ForeignKey(
                        name: "FK_Instructor_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    IDMentor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor", x => x.IDMentor);
                    table.ForeignKey(
                        name: "FK_Mentor_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizzScore",
                columns: table => new
                {
                    IDQuizzScore = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDQuizz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzScore", x => x.IDQuizzScore);
                    table.ForeignKey(
                        name: "FK_QuizzScore_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IDStudent = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isChecked = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.IDStudent);
                    table.ForeignKey(
                        name: "FK_Student_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemAdmin",
                columns: table => new
                {
                    IDSystemAdmin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAdmin", x => x.IDSystemAdmin);
                    table.ForeignKey(
                        name: "FK_SystemAdmin_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    IDTeacher = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.IDTeacher);
                    table.ForeignKey(
                        name: "FK_Teacher_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    IDReply = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CretatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDComment = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.IDReply);
                    table.ForeignKey(
                        name: "FK_Reply_Comment_IDComment",
                        column: x => x.IDComment,
                        principalTable: "Comment",
                        principalColumn: "IDComment",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reply_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    IDCourse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    RegistedNumber = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IDCreator = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.IDCourse);
                    table.ForeignKey(
                        name: "FK_Course_Instructor_IDCreator",
                        column: x => x.IDCreator,
                        principalTable: "Instructor",
                        principalColumn: "IDInstuctor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    IDClass = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDTeacher = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IDCourse = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IDCreator = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.IDClass);
                    table.ForeignKey(
                        name: "FK_Class_Course_IDCourse",
                        column: x => x.IDCourse,
                        principalTable: "Course",
                        principalColumn: "IDCourse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_Instructor_IDCreator",
                        column: x => x.IDCreator,
                        principalTable: "Instructor",
                        principalColumn: "IDInstuctor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_Teacher_IDTeacher",
                        column: x => x.IDTeacher,
                        principalTable: "Teacher",
                        principalColumn: "IDTeacher",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseDetails",
                columns: table => new
                {
                    IDCourseDetail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LessonLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCourse = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetails", x => x.IDCourseDetail);
                    table.ForeignKey(
                        name: "FK_CourseDetails_Course_IDCourse",
                        column: x => x.IDCourse,
                        principalTable: "Course",
                        principalColumn: "IDCourse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseFeedback",
                columns: table => new
                {
                    IDCourseFeedback = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Star = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IDCourse = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeedback", x => x.IDCourseFeedback);
                    table.ForeignKey(
                        name: "FK_CourseFeedback_Course_IDCourse",
                        column: x => x.IDCourse,
                        principalTable: "Course",
                        principalColumn: "IDCourse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseFeedback_User_IDUser",
                        column: x => x.IDUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    IDAssignemnt = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDClass = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.IDAssignemnt);
                    table.ForeignKey(
                        name: "FK_Assignment_Class_IDClass",
                        column: x => x.IDClass,
                        principalTable: "Class",
                        principalColumn: "IDClass",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    IDEnrollment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDStudent = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IDClass = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.IDEnrollment);
                    table.ForeignKey(
                        name: "FK_Enrollment_Class_IDClass",
                        column: x => x.IDClass,
                        principalTable: "Class",
                        principalColumn: "IDClass",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_IDStudent",
                        column: x => x.IDStudent,
                        principalTable: "Student",
                        principalColumn: "IDStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentFeedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentFeedbacks_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "IDClass",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentFeedbacks_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "IDStudent",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentFeedbacks_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "IDTeacher",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizz",
                columns: table => new
                {
                    IDQuizz = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuizzName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    TotalQuestion = table.Column<int>(type: "int", nullable: false),
                    IDCourseDetail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizz", x => x.IDQuizz);
                    table.ForeignKey(
                        name: "FK_Quizz_CourseDetails_IDCourseDetail",
                        column: x => x.IDCourseDetail,
                        principalTable: "CourseDetails",
                        principalColumn: "IDCourseDetail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSubmission",
                columns: table => new
                {
                    IDAssignmentSubmission = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDStudent = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IDAssignemnt = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmission", x => x.IDAssignmentSubmission);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmission_Assignment_IDAssignemnt",
                        column: x => x.IDAssignemnt,
                        principalTable: "Assignment",
                        principalColumn: "IDAssignemnt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmission_Student_IDStudent",
                        column: x => x.IDStudent,
                        principalTable: "Student",
                        principalColumn: "IDStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizzDetail",
                columns: table => new
                {
                    IDQuizzDetail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AChoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BChoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CChoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DChoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDQuizz = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzDetail", x => x.IDQuizzDetail);
                    table.ForeignKey(
                        name: "FK_QuizzDetail_Quizz_IDQuizz",
                        column: x => x.IDQuizz,
                        principalTable: "Quizz",
                        principalColumn: "IDQuizz",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_IDClass",
                table: "Assignment",
                column: "IDClass");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmission_IDAssignemnt",
                table: "AssignmentSubmission",
                column: "IDAssignemnt");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmission_IDStudent",
                table: "AssignmentSubmission",
                column: "IDStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_IDUser",
                table: "Attendance",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Class_IDCourse",
                table: "Class",
                column: "IDCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Class_IDCreator",
                table: "Class",
                column: "IDCreator");

            migrationBuilder.CreateIndex(
                name: "IX_Class_IDTeacher",
                table: "Class",
                column: "IDTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAdmin_IDUser",
                table: "ClassAdmin",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IDUser",
                table: "Comment",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Course_IDCreator",
                table: "Course",
                column: "IDCreator");

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_IDCourse",
                table: "CourseDetails",
                column: "IDCourse");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedback_IDCourse",
                table: "CourseFeedback",
                column: "IDCourse");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedback_IDUser",
                table: "CourseFeedback",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_IDClass",
                table: "Enrollment",
                column: "IDClass");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_IDStudent",
                table: "Enrollment",
                column: "IDStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_IDUser",
                table: "Instructor",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Mentor_IDUser",
                table: "Mentor",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_IDCourseDetail",
                table: "Quizz",
                column: "IDCourseDetail");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzDetail_IDQuizz",
                table: "QuizzDetail",
                column: "IDQuizz");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzScore_IDUser",
                table: "QuizzScore",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_IDComment",
                table: "Reply",
                column: "IDComment");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_IDUser",
                table: "Reply",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Student_UserId",
                table: "Student",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeedbacks_ClassId",
                table: "StudentFeedbacks",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeedbacks_StudentId",
                table: "StudentFeedbacks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeedbacks_TeacherId",
                table: "StudentFeedbacks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemAdmin_IDUser",
                table: "SystemAdmin",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_IDUser",
                table: "Teacher",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_User_IDUser",
                table: "User",
                column: "IDUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSubmission");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "ClassAdmin");

            migrationBuilder.DropTable(
                name: "CourseFeedback");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "QuizzDetail");

            migrationBuilder.DropTable(
                name: "QuizzScore");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "ScoreBroad");

            migrationBuilder.DropTable(
                name: "StudentFeedbacks");

            migrationBuilder.DropTable(
                name: "SystemAdmin");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Quizz");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "CourseDetails");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Notify");
        }
    }
}

using lms.Models;
using lms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = ("Student"))]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly IStorageService _storageService;
        public StudentController(IStorageService storageService, LMSContext context)
        {
            _storageService = storageService;
            _context = context;
        }

        // Đánh giá khóa học 
        [HttpPost]
        [Route("course-assessment")]
        public async Task<ActionResult<Course>> CreateCourseAssessment([FromBody] Course course, [FromQuery] string IDUser, [FromQuery] string IDCourse)
        {
            var temp = await _context.Course.FindAsync(IDCourse);
            if (temp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy IDCourse trong hệ thống",
                    Data = ""
                });
            }
            temp.Rating = course.Rating;
            _context.Entry(temp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(IDCourse))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy course trong hệ thống",
                        Data = temp
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa course thành công",
                Data = temp
            });
        }
        // Chức năng comment của học sinh 

        // Chuc nang lay all comment
        [HttpGet]
        [Route("comment")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComment()
        {
            // Get a comment list 
            var temp = await (from s in _context.Comment
                              join u in _context.User on s.IDUser equals u.Id
                              orderby s.CreatedAt descending
                              select new
                              {
                                  IDUser = s.IDUser,
                                  IDCommnet = s.IDComment,
                                  UserName = u.UserName,
                                  FullName = u.Fullname,
                                  Content = s.Content,
                                  CreatedAt = s.CreatedAt
                              }).ToListAsync();
            //var listCmt = await _context.Comment.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sach Comment học thành công",
                Data = temp
            });
        }
        // Chuc nang lay mot diem
        [HttpGet]
        [Route("comment/{IDComment}")]
        public async Task<ActionResult<Comment>> GetComment(string IDComment)
        {
            var temp = await (from s in _context.Comment
                              join u in _context.User on s.IDUser equals u.Id
                              where s.IDComment == IDComment
                              select  new { 
                                  IDUser = u.Id,
                                  IDComment = s.IDComment,
                                  UserName = u.UserName,
                                  FullName = u.Fullname,
                                  Content = s.Content,
                                  CreatedAt = s.CreatedAt
                              }).FirstOrDefaultAsync();
            //var cmtTemp = await _context.Comment.FindAsync(IDComment);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy Comment thành công",
                Data = temp
            });
        }

        [HttpPost]
        [Route("create-comment")]

        public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment, [FromQuery] string IDUser)
        {
            var cmtTemp = new Comment
            {
                //IDComment = comment.IDComment,
                Content = comment.Content,
                CreatedAt = DateTime.Now,
                IDUser = IDUser
            };
            _context.Comment.Add(cmtTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentExists(cmtTemp.IDComment))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Comment", new { IDComment = cmtTemp.IDComment }, new
            {
                StatusCode = 201,
                Message = "Tạo comment thành công",
                Data = cmtTemp
            });
        }

        // Chuc nang cap nhat mot comment
        [HttpPut]
        [Route("comment/{IDComment}")]
        public async Task<ActionResult<Comment>> PutComment(string IDComment, [FromBody] Comment comment)
        {
            var cmtTemp = await _context.Comment.FindAsync(IDComment);
            if (cmtTemp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy comment trong hệ thống",
                    Data = ""
                });
            }
            //replyTemp.Content = reply.Content;
            cmtTemp.Content = comment.Content;
            _context.Entry(cmtTemp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(IDComment))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy comment trong hệ thống",
                        Data = cmtTemp
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa comment thành công",
                Data = cmtTemp
            });
        }
        // Delete a commnet
        [HttpDelete]
        [Route("comment/{IDComment}")]
        public async Task<IActionResult> DeleteComment(string IDComment)
        {
            var cmt = await _context.Comment.FindAsync(IDComment);
            if (cmt == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy comment trong hệ thống",
                    Data = cmt
                });
            }

            _context.Comment.Remove(cmt);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa comment thành công",
                Data = cmt
            });
        }

        //Đăng ký lớp học
        [HttpPost]
        [Route("register-class")]
        public async Task<ActionResult<Enrollment>> RegisterClass([FromQuery] string IDClass, [FromQuery] string IDUser)
        {
            Student student = await (from u in _context.User
                                     join s in _context.Student on u.Id equals s.IDUser
                                     where u.Id == IDUser
                                     select s).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy học sinh trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            Class classResult = await _context.Class.FindAsync(IDClass);
            if (classResult == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy lớp học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            var checkEnrolllment = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
            if (checkEnrolllment != null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Học viên đã đăng ký vào lớp này",
                    Data = ""
                });
            }
            var enrollment = new Enrollment
            {
                IDStudent = student.IDStudent,
                IDClass = IDClass,
                Progress = 0,
                Score = 0,
            };
            await _context.Enrollment.AddAsync(enrollment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnrollmentExists(enrollment.IDEnrollment))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get enrollment", new { IDEnrollment = enrollment.IDEnrollment }, new
            {
                StatusCode = 201,
                Message = "Đăng ký lớp học thành công",
                Data = enrollment
            });
        }
        //Xem danh sách lớp học theo ID khóa học
        //Get class list
        [HttpGet]
        [Route("get-class-list")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassList([FromQuery] string IDCourse)
        {
            List<ClassDTO> classList = await (from c in _context.Class
                                              join t in _context.Teacher on c.IDTeacher equals t.IDTeacher into Teacher_Class
                                              from teacherclass in Teacher_Class.DefaultIfEmpty()
                                              join u in _context.User on teacherclass.IDUser equals u.Id into Teacher_User
                                              from teacheruser in Teacher_User.DefaultIfEmpty()
                                              join ut in _context.Instructor on c.IDCreator equals ut.IDInstuctor
                                              join uc in _context.User on ut.IDUser equals uc.Id
                                              where  c.IDCourse == IDCourse
                                              select new ClassDTO()
                                              {
                                                  IDClass = c.IDClass,
                                                  ClassName = c.ClassName,
                                                  StartTime = c.StartTime,
                                                  FinishTime = c.FinishTime,
                                                  TeacherName = teacheruser == null ? null : teacheruser.Fullname,
                                                  CreatorName = uc.Fullname
                                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách lớp học thành công",
                Data = classList
            });
        }
        //Xem danh sách lớp học chi tiết
        [HttpGet]
        [Route("get-class-detail/{IDClass}")]
        public async Task<ActionResult<Class>> GetClassDetail(string IDClass)
        {
            Class classResult = await _context.Class.FindAsync(IDClass);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy Lớp học thành công",
                Data = classResult
            });
        }
        //Xem danh sách lớp học đã đăng ký
        [HttpGet]
        [Route("get-registered-class-list")]
        public async Task<ActionResult<IEnumerable<Class>>> GetRegisterdClassList([FromQuery] string IDUser)
        {
            Student student = await (from u in _context.User
                                     join s in _context.Student on u.Id equals s.IDUser
                                     where u.Id == IDUser
                                     select s).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy học sinh trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            List<Class> classList = await (from e in _context.Enrollment
                                           join c in _context.Class on e.IDClass equals c.IDClass
                                           where e.IDStudent == student.IDStudent
                                           select c).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách lớp học đã đăng ký thành công",
                Data = classList
            });
        }
        // Chức năng xem  all feedback
        [HttpGet]
        [Route("feedback/{IDCourse}")]
        public async Task<ActionResult<IEnumerable<CourseFeedback>>> GetCourseFeedback(string IDCourse)
        {
            var temp = await (from u in _context.CourseFeedback
                              where u.IDCourse == IDCourse
                              select u).ToListAsync();
            //var listFb = await _context.CourseFeedback.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh feedback thành công",
                Data = temp
            });
        }
        // Post a course feedback
        [HttpPost]
        [Route("create-coursefeedback")]

        public async Task<ActionResult<CourseFeedback>> CreateCourseFeedback([FromBody] CourseFeedback courseFeedback, [FromQuery] string IDUser, [FromQuery] string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if(course == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy khóa học này"
                });
            }
            var temp = new CourseFeedback
            {
                Content = courseFeedback.Content,
                Star = courseFeedback.Star,
                CreateAt = courseFeedback.CreateAt,
                IDCourse = courseFeedback.IDCourse,
                IDUser = IDUser,
                Course = course
            };
            _context.CourseFeedback.Add(temp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseFeedbackExists(temp.IDCourseFeedback))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get CourseFeedback", new { IDCourseFeedback = temp.IDCourseFeedback }, new
            {
                StatusCode = 201,
                Message = "Tạo CourseFeedback thành công",
                Data = temp
            });
        }


        // Chức năng xem điểm mà giáo viên đánh giá bài tập


        [HttpGet]
        [Route("quizscore/{IDQuizz}")]
        public async Task<ActionResult<IEnumerable<QuizzScore>>> GetQuizzScore()
        {
            var qs = await _context.QuizzScore.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh quizzscore thành công",
                Data = qs
            });
        }

        [HttpGet]
        [Route("quizzscore/{IDQuizz}")]
        public async Task<ActionResult<QuizzScore>> GetQuizzScore(string IDQuizz)
        {
            if (IDQuizz == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy bài IDQuizz trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            // chuwa biet luong ddi 
            var qscore = await _context.QuizzScore.FindAsync(IDQuizz);

            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy QS thành công",
                Data = qscore
            });
        }

        // Chức năng xem chi tiet khóa học
        [HttpGet]
        [Route("course/{IDCourse}/course-detail-list")]
        public async Task<ActionResult<IEnumerable<CourseDetail>>> GetCourseDetailList(string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if (course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            var lstCourseDetail = await (from c in _context.Course
                                         join cd in _context.CourseDetails on c.IDCourse equals cd.IDCourse
                                         where c.IDCourse == IDCourse
                                         select new CourseDetailDTO()
                                         {
                                             IDCourseDetail = cd.IDCourseDetail,
                                             LessonName = cd.LessonName,
                                             LessonLink = cd.LessonLink,
                                             Material = cd.Material,
                                             LessonDuration = cd.LessonDuration
                                         }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách bài học thành công",
                Data = new
                {
                    Course = course,
                    CourseDetailList = lstCourseDetail,
                }
            });
        }

        // Chức năng xem thông tin giáo viên
        [HttpGet]
        [Route("teacher-detail/{IDTeacher}")]
        public async Task<ActionResult<User>> GetTeacherDetail(string IDTeacher)
        {
            // query to get information of teacher
            var temp = await _context.User.FindAsync(IDTeacher);
            if (temp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                    Data = temp
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy mot thông tin giao vien thành công",
                Data = temp
            });
        }
        //Student submit assignment
        [Route("submit-assignment")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile formFile, [FromQuery] string IDAssignment, [FromQuery] string IDUser)
        {
            string fileLink = "";
            if (formFile == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "File nộp bài không được bỏ trống",
                    Data = ""
                });
            }
            var student = (from u in _context.User
                           join s in _context.Student on u.Id equals s.IDUser
                           where u.Id == IDUser
                           select new
                           {
                               IDStudent = s.IDStudent,
                               UserName = u.UserName,
                           }).FirstOrDefault();
            string fileName = IDAssignment + "_" + student.UserName + "_" + formFile.FileName;
            if (AssignmentSubmissionNameExists(fileName))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Bạn đã nộp file này rồi",
                    Data = ""
                });
            }
            fileLink = await _storageService.Upload(formFile, fileName);
            var assignmentSubmission = new AssignmentSubmission()
            {
                FileName = fileName,
                FileLink = fileLink,
                IDStudent = student.IDStudent,
                IDAssignemnt = IDAssignment
            };
            _context.AssignmentSubmission.Add(assignmentSubmission);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssignmentSubmissionExists(assignmentSubmission.IDAssignmentSubmission))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 201,
                Message = "Nộp bài tập thành công",
                Data = new
                {
                    IDAssignmentSubmission = assignmentSubmission.IDAssignmentSubmission,
                    FileName = assignmentSubmission.FileName,
                    fileLink = assignmentSubmission.FileLink,
                    IDUser = IDUser,
                    Username = student.UserName
                }
            });
        }
        private bool AssignmentSubmissionNameExists(string FileName)
        {
            return _context.AssignmentSubmission.Any(n => n.FileName == FileName);
        }
        private bool AssignmentSubmissionExists(string IDAssignmentSubmission)
        {
            return _context.AssignmentSubmission.Any(n => n.IDAssignmentSubmission == IDAssignmentSubmission);
        }

        // Chức năng tìm kiếm khóa học 
        [HttpGet]
        [Route("search-course/{searchString}")]
        public async Task<ActionResult<User>> GetSearchCourse(string searchString)
        {
            if (searchString == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không có gì để tìm kiếm",
                    Data = ""
                });
            }
            var temp = await (from s in _context.Course
                              where s.CourseName.Contains(searchString)
                              select new
                              {
                                  IDCourse = s.IDCourse,
                                  CourseName = s.CourseName,
                                  Description = s.Description,
                                  Image = s.Image,
                                  Duration = s.Duration,
                                  RegistedNumber = s.RegistedNumber,
                                  Rating = s.Rating,
                                  Field = s.Field,
                                  CreatedAt = s.CreatedAt,
                                  //CreatorName = s.Fullname
                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "",
                Data = temp
            });
        }

        //// Chức năng xem các khóa học đã đăng ký
        //[HttpGet]
        //[Route("registered-course/{IDStudent}")]
        //public async Task<ActionResult> GetRegisteredCourse(string IDStudent)
        //{
        //    var student = await _context.Student.FindAsync(IDStudent);
        //    if (student == null)
        //    {
        //        return NotFound(new
        //        {
        //            StatusCode = 404,
        //            Message = "Không tìm thấy học sinh",
        //            Data = ""
        //        });
        //    }
        //    var temp = await (from s in _context.Enrollment
        //                      join i in _context.Class on s.IDClass equals i.IDClass
        //                      join e in _context.Course on i.IDCourse equals e.IDCourse
        //                      where s.IDStudent == IDStudent
        //                      select new
        //                      {
        //                          CourseName = e.CourseName,
        //                          Description = e.Description,
        //                          Rating = e.Rating
        //                      }).ToListAsync();
        //    return Ok(new
        //    {
        //        StatusCode = 200,
        //        Message = "Lấy thông tin khóa học đã đăng ký",
        //        Data = temp
        //    });
        //}

        // Chức năng hủy đăng ký khóa học 
        //[HttpPost]
        //[Route("remove-class")]
        //public async Task<ActionResult<Enrollment>> RemoveClass([FromQuery] string IDClass, [FromQuery] string IDUser)
        //{
        //    Student student = await (from u in _context.User
        //                             join s in _context.Student on u.Id equals s.IDUser
        //                             where u.Id == IDUser
        //                             select s).FirstOrDefaultAsync();
        //    if (student == null)
        //    {
        //        return NotFound(new
        //        {
        //            StatusCode = 404,
        //            Message = "Không tìm thấy học sinh trong cơ sở dữ liệu",
        //            Data = ""
        //        });
        //    }
        //    Class classResult = await _context.Class.FindAsync(IDClass);
        //    if (classResult == null)
        //    {
        //        return NotFound(new
        //        {
        //            StatusCode = 404,
        //            Message = "Không tìm thấy lớp học trong cơ sở dữ liệu",
        //            Data = ""
        //        });
        //    }
        //    var checkEnrolllment = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
        //    if (checkEnrolllment == null)
        //    {
        //        return BadRequest(new
        //        {
        //            StatusCode = 400,
        //            Message = "Học viên chưa đăng ký vào lớp này",
        //            Data = ""
        //        });
        //    }

        //    //var enrollment = _context.Enrollment.Where(s => s.IDStudent == IDUser).FirstOrDefault();
        //    var temp = _context.Enrollment.Remove(checkEnrolllment);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        StatusCode = 200,
        //        Message = "Xoa khoi thành công",
        //        Data = ""
        //    });
        //}

        // Chức năng xem thông kê
        // -- xem đánh giá điểm từ mentor
        //statistic/student-rating
        [HttpGet]
        [Route("statistic/student-rating")]
        public async Task<ActionResult> GetStudentRating([FromQuery] string IDStudent)
        {
            var temp = await (from s in _context.Rating
                              where s.IDUser == IDStudent
                              select new
                              {
                                  Content = s.Content,
                                  Rating = s.rating
                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy điểm đánh gia từ mentor",
                Data = temp
            });
        }
        // -- xem điểm thống kê của các bài quizz trên 50
        //statistic/student-quizzscore
        [HttpGet]
        [Route("statistic/student-quizzscore")]
        public async Task<ActionResult> GetStudentQuizzScore([FromQuery] string IDStudent)
        {
            var temp = await (from s in _context.QuizzScore
                              where s.IDUser == IDStudent
                              select new
                              {
                                  Score = s.Score
                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy điểm thông kê từ các bài quizzscore",
                Data = temp
            });
        }
        // -- xem điểm thống kê của các bài quizz trên 50
        //statistic/student-progress
        [HttpGet]
        [Route("statistic/student-progress")]
        public async Task<ActionResult> GettStudentProgress([FromQuery] string IDStudent)
        {
            var temp = await (from s in _context.QuizzScore
                              where s.IDUser == IDStudent
                              select new
                              {
                                  Score = s.Score
                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy điểm thông kê từ các bài quizzscore",
                Data = temp
            });
        }



        private bool CommentExists(string IDComment)
        {
            return _context.Comment.Any(e => e.IDComment == IDComment);
        }
        private bool CourseFeedbackExists(string IDCourseFeedback)
        {
            return _context.CourseFeedback.Any(e => e.IDCourseFeedback == IDCourseFeedback);
        }
        private bool CourseExists(string IDCourse)
        {
            return _context.Course.Any(e => e.IDCourse == IDCourse);
        }
        private bool EnrollmentExists(string IDEnrollment)
        {
            return _context.Enrollment.Any(e => e.IDEnrollment == IDEnrollment);
        }
    }
}

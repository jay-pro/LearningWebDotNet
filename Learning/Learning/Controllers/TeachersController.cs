using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms.Models;
using Microsoft.AspNetCore.Identity;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly UserManager<User> _userManager;

        public TeachersController(LMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("get-info")]
        public async Task<ActionResult> GetInfo(string userId)
        {
            User u = await _userManager.FindByIdAsync(userId);
            if (u != null)
            {
                return Ok(new
                {
                    Message = "Lấy thông tin thành công",
                    Data = u
                });
            }
            return NotFound();
        }

        [HttpPut("update-info")]
        public async Task<ActionResult> UpdateInfo(User u)
        {
            try {
                await _userManager.UpdateAsync(u);
                return Ok(new
                {
                    Message = "Ok"
                });
            } catch
            {
                return BadRequest();
            }
        }
        [HttpPost("{IDClass}/add-student/{IDUser}")]
        public async Task<ActionResult> AddStudent(string IDClass, string IDUser)
        {
            var student = await _context.Student.Include("User").Where(x => x.IDUser == IDUser).FirstOrDefaultAsync();
            if(student == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy học viên trong cơ sở dữ liệu"
                });
            }
            var cls = await _context.Class.FindAsync(IDClass);
            if(cls == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học trong cơ sở dữ liệu"
                });
            }
            var checkEnrolllment = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
            if (checkEnrolllment != null)
            {
                return Conflict(new
                {
                    Message = "Học viên đã đăng ký vào lớp này"
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
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Thêm thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpDelete("{IDClass}/remove-student/{IDUser}")]
        public async Task<ActionResult> RemoveStudent(string IDClass, string IDUser)
        {
            var student = await _context.Student.Include("User").Where(x => x.IDUser == IDUser).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy học viên trong cơ sở dữ liệu"
                });
            }
            var cls = await _context.Class.FindAsync(IDClass);
            if (cls == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học trong cơ sở dữ liệu"
                });
            }
            var Enroll = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
            if (Enroll == null)
            {
                return Conflict(new
                {
                    Message = "Học viên không có trong lớp học này"
                });
            }
            _context.Enrollment.Remove(Enroll);
            try
            {
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Xóa thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpPost("{IDClass}/add-student-id/{IDStudent}")]
        public async Task<ActionResult> AddStudentID(string IDClass, string IDStudent)
        {
            var student = await _context.Student.Include("User").Where(x => x.IDStudent == IDStudent).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy học viên trong cơ sở dữ liệu"
                });
            }
            var cls = await _context.Class.FindAsync(IDClass);
            if (cls == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học trong cơ sở dữ liệu"
                });
            }
            var checkEnrolllment = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
            if (checkEnrolllment != null)
            {
                return Conflict(new
                {
                    Message = "Học viên đã đăng ký vào lớp này"
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
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Thêm thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpDelete("{IDClass}/remove-student-id/{IDStudent}")]
        public async Task<ActionResult> RemoveStudentID(string IDClass, string IDStudent)
        {
            var student = await _context.Student.Include("User").Where(x => x.IDStudent == IDStudent).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy học viên trong cơ sở dữ liệu"
                });
            }
            var cls = await _context.Class.FindAsync(IDClass);
            if (cls == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học trong cơ sở dữ liệu"
                });
            }
            var Enroll = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == student.IDStudent).FirstOrDefault();
            if (Enroll == null)
            {
                return Conflict(new
                {
                    Message = "Học viên không có trong lớp học này"
                });
            }
            _context.Enrollment.Remove(Enroll);
            try
            {
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Xóa thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpGet("get-students-to-add")]
        public async Task<ActionResult> GetStudentsToAdd(string IDClass)
        {
            if (!await _context.Class.AnyAsync(c => c.IDClass == IDClass))
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học ,"
                });
            }
            var enrolled = await (from e in _context.Enrollment
                                  where e.IDClass == IDClass
                                  select e.IDStudent).ToListAsync();
            var notEnrolled = _context.Student.Where(s => !enrolled.Contains(s.IDStudent));
            var students = await (from u in _context.User
                                  join s in _context.Student on u.Id equals s.IDUser
                                  join ne in notEnrolled on u.Id equals ne.IDUser
                                  select new
                                  {
                                      IDStudent = s.IDStudent,
                                      IDUser = s.IDUser,
                                      UserName = u.UserName,
                                      Avatar = u.Avatar,
                                      FullName = u.Fullname,
                                      u.DateOfBirth,
                                      Phone = u.Phone,
                                      Address = u.Address,
                                      Email = u.Email,
                                  }).ToListAsync();
            return Ok(new
            {
                Message = "Lấy danh sách thành công",
                Data = students
            });
        }
        [HttpGet("get-students-to-add/{IDClass}/search")]
        public async Task<ActionResult> SearchStudentsToAdd(string IDClass, string userName)
        {
            if (!await _context.Class.AnyAsync(c => c.IDClass == IDClass))
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học ,"
                });
            }
            var enrolled = await (from e in _context.Enrollment
                                  where e.IDClass == IDClass
                                  select e.IDStudent).ToListAsync();
            var notEnrolled = _context.Student.Where(s => !enrolled.Contains(s.IDStudent));
            var students = await (from u in _context.User
                                  join s in _context.Student on u.Id equals s.IDUser
                                  join ne in notEnrolled on u.Id equals ne.IDUser
                                  where u.UserName.Contains(userName)
                                  select new
                                  {
                                      IDStudent = s.IDStudent,
                                      IDUser = s.IDUser,
                                      UserName = u.UserName,
                                      Avatar = u.Avatar,
                                      FullName = u.Fullname,
                                      u.DateOfBirth,
                                      Phone = u.Phone,
                                      Address = u.Address,
                                      Email = u.Email,
                                  }).ToListAsync();
            return Ok(new
            {
                Message = "Lấy danh sách thành công",
                Data = students
            });
        }
        [HttpGet("get-students/{classId}")]
        public async Task<ActionResult> GetStudents(string classId)
        {
            if(!await _context.Class.AnyAsync(c => c.IDClass == classId))
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy lớp học ,"
                });
            }
            var students = await (from u in _context.User
                       join s in _context.Student on u.Id equals s.IDUser
                       join e in _context.Enrollment on s.IDStudent equals e.IDStudent
                       where e.IDClass == classId
                       select new
                       {
                           IDStudent = s.IDStudent,
                           IDUser = s.IDUser,
                           UserName = u.UserName,
                           Avatar = u.Avatar,
                           FullName = u.Fullname,
                           u.DateOfBirth,
                           Phone = u.Phone,
                           Address = u.Address,
                           Email = u.Email,
                       }).ToListAsync();
            return Ok(new
            {
                Message = "Lấy danh sách thành công",
                Data = students
            });
        }
        [HttpGet("get-students/{classId}/{studentId}")]
        public async Task<ActionResult> GetStudentsInfo(string classId, string studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
                return NotFound(new { 
                    Message = "Không tìm thấy học sinh này"
                });
            User u = await _context.User.FindAsync(student.IDUser);
            UserDTO userDTO = new UserDTO()
            {
                UserName = u.UserName,
                Fullname = u.Fullname,
                Avatar = u.Avatar,
                Email = u.Email,
                Phone = u.Phone
            };
            return Ok(new
            {
                Message = "Lấy thông tin thành công",
                Data = userDTO
            });
        }
        [HttpPost("roadmap/{classId}")]
        public async Task<ActionResult> AddLesson(string classId, Lesson lesson, string IDUser)
        {
            if (String.IsNullOrWhiteSpace(lesson.Name) || String.IsNullOrWhiteSpace(lesson.Content))
            {
                return BadRequest(new
                {
                    Message = "Không được và tiêu đề không để trống nội dung"
                });
            }
            User u = await _context.User.FindAsync(IDUser);
            Class cls = await _context.Class.FindAsync(classId);
            var teacher = await _context.Teacher.Where(x => x.IDUser == IDUser).FirstOrDefaultAsync();
            if (teacher == null || cls == null)
            {
                return Unauthorized();
            }
            Lesson ls = new Lesson()
            {
                Name = lesson.Name,
                ClassId = classId,
                TeacherId = teacher.IDTeacher,
                Teacher = teacher,
                Class = cls
            };
            await _context.Lesson.AddAsync(ls);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Thêm thành công",
                Data = ls
            });
        }

        [HttpPut("roadmap/{classId}")]
        public async Task<ActionResult> EditLesson(string lessonId, string IDUser, Lesson lesson)
        {
            if (String.IsNullOrWhiteSpace(lesson.Name) || String.IsNullOrWhiteSpace(lesson.Content))
            {
                return BadRequest(new
                {
                    Message = "Không được và tiêu đề không để trống nội dung"
                });
            }
            var lessontemp = await _context.Lesson.FindAsync(lessonId);
            var teacher = await _context.Teacher.Where(x => x.IDUser == IDUser).FirstOrDefaultAsync();
            if (teacher == null || teacher.IDTeacher != lessontemp.TeacherId)
            {
                return Unauthorized(new
                {
                    message = "Bạn không có quyền chỉnh sửa"
                });
            }
            if (lesson == null)
            {
                return NotFound();
            }
            lessontemp.Name = lesson.Name;
            lessontemp.Content = lesson.Content;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Chỉnh sửa thành công",
                Data = lesson
            });
        }
        [HttpGet("roadmap/{IDClass}")]
        public async Task<ActionResult> GetLesson(string IDClass)
        {            
            var cls = await _context.Class.FindAsync(IDClass);
            if(cls == null)
            {
                return NotFound(new
                {
                    message = "Không có lớp học này"
                });
            }
            var lessons = await _context.Lesson.Include("courseDetails").Where(x => x.ClassId == cls.IDClass).ToListAsync();
            return Ok(new
            {
                Message = "Lấy bài học thành công",
                Data = lessons
            });
        }

        [HttpGet("roadmap/get-course-details/{IDClass}")]
        public async Task<ActionResult> GetCourseDetails(string IDClass)
        {
            var cls = await _context.Class.FindAsync(IDClass);
            if (cls == null)
            {
                return NotFound();
            }
            var coursedetails = _context.CourseDetails.Where(c => c.IDCourse == cls.IDCourse); 
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Lấy chi tiết khóa học thành công",
                Data = coursedetails
            });
        }

        [HttpPost("roadmap/create-course-details/{lessonId}")]
        public async Task<ActionResult> CreateCourseDetails(CourseDetailDTO courseDetail, string lessonId)
        {
            CourseDetail cd = new CourseDetail()
            {
                LessonDuration = courseDetail.LessonDuration,
                LessonName = courseDetail.LessonName,
                LessonLink = courseDetail.LessonLink,
                Material = courseDetail.Material
            };
            var lesson = await _context.Lesson.FindAsync(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }
            try
            {
                await _context.CourseDetails.AddAsync(cd);
                lesson.courseDetails.Add(cd);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Tạo thành công",
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi tạo nội dung",
                    Data = e.Message
                });
            }
        }

        [HttpPut("roadmap/edit-course-details")]
        public async Task<ActionResult> EditCourseDetails(CourseDetailDTO courseDetail)
        {
            CourseDetail cd = new CourseDetail()
            {
                LessonDuration = courseDetail.LessonDuration,
                LessonName = courseDetail.LessonName,
                LessonLink = courseDetail.LessonLink,
                Material = courseDetail.Material
            };
            try
            {
                await _context.CourseDetails.AddAsync(cd);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Chỉnh sửa thành công",
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi chỉnh sửa",
                    Data = e.Message
                });
            }
        }

        [HttpDelete("roadmap/remove-course-details")]
        public async Task<ActionResult> RemoveCourseDetails(string courseDetailId)
        {
            try
            {
                var courseDetail = await _context.CourseDetails.FindAsync(courseDetailId);
                _context.CourseDetails.Remove(courseDetail);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi xóa",
                    Data = e.Message
                });
            }
            return Ok(new
            {
                Message = "Xóa thành công",
            });
        }

        [HttpDelete("roadmap/{lessonId}")]
        public async Task<ActionResult> DeleteLesson(string lessonId, string uid)
        {
            var lesson = await _context.Lesson.FindAsync(lessonId);
            var teacher = await _context.Teacher.Where(x => x.IDUser == uid).FirstOrDefaultAsync();
            if (teacher == null || teacher.IDTeacher != lesson.TeacherId)
            {
                return Unauthorized(new
                {
                    message = "Bạn không có quyền chỉnh sửa"
                });
            }
            if (lesson == null)
            {
                return NotFound(new { 
                    Message = "Không tìm thấy bài học này"
                });
            }
            try
            {
                _context.Remove(lesson);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Xóa thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi xóa",
                    Data = e.Message
                });
            }
        }

        [HttpGet("mentor/{classId}")]
        public async Task<ActionResult<IEnumerable<Mentor>>> GetMentors(string classId)
        {
            var classes = await _context.Class.Where(x => x.IDClass == classId).Include(x => x.Mentors).FirstOrDefaultAsync();
            if (classes == null)
            {
                return NotFound();
            }
            if (classes.Mentors.Count == 0)
                return Ok(new
                {
                    Message = "Lớp học chưa có mentor"
                });
            return Ok(new
            {
                Message = "Lấy mentor thành công",
                Data = classes.Mentors.ToList()
            });
        }

        [HttpDelete("mentor/{classId}/{mentorId}")]
        public async Task<IActionResult> RemoveMentor(string classId, string mentorId)
        {
            var classes = await _context.Class.Where(x => x.IDClass == classId).FirstOrDefaultAsync();
            var mentor = await _context.Mentor.Where(x => x.IDMentor == mentorId).FirstOrDefaultAsync();
            if (classes == null || mentor == null)
            {
                return NotFound(new
                {
                    message = "Không có lớp học này"
                });
            }
            classes.Mentors.Remove(mentor);
            _context.SaveChanges();
            return Ok(new
            {
                message = "Xóa thành công"
            });
        }

        [HttpPost("mentor/{classId}")]
        public async Task<IActionResult> AddMentor(string classId, string mentorId)
        {
            var classes = await _context.Class.Where(x => x.IDClass == classId).Include(x => x.Mentors).FirstOrDefaultAsync();
            var mentor = await _context.Mentor.Where(x => x.IDMentor == mentorId).FirstOrDefaultAsync();
            if (classes == null || mentor == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy"
                });
            }
            try
            {
                if (classes.Mentors.Contains(mentor))
                    return Conflict(new
                    {
                        message = "Đã có mentor này trong lớp học"
                    });
                else
                    classes.Mentors.Add(mentor);
                _context.SaveChanges();
            }
            catch
            {
            }
            return Ok(new
            {
                message = "Thêm thành công"
            });
        }

        [HttpGet("feedback-student")]
        public async Task<ActionResult> GetFeedback(string classId, string IdUser)
        {
            var teacher = await _context.Teacher.FindAsync(IdUser);
            var feedbacks = await _context.StudentFeedbacks.Where(x => x.ClassId == classId && x.TeacherId == teacher.IDTeacher).ToListAsync();
            try
            {
                return Ok(new
                {
                    Message = "Lấy feedback  thành công",
                    Data = feedbacks
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpPost("feedback-student")]
        public async Task<ActionResult> Feedback(string classId, string studentId, string teacherId, [FromBody]string content)
        {
            var student = await _context.Student.FindAsync(studentId);
            var teacher = await _context.Teacher.FindAsync(teacherId);
            var cls = await _context.Class.FindAsync(classId);
            if (student == null || teacher == null)
            {
                return NotFound(new
                {
                    Message = "Không tìm thấy học viên hoặc giáo viên"
                });
            }
            if (cls == null)
            {
                return NotFound(new
                {
                    Message = "Không tồn tại lớp học này"
                });
            }
            StudentFeedback feedback = new StudentFeedback()
            {
                ClassId = classId,
                TeacherId = teacherId,
                StudentId = studentId,
                Class = cls,
                Student = student,
                Teacher = teacher,
                Content = content
            };
            try
            {
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Thêm feedback  thành công",
                    Data = feedback
                });
            } catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpPost("feedback-student/edit")]
        public async Task<ActionResult> EditFeedback(string feedbackId, [FromBody]string content)
        {
            var feedback = await _context.StudentFeedbacks.FindAsync(feedbackId);
            feedback.Content = content;
            try
            {
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Chỉnh sửa feedback  thành công",
                    Data = feedback
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                }) ;
            }           
        }
        [HttpDelete("feedback-student/{feedbackId}")]
        public async Task<ActionResult> DeleteFeedback(string feedbackId)
        {
            try
            {
                var feedback = await _context.StudentFeedbacks.FindAsync(feedbackId);
                _context.StudentFeedbacks.Remove(feedback);
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Xóa feedback thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = e.Message
                });
            }
        }
        [HttpGet("get-classes")]
        public async Task<ActionResult> GetClasses(string IdUser)
        {
            var teacher = await _context.Teacher.Where(x=>x.IDUser == IdUser).FirstOrDefaultAsync();
            if (teacher == null)
                return NotFound(new
                {
                    Message = "Tài khoản không tồn tại"
                });
            var cls = await _context.Class.Where(x => x.IDTeacher == teacher.IDTeacher).ToListAsync();
            try
            {
                return Ok(new
                {
                    Message = "Lấy lớp học thành công",
                    Data = cls
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi khi lấy lớp học",
                    Data = e.Message
                });
            }
        }
        [HttpGet("assignment")]
        public async Task<ActionResult> GetAssignment(string IdClass)
        {
            try
            {
                var assignments = await _context.Assignment.Include("AssignmentSubmission")
                    .Where(x => x.IDClass == IdClass).ToListAsync();
                if (assignments == null)
                    return NotFound(new
                    {
                        Message = "Chưa có assignment nào"
                    });
                return Ok(new
                {
                    Message = "Lấy assignment thành công",
                    Data = assignments
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi khi lấy assignment",
                    Data = e.Message
                });
            }
        }
        [HttpPost("assignment/create")]
        public async Task<ActionResult> CreateAssignment(Assignment assignment)
        {
            try
            {
                var cls = await _context.Class.FindAsync(assignment);
                assignment.Class = cls;
                await _context.Assignment.AddAsync(assignment);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Tạo assignment thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Thất bại",
                    Data = e.Message
                });
            }
        }
        [HttpPut("assignment/edit")]
        public async Task<ActionResult> EditAssignment(Assignment assignment)
        {
            try
            {
                _context.Assignment.Update(assignment);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Sửa assignment thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Thất bại",
                    Data = e.Message
                });
            }
        }
        [HttpPut("assignment/delete")]
        public async Task<ActionResult> DeleteAssignment(string assignmentId)
        {
            try
            {
                var assignment = await _context.Assignment.Include("AssignmentSubmission")
                    .Where(x=> x.IDAssignemnt == assignmentId).FirstOrDefaultAsync();
                if(assignment == null)
                {
                    return NotFound(new {
                        Message = "Không tìm thấy assignment này"
                    });
                }
                _context.Assignment.Remove(assignment);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Xóa assignment thành công"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Thất bại",
                    Data = e.Message
                });
            }
        }
        //create assignmentt
        [HttpPost]
        [Route("create-assignment")]
        public async Task<ActionResult<Assignment>> createAssignment([FromBody] Assignment assignment, [FromQuery] string IDClass)
        {
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
            var assignmentResult = new Assignment()
            {
                AssignmentName = assignment.AssignmentName,
                Description = assignment.Description,
                IDClass = IDClass
            };
            _context.Assignment.Add(assignmentResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssignmentExists(assignmentResult.IDAssignemnt))
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
            return CreatedAtAction("Get Assignment", new { IDAssignment = assignmentResult.IDAssignemnt }, new
            {
                StatusCode = 201,
                Message = "Tạo bài tập thành công",
                Data = assignmentResult
            });
        }
        //Get assignment list
        [HttpGet]
        [Route("get-assignments-list-according-to-class")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentListAccordingtoClass([FromQuery] string IDClass)
        {
            List<Assignment> assignmentList = await _context.Assignment.Where(a => a.IDClass == IDClass).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách bài tập theo lớp học",
                Data = assignmentList
            });
        }
        //Get assignment detail 
        [HttpGet]
        [Route("get-assignment/{IDAssignment}")]
        public async Task<ActionResult<Assignment>> GetAssignmentDetail(string IDAssignment)
        {
            Assignment assignment = await _context.Assignment.FindAsync(IDAssignment);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy chi tiết bài tập thành công",
                Data = assignment
            });
        }
        private async Task<ActionResult> GetQuizz(string quizzId)
        {
            var quizz = await _context.Quizz.FindAsync(quizzId);
            return Ok();
        }
        private bool AssignmentExists(string IDAssignemnt)
        {
            return _context.Assignment.Any(e => e.IDAssignemnt == IDAssignemnt);
        }
    }
}

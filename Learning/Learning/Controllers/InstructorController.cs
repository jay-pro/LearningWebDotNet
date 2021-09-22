using lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = ("Instructor"))]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly LMSContext _context;
        public InstructorController(LMSContext context)
        {
            _context = context;

        }
        // Create a course
        [HttpPost]
        [Route("create-course")]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course, [FromQuery] string IDUser)
        {
            var instructor = (from u in _context.User
                             join i in _context.Instructor on u.Id equals i.IDUser
                             where u.Id == IDUser
                             select i).FirstOrDefault();
            if (instructor == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Data = ""
                });
            }
            DateTime today = DateTime.Now;
            var courseTempt = new Course {
                CourseName = course.CourseName,
                Description = course.Description,
                Duration = course.Duration,
                RegistedNumber = 0,
                Rating = 0,
                Status = "waitingforapproval",
                Image = course.Image,
                Field = course.Field,
                CreatedAt = today,
                IsDeleted = false,
                IDCreator = instructor.IDInstuctor
            };
            _context.Course.Add(courseTempt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(courseTempt.IDCourse))
                {
                    return Conflict(new { StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = "" });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Course", new { IDCourse = courseTempt.IDCourse }, new {
                StatusCode = 201,
                Message = "Tạo khóa học thành công",
                Data = courseTempt });
        }
        // Get course list
        [HttpGet]
        [Route("course")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesList([FromQuery] string IDUser){
            var lstCourse = await (from c in _context.Course
                                   join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                   join u in _context.User on i.IDUser equals u.Id
                                   where c.Status == "approval" && c.IsDeleted == false && u.Id == IDUser
                                   select new CourseDTO(){
                                       IDCourse = c.IDCourse,
                                       CourseName = c.CourseName,
                                       Description = c.Description,
                                       Image = c.Image,
                                       Duration = c.Duration,
                                       RegistedNumber = c.RegistedNumber,
                                       Rating = c.Rating,
                                       Field = c.Field,
                                       CreatedAt = c.CreatedAt,
                                       CreatorName = u.Fullname
                                   }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách khóa học thành công",
                Data = lstCourse
            });
        }
        // Get course
        [HttpGet]
        [Route("course/{IDCourse}")]
        public async Task<ActionResult<Course>> GetCourses(String IDCourse)
        {
            var Course = await (from c in _context.Course
                                   join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                   join u in _context.User on i.IDUser equals u.Id
                                   where c.IDCourse == IDCourse
                                select new CourseDTO()
                                   {
                                       IDCourse = c.IDCourse,
                                       CourseName = c.CourseName,
                                       Description = c.Description,
                                       Image = c.Image,
                                       Duration = c.Duration,
                                       RegistedNumber = c.RegistedNumber,
                                       Rating = c.Rating,
                                       Field = c.Field,
                                       CreatedAt = c.CreatedAt,
                                       CreatorName = u.Fullname
                                   }).FirstOrDefaultAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy khóa học thành công",
                Data = Course
            });
        }
        //Update a course
        [HttpPut]
        [Route("course/{IDCourse}")]
        public async Task<ActionResult<Course>> PutCourse(String IDCourse, [FromBody] Course course)
        {
            var CourseTemp = await _context.Course.FindAsync(IDCourse);
            if (CourseTemp == null)
            {
                return NotFound(new {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học trong hệ thống",
                    Data = ""
                    });
            }
            CourseTemp.CourseName = course.CourseName;
            CourseTemp.Description = course.Description;
            CourseTemp.Duration = course.Duration;
            CourseTemp.Field = course.Field;
            CourseTemp.Status = "waitingforapproval";
            _context.Entry(CourseTemp).State = EntityState.Modified;
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
                        Message = "Không tìm thấy khóa học trong hệ thống",
                        Data = course
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new {
                StatusCode = 204,
                Message = "Chỉnh sửa khóa học thành công",
                Data = course
            });
        }
        //Create a course detail list
        [HttpPost]
        [Route("create-course-detail-list/{IDCourse}")]
        public async Task<ActionResult<List<CourseDetail>>> CreateCourseDetailList(string IDCourse, [FromBody] List<CourseDetail> lstCourseDetail){
            
            if (IDCourse == null)
            {
                return BadRequest(new { StatusCode = 400,
                            Message = "Bài học phải gắn với một khóa học nhất định",
                            Data = ""});
            }
            if (!CourseExists(IDCourse))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            List<CourseDetail> lstCourseDetailResult = new List<CourseDetail>();
            foreach (CourseDetail courseDetail in lstCourseDetail)
            {
                var courseDetailtemp = new CourseDetail
                {
                    LessonName = courseDetail.LessonName,
                    LessonLink = courseDetail.LessonLink,
                    Material = courseDetail.Material,
                    LessonDuration = courseDetail.LessonDuration,
                    IDCourse = IDCourse
                };
                lstCourseDetailResult.Add(courseDetailtemp);
                _context.CourseDetails.Add(courseDetailtemp);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (CourseExists(courseDetailtemp.IDCourseDetail))
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
            }

            return CreatedAtAction("List Course", new
            {
                StatusCode = 201,
                Message = "Tạo bài học thành công",
                Data = lstCourseDetailResult
            });
        }
        //Create a course detail
        [HttpPost]
        [Route("course/{IDCourse}/create-course-detail")]
        public async Task<ActionResult<CourseDetail>> CreateCourseDetail(string IDCourse, [FromBody] CourseDetail CourseDetail)
        {
            if (!CourseExists(IDCourse))
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            var courseDetailtemp = new CourseDetail
            {
                LessonName = CourseDetail.LessonName,
                LessonLink = CourseDetail.LessonLink,
                Material = CourseDetail.Material,
                LessonDuration = CourseDetail.LessonDuration,
                IDCourse = IDCourse
            };
            _context.CourseDetails.Add(courseDetailtemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(courseDetailtemp.IDCourseDetail))
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
            return CreatedAtAction("Course detail", new { IDCourseDetail = courseDetailtemp.IDCourseDetail},new
            {
                StatusCode = 201,
                Message = "Tạo bài học thành công",
                Data = courseDetailtemp
            });
        }
        //Update a course detail 
        [HttpPut]
        [Route("course/{IDCourseDetail}/update-course-detail")]
        public async Task<ActionResult<CourseDetail>> PutCourseDetail (string IDCourseDetail, [FromBody] CourseDetail courseDetail)
        {
            if (IDCourseDetail != courseDetail.IDCourseDetail)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "ID khóa học không phù hợp",
                    Data = ""
                });
            }
            _context.Entry(courseDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseDetailExists(courseDetail.IDCourseDetail))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy khóa học trong hệ thống",
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
                StatusCode = 204,
                Message = "Chỉnh sửa bài học thành công",
                Data = courseDetail
            });
        }
        //Update a course detail list
        [HttpPut]
        [Route("course/update-course-detail-list")]
        public async Task<ActionResult<IEnumerable<CourseDetail>>> PutCourseDetailList ([FromBody] List<CourseDetail> lstCourseDetail)
        {
            foreach(CourseDetail courseDetail in lstCourseDetail)
            {
                _context.Entry(courseDetail).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseDetailExists(courseDetail.IDCourseDetail))
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = "Không tìm thấy khóa học trong hệ thống",
                            Data = ""
                        });
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa danh sách bài học thành công",
                Data = lstCourseDetail
            });
        }
        //Get a course detail list with IDCourse
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
                                         select new CourseDetailDTO() {
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
        //Get a course detail with IDCourse
        [HttpGet]
        [Route("course/{IDCourse}/course-detail/{IDCourseDetail}")]
        public async Task<ActionResult<CourseDetail>> GetCourseDetail (string IDCourse, string IDCourseDetail)
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
            var CourseDetail =  await (from cd in _context.CourseDetails
                                       where cd.IDCourseDetail == IDCourseDetail
                                       select new CourseDetailDTO()
                                       {
                                           IDCourseDetail = cd.IDCourseDetail,
                                           LessonName = cd.LessonName,
                                           LessonLink = cd.LessonLink,
                                           Material = cd.Material,
                                           LessonDuration = cd.LessonDuration
                                       }).SingleOrDefaultAsync();
            if (CourseDetail == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy bài học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách bài học thành công",
                Data = new
                {
                    Course = course,
                    CourseDetailList = CourseDetail,
                }
            });
        }
        //Create a class
        [HttpPost]
        [Route("create-class")]
        public async Task<ActionResult<Class>> CreateClass([FromBody] Class _class, [FromQuery] string IDCourse,
                                                [FromQuery] string IDTeacher, [FromQuery] string IDUser){
            var instructor = (from u in _context.User
                              join i in _context.Instructor on u.Id equals i.IDUser
                              where u.Id == IDUser
                              select i).FirstOrDefault();
            if (instructor == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Data = ""
                });
            }
            if (IDCourse == null)
            { 
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Lớp học phải gắn với một khóa học nhất định",
                    Data = ""
                });
            }
            if (!CourseExists(IDCourse))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            if (!TeacherExists(IDTeacher) && IDTeacher != null) {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy giáo viên trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            var classResult = new Class
            {
                ClassName = _class.ClassName,
                StartTime = _class.StartTime,
                FinishTime = _class.FinishTime,
                IDTeacher = IDTeacher,
                IDCourse = IDCourse,
                IDCreator = instructor.IDInstuctor
            };
            _context.Class.Add(classResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(classResult.IDClass))
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
            return CreatedAtAction("Get Class", new { IDClass = classResult.IDClass }, new
            {
                StatusCode = 201,
                Message = "Tạo lớp học thành công",
                Data = classResult
            });
        }
        //Get class list
        [HttpGet]
        [Route("get-class-list")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassList([FromQuery] string IDUser, [FromQuery] string IDCourse)
        {
            var instructor = (from u in _context.User
                              join i in _context.Instructor on u.Id equals i.IDUser
                              where u.Id == IDUser
                              select new { 
                                  IDInstructor = i.IDInstuctor,
                                  IDUser = i.User,
                                  FullName = u.Fullname
                              }).FirstOrDefault();
            if (instructor == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Data = ""
                });
            }
            List<ClassDTO> classList = await (from c in _context.Class
                                              join t in _context.Teacher on c.IDTeacher equals t.IDTeacher into Teacher_Class
                                              from teacherclass in Teacher_Class.DefaultIfEmpty()
                                              join u in _context.User on teacherclass.IDUser equals u.Id into Teacher_User
                                              from teacheruser in Teacher_User.DefaultIfEmpty()
                                              where c.IDCreator == instructor.IDInstructor && c.IDCourse == IDCourse
                                              select new ClassDTO() {
                                                  IDClass = c.IDClass,
                                                  ClassName = c.ClassName,
                                                  StartTime = c.StartTime,
                                                  FinishTime = c.FinishTime,
                                                  TeacherName = teacheruser == null ? null : teacheruser.Fullname,
                                                  CreatorName = instructor.FullName
                                              }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách lớp học thành công",
                Data = classList
            });
        }
        //Get class detail
        [HttpGet]
        [Route("get-class-detail/{IDClass}")]
        public async Task<ActionResult<Class>> GetClassDetail(string IDClass)
        {
            //from c in _context.Comment
            //join r in _context.Reply on c.IDComment equals r.IDComment into CommentReply
            //from comrep in CommentReply.DefaultIfEmpty()
            ClassDTO classResult = await (from c in _context.Class
                                          join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                          join ui in _context.User on i.IDUser equals ui.Id
                                          join t in _context.Teacher on c.IDTeacher equals t.IDTeacher into Teacher_Class
                                          from teacherclass in Teacher_Class.DefaultIfEmpty()
                                          join ut in _context.User on teacherclass.IDUser equals ut.Id into User_Teacher
                                          from userTeacher in User_Teacher.DefaultIfEmpty()
                                          where c.IDClass == IDClass
                                          select new ClassDTO()
                                          {
                                              IDClass = c.IDClass,
                                              ClassName = c.ClassName,
                                              StartTime = c.StartTime,
                                              FinishTime = c.FinishTime,
                                              TeacherName = userTeacher == null ? null : userTeacher.Fullname,
                                              CreatorName = ui.Fullname
                                          }).SingleOrDefaultAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy Lớp học thành công",
                Data = classResult
            });
        }
        //create assignmentt
        [HttpPost]
        [Route("create-assignment")]
        public async Task<ActionResult<Assignment>> createAssignment ([FromBody] Assignment assignment, [FromQuery] string IDClass)
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
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentListAccordingtoClass ([FromQuery] string IDClass)
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
        public async Task<ActionResult<Assignment>> GetAssignment(string IDAssignment)
        {
            Assignment assignment = await _context.Assignment.FindAsync(IDAssignment);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy chi tiết bài tập thành công",
                Data = assignment
            });
        }
        //Get teacher list
        [HttpGet]
        [Route("get-teacher-list")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacherList()
        {
            var lstTeacher = await (from teacher in _context.Teacher
                                              join user in _context.User on teacher.IDUser equals user.Id
                                              select new { teacher.IDTeacher, user.Fullname, user.UserName,
                                              user.Avatar, user.DateOfBirth, user.Major})
                                              .AsNoTracking()
                                              .ToListAsync();
            if (lstTeacher == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy giáo viên",
                    Data = ""
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách giáo viên thành công",
                Data = lstTeacher
            });
        }
        //get teacher detail
        [HttpGet]
        [Route("get-teacher/{IDTeacher}")]
        public async Task<ActionResult<Teacher>> GetTeacher(string IDTeacher)
        {
            var teacher = await (from t in _context.Teacher
                                 join u in _context.User on t.IDUser equals u.Id
                                 where t.IDTeacher == IDTeacher && u.Status == "approval"
                                 select new
                                 {
                                     t.IDTeacher,
                                     u.Fullname,
                                     u.UserName,
                                     u.Avatar,
                                     u.DateOfBirth,
                                     u.Major,
                                     u.Address,
                                     u.Phone,
                                     u.Email,
                                 })
                                 .AsNoTracking()
                                 .ToListAsync();
            var lstclass = await _context.Class.Where(c => c.IDTeacher == IDTeacher).ToListAsync();
            if (teacher == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy giáo viên",
                    Data = ""
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy giáo viên thành công",
                Data = new
                {
                    TeacherDetail = teacher,
                    ClassList = lstclass
                }
            });
        }
        //Assign teacher to class
        [HttpPut]
        [Route("assign-teacher-to-class")]
        public async Task<ActionResult<Class>> AssignTeacherToClass ([FromQuery] string IDClass, [FromQuery] string IDTeacher)
        {
            if (!TeacherExists(IDTeacher))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy giáo viên trong cơ sở dữ liệu",
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
            classResult.IDTeacher = IDTeacher;
            _context.Class.Update(classResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            ClassDTO classResultDTO = await (from c in _context.Class
                                             join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                             join ui in _context.User on i.IDUser equals ui.Id
                                             join t in _context.Teacher on c.IDTeacher equals t.IDTeacher into Teacher_Class
                                             from teacherclass in Teacher_Class.DefaultIfEmpty()
                                             join ut in _context.User on teacherclass.IDUser equals ut.Id into User_Teacher
                                             from userTeacher in User_Teacher.DefaultIfEmpty()
                                             where c.IDClass == IDClass
                                             select new ClassDTO()
                                             {
                                                 IDClass = c.IDClass,
                                                 ClassName = c.ClassName,
                                                 StartTime = c.StartTime,
                                                 FinishTime = c.FinishTime,
                                                 TeacherName = userTeacher == null ? null : userTeacher.Fullname,
                                                 CreatorName = ui.Fullname
                                             }).SingleOrDefaultAsync();
            return Ok(new {
                StatusCode = 204,
                Message = "Gán giáo viên thành công",
                Data = classResultDTO
            });
        }
        //Remove teacher to class
        [HttpPut]
        [Route("remove-teacher-to-class")]
        public async Task<ActionResult<Class>> RemoveTeacherToClass([FromQuery] string IDClass)
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
            classResult.IDTeacher = null;
            _context.Class.Update(classResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            ClassDTO classResultDTO = await (from c in _context.Class
                                             join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                             join ui in _context.User on i.IDUser equals ui.Id
                                             join t in _context.Teacher on c.IDTeacher equals t.IDTeacher into Teacher_Class
                                             from teacherclass in Teacher_Class.DefaultIfEmpty()
                                             join ut in _context.User on teacherclass.IDUser equals ut.Id into User_Teacher
                                             from userTeacher in User_Teacher.DefaultIfEmpty()
                                             where c.IDClass == IDClass
                                             select new ClassDTO()
                                             {
                                                 IDClass = c.IDClass,
                                                 ClassName = c.ClassName,
                                                 StartTime = c.StartTime,
                                                 FinishTime = c.FinishTime,
                                                 TeacherName = userTeacher == null ? null : userTeacher.Fullname,
                                                 CreatorName = ui.Fullname
                                             }).SingleOrDefaultAsync();
            return Ok(new
            {
                StatusCode = 204,
                Message = "Xóa giáo viên khỏi lớp học thành công",
                Data = classResultDTO
            });
        }
        //Assign a student to class
        [HttpPost]
        [Route("assign-student-to-class")]
        public async Task<ActionResult<Enrollment>> AssignStudent ([FromQuery] string IDClass, [FromQuery] string IDStudent)
        {
            Student student = await _context.Student.FindAsync(IDStudent);
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
            var checkEnrolllment = _context.Enrollment.Where(e => e.IDClass == IDClass && e.IDStudent == IDStudent).FirstOrDefault();
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
                IDStudent = IDStudent,
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
            return Ok(new
            {
                StatusCode = 204,
                Message = "Gán học viên thành công",
                Data = enrollment
            });
        }
        //Create a quizz 
        [HttpPost]
        [Route("create-quizz")]
        public async Task<ActionResult<Quizz>> CreateQuizz ([FromBody] Quizz Quizz,[FromQuery] string IDCourseDetail)
        {
            if (IDCourseDetail == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Bài quizz phải tương ứng với một bài học",
                    Data = ""
                });
            }
            if (!CourseDetailExists(IDCourseDetail))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy bài học trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            var QuizzResult = new Quizz
            {
                QuizzName = Quizz.QuizzName,
                Duration = Quizz.Duration,
                TotalScore = Quizz.TotalScore,
                IDCourseDetail = IDCourseDetail
            };
            _context.Quizz.Add(QuizzResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuizzExists(QuizzResult.IDQuizz))
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
            return CreatedAtAction("Get Quizz", new { IDQuizz = QuizzResult.IDQuizz }, new
            {
                StatusCode = 201,
                Message = "Tạo quizz thành công",
                Data = QuizzResult
            });
        }
        //Create a quizz list
        [HttpPost]
        [Route("{IDQuizz}/create-quizz-detail")]
        public async Task<ActionResult<List<QuizzDetail>>> CreateQuizzDetail ([FromBody] List<QuizzDetail> lstQuizzDetail, string IDQuizz)
        {
            if (IDQuizz == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Câu hỏi phải tương ứng với bài quizz",
                    Data = ""
                });
            }
            Quizz Quizz = _context.Quizz.Find(IDQuizz);
            if (Quizz == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không tìm thấy bài quizz trong cơ sở dữ liệu",
                    Data = ""
                });
            }
            List<QuizzDetail> lstQuizzDetailResult = new List<QuizzDetail>();
            foreach(QuizzDetail quizzDetail in lstQuizzDetail)
            {
                var QuizzDetail = new QuizzDetail
                {
                    Question = quizzDetail.Question,
                    AChoice = quizzDetail.AChoice,
                    BChoice = quizzDetail.BChoice,
                    CChoice = quizzDetail.CChoice,
                    DChoice = quizzDetail.DChoice,
                    Answer = quizzDetail.Answer,
                    IDQuizz = IDQuizz
                };
                lstQuizzDetailResult.Add(QuizzDetail);
                _context.QuizzDetail.Add(QuizzDetail);              
            }
            Quizz.TotalQuestion = Quizz.TotalQuestion + lstQuizzDetailResult.Count();
            _context.Quizz.Update(Quizz);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("List Quizz Detail", new
            {
                StatusCode = 201,
                Message = "Tạo danh sách câu hỏi thành công",
                Data = lstQuizzDetailResult
            });
        }
        //Get a list comment and reply
        [HttpGet]
        [Route("get-comment-reply")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentList()
        {
            var commentlist = await (from c in _context.Comment
                              join r in _context.Reply on c.IDComment equals r.IDComment into CommentReply
                              from comrep in CommentReply.DefaultIfEmpty()
                              join uc in _context.User on c.IDUser equals uc.Id 
                              join ur in _context.User on comrep.IDUser equals ur.Id into UserReply
                              from repuser in UserReply.DefaultIfEmpty()
                              select new
                              {
                                  IDComment = c.IDComment,
                                  CommentContent = c.Content,
                                  CommentCreatedAt = c.CreatedAt,
                                  CommentIDUser = c.IDUser,
                                  CommentUsername = uc.UserName,
                                  CommentAvatar = uc.Avatar,
                                  IDReply = comrep == null ? "<null>" : comrep.IDReply,
                                  ReplyContent = comrep == null ? "<null>" : comrep.Content,
                                  ReplyIDUser = comrep == null ? "<null>" : comrep.IDUser,
                                  ReplyUsername = repuser == null ? "<null>" : repuser.UserName,
                                  ReplyAvatar = repuser == null ? "<null>" : repuser.Avatar
                              })
                              .ToListAsync();
            commentlist = commentlist.OrderByDescending( c => c.CommentCreatedAt).ToList();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách thắc mắc thành công",
                Data = commentlist
            });
        }
        //Post a reply
        [HttpPost]
        [Route("post-a-reply")]
        public async Task<ActionResult<Reply>> PostReply([FromQuery] string IDUser, [FromQuery] string IDComment, [FromBody] Reply reply)
        {
            DateTime today = DateTime.Now;
            var ReplyResut = new Reply
            {
                Content = reply.Content,
                IDUser = IDUser,
                CretatedAt = today,
                IDComment = IDComment
            };
            _context.Reply.Add(ReplyResut);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReplyExists(ReplyResut.IDReply))
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
            return CreatedAtAction("Get Reply", new
            {
                StatusCode = 201,
                Message = "Đăng giải đáp thành công",
                Data = ReplyResut
            });
        }
        //Put a reply
        [HttpPut]
        [Route("edit-a-reply/{IDReply}")]
        public async Task<ActionResult<Reply>> PutReply (String IDReply, [FromBody] Reply reply)
        {
            Reply ReplyResult = await _context.Reply.FindAsync(IDReply);
            if (ReplyResult == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy giải đáp trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            ReplyResult.Content = reply.Content;
            _context.Entry(ReplyResult).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!ReplyExists(ReplyResult.IDReply))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy giải đáp trong cơ sở dữ liệu",
                        Data = "",
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Reply", new
            {
                StatusCode = 201,
                Message = "Chỉnh sửa phản hồi thành công",
                Data = ReplyResult
            });
        }
        //statistic/view-created-course-month
        [HttpGet]
        [Route("statistic/view-course-month")]
        public async Task<ActionResult<IEnumerable<Course>>> CreatedCourseStatisticMonth ([FromQuery] string IDUser)
        {
            var instructor = (from u in _context.User
                              join i in _context.Instructor on u.Id equals i.IDUser
                              where u.Id == IDUser
                              select i).FirstOrDefault();
            var lstCourse = await (from c in _context.Course
                                   where c.Status == "approval" && c.IDCreator == instructor.IDInstuctor && c.IsDeleted != true
                                   select new { 
                                       month = c.CreatedAt.Month,
                                       IDCourse = c.IDCourse}).ToListAsync();
            List<MonthQuantity> Statistic = new List<MonthQuantity>();
            for (int i = 0; i < 12; i++)
            {
                Statistic.Add(new MonthQuantity ( i + 1, 0 ));
            }
            for (int i = 0; i < lstCourse.Count(); i++)
            {
                Statistic[lstCourse[i].month - 1].Quantity = Statistic[lstCourse[i].month - 1].Quantity + 1;
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách thống kê khóa học theo tháng thành công",
                Data = Statistic
            });
        }
        //statistic/class-student-quantity
        [HttpGet]
        [Route("statistic/class-student-quantity")]
        public async Task<ActionResult> StudentQuantityofClass([FromQuery] string IDUser)
        {
            var instructor = (from u in _context.User
                              join i in _context.Instructor on u.Id equals i.IDUser
                              where u.Id == IDUser
                              select i).FirstOrDefault();
            var lstStudentQuantity = await (from e in _context.Enrollment
                                            join c in _context.Class on e.IDClass equals c.IDClass
                                            where c.IDCreator == c.IDCreator
                                            group c by c.ClassName into g
                                            select new
                                            {
                                                ClassName = g.Key,
                                                Quantity =  g.Count()
                                            }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách thống kê số lượng học viên theo lớp học",
                Data = lstStudentQuantity
            });
        }
        //statistic/course-completion-ratio
        [HttpGet]
        [Route("statistic/course-completion-ratio")]
        public async Task<ActionResult> CourseCompletionRatio([FromQuery] string IDUser)
        {
            var instructor = (from u in _context.User
                              join i in _context.Instructor on u.Id equals i.IDUser
                              where u.Id == IDUser
                              select i).FirstOrDefault();
            var lstCompletionQuantity = await (from c in _context.Class
                                            join e in _context.Enrollment on c.IDClass equals e.IDClass
                                            where c.IDCreator == instructor.IDInstuctor
                                            group e by c.ClassName  into g
                                            select new
                                            {
                                                ClassName = g.Key,
                                                CompletionRatio = g.Sum (e => e.Progress >= 100 ? 1 : 0),
                                                Count = g.Count()
                                            }).ToListAsync();
            List<NameRatio> lstResult = new List<NameRatio>();
            for (int i = 0; i < lstCompletionQuantity.Count; i++)
            {
                float studentQuantity = lstCompletionQuantity[i].Count;
                var temp = (lstCompletionQuantity[i].CompletionRatio / studentQuantity) * 100;
                lstResult.Add(new(lstCompletionQuantity[i].ClassName, temp));

            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách tỷ lệ học viên hoàn thành lớp học thành công",
                Data = lstResult
            });
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
        private bool CourseExists(string IDCourse)
        {
            return _context.Course.Any(e => e.IDCourse == IDCourse);
        }
        private bool CourseDetailExists(string IDCourseDetail)
        {
            return _context.CourseDetails.Any(e => e.IDCourseDetail == IDCourseDetail);
        }
        private bool ClassExists(string IDClass)
        {
            return _context.Class.Any(e => e.IDClass == IDClass);
        }
        private bool TeacherExists (string IDTeacher)
        {
            return _context.Teacher.Any(e => e.IDTeacher == IDTeacher);
        }
        private bool StudentExists (String IDStudent)
        {
            return _context.Student.Any(e => e.IDStudent == IDStudent);
        }
        private bool QuizzExists (string IDQuizz)
        {
            return _context.Quizz.Any(e => e.IDQuizz == IDQuizz);
        }
        private bool QuizzDetail (string IDQuizzDetail)
        {
            return _context.QuizzDetail.Any(e => e.IDQuizzDetail == IDQuizzDetail);
        }
        private bool ReplyExists(string IDReply)
        {
            return _context.Reply.Any( e => e.IDReply == IDReply);
        }
        private bool EnrollmentExists(string IDEnrollment)
        {
            return _context.Enrollment.Any(e => e.IDEnrollment == IDEnrollment);
        }
        private bool AssignmentExists (string IDAssignemnt)
        {
            return _context.Assignment.Any(e => e.IDAssignemnt == IDAssignemnt);
        }
    }
}

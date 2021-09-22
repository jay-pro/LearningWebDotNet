using lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = ("SystemAdmin"))]
    [ApiController]
    public class SystemAdminController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly UserManager<User> _userManager;
        public SystemAdminController(LMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Get list user
        [HttpGet]
        [Route("user-management/get-user-list")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserList (){
            var listUser = await (from u in _context.User
                                  where u.Status == "waitingforapproval" || u.Status == "denied"
                                  select new { 
                                      IDUser = u.Id,
                                      Username = u.UserName,
                                      Fullname = u.Fullname,
                                      Avatar = u.Avatar,
                                      DateOfBirth = u.DateOfBirth,
                                      Major = u.Major,
                                      u.Address,
                                      u.Phone,
                                      u.Email,
                                  }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách user thành công",
                Data = listUser
            });
        }
        //Get list user
        [HttpGet]
        [Route("user-management/get-all-user-list")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserList()
        {
            var listUser = await (from u in _context.User
                                  select new
                                  {
                                      IDUser = u.Id,
                                      Username = u.UserName,
                                      Fullname = u.Fullname,
                                      Avatar = u.Avatar,
                                      DateOfBirth = u.DateOfBirth,
                                      Major = u.Major,
                                      u.Address,
                                      u.Phone,
                                      u.Email,
                                  }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách user thành công",
                Data = listUser
            });
        }
        //Get  user
        [HttpGet]
        [Route("user-management/get-user/{IDUser}")]
        public async Task<ActionResult<User>> GetUser(string IDUser) {
            var user = await (from u in _context.User
                                  where u.Id == IDUser
                                  select new
                                  {
                                      IDUser = u.Id,
                                      Username = u.UserName,
                                      Fullname = u.Fullname,
                                      Avatar = u.Avatar,
                                      DateOfBirth = u.DateOfBirth,
                                      Major = u.Major,
                                      u.Address,
                                      u.Phone,
                                      u.Email,
                                  }).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy user",
                    Data = ""
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy user thành công",
                Data = user
            });
        }
        //Approve user
        [HttpPut]
        [Route("user-management/approve-user/{IDUser}")]
        public async Task<ActionResult<User>> ApproveUser(string IDUser) {
            User user = await _context.User.FindAsync(IDUser);
            if (user  == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            user.Status = "approval";
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Phê duyệt user thành công",
                Data = user
            });
        }
        //Denied user
        [HttpPut] 
        [Route("user-management/deny-user/{IDUser}")]
        public async Task<ActionResult<User>> DenyUser (string IDUser)
        {
            User user = await _context.User.FindAsync(IDUser);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            user.Status = "denied";
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                        Data = "",
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Từ chối Phê duyệt user thành công",
                Data = user
            });
        }
        //lock user
        [HttpPut]
        [Route("user-management/lock-user/{IDUser}")]
        public async Task<ActionResult<User>> LockUser(string IDUser, DateTime dateTime)
        {
            if (dateTime <= DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Thời gian khóa phải lớn hơn thời gian hiện tại",
                    Data = ""
                });
            }
            User user = await _context.User.FindAsync(IDUser);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, dateTime);
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                        Data = "",
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Khóa user thành công",
                Data = user
            });
        }
        //Unlock user
        [HttpPut]
        [Route("user-management/unlock-user/{IDUser}")] 
        public async Task<ActionResult<User>> UnlockUser (string IDUser)
        {
            User user = await _context.User.FindAsync(IDUser);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            DateTime endTimeExpire = DateTime.Now;
            await _userManager.SetLockoutEndDateAsync(user, endTimeExpire);
            await _userManager.SetLockoutEnabledAsync(user, false);
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Mở khóa user thành công",
                Data = user
            });
        }
        //Delete User
        [HttpDelete]
        [Route("user-management/hard-delete-user/{IDUser}")]
        public async Task<ActionResult> HardDeleteUser (string IDUser)
        {
            User user = await _context.User.FindAsync(IDUser);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy người dùng trong cơ sở dữ liệu",
                    Data = "",
                });
            }
            await _userManager.DeleteAsync(user);
            return Ok(new
            {
                StatusCode = 204,
                Message = "Xóa user thành công",
                Data = ""
            });
        }
        //Get waitingforapproval course list
        [HttpGet]
        [Route("course-management/get-course-list")]
        public async Task<ActionResult<IEnumerable<Course>>> GetWaitingforApprovalCourseList()
        {
            var CourseList = await (from c in _context.Course
                                join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                join u in _context.User on i.IDUser equals u.Id
                                where (c.Status == "waitingforapproval" || c.Status == "denied") && c.IsDeleted != true
                                select new {
                                    IDCourse = c.IDCourse,
                                    CourseName = c.CourseName,
                                    Description = c.Description,
                                    Image = c.Image,
                                    Duration = c.Duration,
                                    RegistedNumber = c.RegistedNumber,
                                    Rating = c.Rating,
                                    Field = c.Field,
                                    Status = c.Status,
                                    CreatedAt = c.CreatedAt,
                                    Creator = u.Fullname
                                }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách khóa học thành công",
                Data = CourseList
            });
        }
        //Get a course detail
        [HttpGet]
        [Route("course-management/get-course-detail/{IDCourse}")]
        public async Task<ActionResult<Course>> GetDetailCourse (string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if ( course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học",
                    Data = ""
                });
            }
            var courseDetailList = await (from cd in _context.CourseDetails
                                          where cd.IDCourse == IDCourse
                                          select new {
                                              IDCourseDetail = cd.IDCourseDetail,
                                              LessonName = cd.LessonName,
                                              LessonLink = cd.LessonLink,
                                              Material = cd.Material,
                                              LessonDuration = cd.LessonDuration,
                                              IDCourse = cd.IDCourse
                                          }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy khóa học thành công",
                Data = new  {
                    Course = course,
                    CourseDetail = courseDetailList
                }
            });

        }
        //Approve a course
        [HttpPut]
        [Route("course-management/approve-course/{IDCourse}")]
        public async Task<ActionResult<Course>> ApproveCourse(string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if (course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học",
                    Data = ""
                });
            }
            course.Status = "approval";
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CourseExists(course.IDCourse))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Phê duyệt khóa học thành công",
                Data = course
            });
        }
        //deny a course
        [HttpPut]
        [Route("course-management/deny-course/{IDCourse}")]
        public async Task<ActionResult<Course>> DenyCourse(string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if (course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học",
                    Data = ""
                });
            }
            course.Status = "denied";
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CourseExists(course.IDCourse))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Từ chối phê duyệt khóa học thành công",
                Data = course
            });
        }
        //soft delete course
        [HttpPut]
        [Route("course-management/soft-delete-course/{IDCourse}")]
        public async Task<ActionResult> SoftDeleteCourse(string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if (course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học",
                    Data = ""
                });
            }
            course.IsDeleted = true;
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CourseExists(course.IDCourse))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Xóa khóa học thành công",
                Data = course
            });
        }
        //Restore a course
        [HttpPut]
        [Route("course-management/restore-deleted-course/{IDCourse}")]
        public async Task<ActionResult> RestoredDeletedCourse(string IDCourse)
        {
            var course = await _context.Course.FindAsync(IDCourse);
            if (course == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy khóa học",
                    Data = ""
                });
            }
            course.IsDeleted = false;
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CourseExists(course.IDCourse))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
                        Data = "",
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
                Message = "Phục hồi khóa học thành công",
                Data = course
            });
        }
        //hard delete course
        //[HttpDelete]
        //[Route("course-management/hard-delete-course/{IDCourse}")]
        //public async Task<ActionResult> HardDeleteCourse(string IDCourse)
        //{
        //    var course = await _context.Course.FindAsync(IDCourse);
        //    if (course == null)
        //    {
        //        return NotFound(new
        //        {
        //            StatusCode = 404,
        //            Message = "Không tìm thấy khóa học",
        //            Data = ""
        //        });
        //    }
        //    _context.Course.Remove(course);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (!CourseExists(course.IDCourse))
        //        {
        //            return NotFound(new
        //            {
        //                StatusCode = 404,
        //                Message = "Không tìm thấy khóa học trong cơ sở dữ liệu",
        //                Data = "",
        //            });
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Ok(new
        //    {
        //        StatusCode = 204,
        //        Message = "Xóa khóa học vĩnh viễn thành công",
        //        Data = ""
        //    });
        //}
        //Get list course IsDelete = true
        [HttpGet]
        [Route("course-management/get-deleted-course-list")]
        public async Task<ActionResult<IEnumerable<Course>>> GetDeletedCourseList()
        {
            var CourseList = await (from c in _context.Course
                                    join i in _context.Instructor on c.IDCreator equals i.IDInstuctor
                                    join u in _context.User on i.IDUser equals u.Id
                                    where  c.IsDeleted == true
                                    select new
                                    {
                                        IDCourse = c.IDCourse,
                                        CourseName = c.CourseName,
                                        Description = c.Description,
                                        Image = c.Image,
                                        Duration = c.Duration,
                                        RegistedNumber = c.RegistedNumber,
                                        Rating = c.Rating,
                                        Field = c.Field,
                                        Status = c.Status,
                                        CreatedAt = c.CreatedAt,
                                        Creator = u.Fullname
                                    }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách khóa học thành công",
                Data = CourseList
            });
        }
        //statistic/view-created-course-month
        [HttpGet]
        [Route("statistic/view-course-month")]
        public async Task<ActionResult<IEnumerable<Course>>> CreatedCourseStatisticMonth()
        {
            var lstCourse = await (from c in _context.Course
                                   where c.Status == "approval" && c.IsDeleted != true
                                   select new
                                   {
                                       month = c.CreatedAt.Month,
                                       IDCourse = c.IDCourse
                                   }).ToListAsync();
            List<MonthQuantity> Statistic = new List<MonthQuantity>();
            for (int i = 0; i < 12; i++)
            {
                Statistic.Add(new MonthQuantity(i + 1, 0));
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
        //statistic/class-student-quantity-course
        [HttpGet]
        [Route("statistic/class-student-quantity-course")]
        public async Task<ActionResult> StudentQuantityofClassAccordingtoCourse([FromQuery] string IDCourse)
        {
            var lstStudentQuantity = await (from c in _context.Course
                                            join cl in _context.Class on c.IDCourse equals cl.IDCourse
                                            join e in _context.Enrollment on cl.IDClass equals e.IDClass
                                            where c.IDCourse == IDCourse
                                            group cl by cl.ClassName into g
                                            select new
                                            {
                                                ClassName = g.Key,
                                                Quantity = g.Count()
                                            }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách thống kê số lượng học viên của lớp học theo khóa học",
                Data = lstStudentQuantity
            });
        }
        //statistic/view-quantiy-course-according-to-field
        [HttpGet]
        [Route("statistic/quantiy-course-according-to-field")]
        public async Task<ActionResult> CourseRatioAccordingtoField()
        {
            var lstCourseQuantity = await (from c in _context.Course
                                           group c by c.Field into g
                                           select new
                                           {
                                               Field = g.Key,
                                               Quantity = g.Count()
                                           }).ToListAsync();
            float total = lstCourseQuantity.Sum(l => l.Quantity);
            List<NameRatio> lstCourseRatioField = new List<NameRatio>();
            for (int i = 0; i < lstCourseQuantity.Count; i++)
            {
                float temp = (lstCourseQuantity[i].Quantity / total) * 100;
                lstCourseRatioField.Add(new NameRatio(lstCourseQuantity[i].Field, temp));
            }
             return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách thống kê tỷ lệ lĩnh vực của các khóa học trong hệ thống",
                Data = lstCourseRatioField
             });
        }
        //statistic/course-completion-ratio
        [HttpGet]
        [Route("statistic/course-completion-ratio")]
        public async Task<ActionResult> CourseCompletionRatio([FromQuery] string IDCourse)
        {
            var lstCompletionQuantity = await (from c in _context.Class
                                               join e in _context.Enrollment on c.IDClass equals e.IDClass
                                               where c.IDCourse == IDCourse
                                               group e by c.ClassName into g
                                               select new
                                               {
                                                   ClassName = g.Key,
                                                   CompletionRatio = g.Sum(e => e.Progress >= 100 ? 1 : 0),
                                                   Count = g.Count()
                                               }).ToListAsync();
            List<NameRatio> lstResult = new List<NameRatio>();
            for (int i = 0; i < lstCompletionQuantity.Count; i++)
            {
                float StudentQuantity = lstCompletionQuantity[i].Count;
                var temp = (lstCompletionQuantity[i].CompletionRatio / StudentQuantity) * 100;
                lstResult.Add(new(lstCompletionQuantity[i].ClassName, temp));

            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách tỷ lệ học viên hoàn thành lớp học thành công",
                Data = lstResult
            });
        }
        //statistic/course-quantity-according-to-rating
        [HttpGet]
        [Route("statistic/course-quantity-according-to-rating")]
        public async Task<ActionResult> CourseQuantityAccordingtoRating()
        {
            var lstCourseQuantity = await (from c in _context.Course
                                           group c by c.Rating into g
                                           select new
                                           {
                                               Rating = g.Key,
                                               Quantity = g.Count()
                                           }).ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy số lượng khóa học theo rating thành công",
                Data = lstCourseQuantity
            });
        }

        private bool UserExists (string IDUser)
        {
            return _context.User.Any(u => u.Id == IDUser);
        }
        private bool CourseExists(string IDCourse)
        {
            return _context.Course.Any(e => e.IDCourse == IDCourse);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms.Models;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LMSContext _context;

        public CoursesController(LMSContext context)
        {
            _context = context;
        }

        // GET: api/Courses   lấy tất cả các khóa học
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var allcourse = await _context.Course.ToListAsync();
            if(allcourse == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Không có khóa học nào tồn tại trong db",
                    Data = ""
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sách tất cả các khóa học thành công",
                Data = allcourse
            });
        }

        // Get a course detail
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class CourseDetailDTO
    {
        public string IDCourseDetail { get; set; }
        public string LessonName { get; set; }
        public string LessonLink { get; set; }
        public string Material { get; set; }
        public string LessonDuration { get; set; }
    }
}

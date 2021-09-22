using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class CourseDTO
    {
        public string IDCourse { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
        public int RegistedNumber { get; set; }
        public int Rating { get; set; }
        public string Field { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorName { get; set; }
    }
}

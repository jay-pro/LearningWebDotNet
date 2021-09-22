using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Lesson
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public string ClassId { get; set; }
        public string TeacherId { get; set; }
        public Class Class { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseDetail> courseDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class CourseFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDCourseFeedback { get; set; }
        public string Content { get; set; }
        public int Star { get; set; }
        public DateTime CreateAt { get; set; }
        public string IDUser {get; set;}
        public string IDCourse { get; set; }
        [ForeignKey("IDUser")]
        public User User { get; set; }
        [ForeignKey("IDCourse")]
        public Course Course { get; set; }
    }
}

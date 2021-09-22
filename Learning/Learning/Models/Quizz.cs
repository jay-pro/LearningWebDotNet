using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Quizz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDQuizz { get; set; }
        [Required(ErrorMessage = "Tên bài quizz không được trống")]
        [StringLength(100, ErrorMessage = "Tên bài quizz không được vượt quá 100 từ")]
        public string QuizzName { get; set; }
        [Required(ErrorMessage = "Thời gian làm quizz không được trống")]
        public int Duration { get; set; }
        public int TotalScore { get; set; }
        public int TotalQuestion { get; set; }
        public string IDCourseDetail { get; set; }
        [ForeignKey("IDCourseDetail")]
        public CourseDetail CourseDetail { get; set; }
    }
}

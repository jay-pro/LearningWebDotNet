using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class CourseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDCourseDetail { get; set; }
        [Required(ErrorMessage = "Tên bài học không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Tên bài học không vượt quá 100 ký tự")]
        public string LessonName { get; set; }
        [Required(ErrorMessage = "Tài liệu bài học không được bỏ trống")]
        public string LessonLink { get; set; }
        public string Material { get; set; }
        [Required(ErrorMessage = "Thời lượng bài học không được để trống")]
        public string LessonDuration { get; set; }
        public string IDCourse { get; set; }
        [ForeignKey("IDCourse")]
        public Course Course { get; set; }
        public ICollection<Quizz> Quizz { get; set; }
    }
}

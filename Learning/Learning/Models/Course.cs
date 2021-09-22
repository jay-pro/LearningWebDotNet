using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDCourse { get; set; }
        [Required(ErrorMessage = "Tên khóa học không được trống")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Mô tả khóa học không được trống")]
        [StringLength(5000, ErrorMessage = "Mô tả khóa học quá dài, không được nhiều hơn 5000")]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập thời lượng khóa học")]
        public int Duration { get; set; }
        public int RegistedNumber { get; set; }
        public int Rating { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn lĩnh vực của khóa học")]
        public string Field { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string IDCreator {get; set;}
        [ForeignKey("IDCreator")]
        public Instructor Instructor { get; set; }
        public ICollection<CourseDetail> CourseDetail { get; set; }
    }
}

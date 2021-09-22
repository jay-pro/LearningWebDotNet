using lms.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDClass { get; set; }
        [Required(ErrorMessage = "Tên lớp học không được trống")]
        [StringLength(100, ErrorMessage = "Tên lớp không được vượt quá 100 ký tự")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Lớp học phải có thời gian bắt đầu")]
        [Class_EnsureFutureStartTimeOnCreation]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Lớp học phải có thời gian kết thúc")]
        [Class_EnsureFutureFinishTimeOnCreation]
        [Class_EnsureFinishTimeAfterStartTime]
        public DateTime FinishTime { get; set; }
        public string IDTeacher { get; set; }
        public string IDCourse { get; set; }
        public string IDCreator { get; set; }
        [ForeignKey("IDCreator")]
        public Instructor Instructor { get; set; }
        [ForeignKey("IDTeacher")]
        public Teacher teacher { get; set; }
        [ForeignKey("IDCourse")]
        public Course Course { get; set; }        
        public ICollection<Mentor> Mentors { get; set; }
    }
}

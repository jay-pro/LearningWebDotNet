using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class QuizzDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDQuizzDetail { get; set; }
        [Required(ErrorMessage = "Tên câu hỏi không được trống")]
        public string Question { get; set; }
        public string AChoice { get; set; }
        public string BChoice { get; set; }
        public string CChoice { get; set; }
        public string DChoice { get; set; }
        [Required(ErrorMessage = "Tên câu trả lời không được trống")]
        public string Answer { get; set; }
        public string IDQuizz { get; set; }
        [ForeignKey("IDQuizz")]
        public Quizz Quizz { get; set; }
    }
}

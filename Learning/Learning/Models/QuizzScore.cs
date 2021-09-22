using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class QuizzScore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDQuizzScore { get; set; }
        public string Score { get; set; }
        //public Quizz Quizz { get; set; }
        public string IDQuizz { get; set; }
        [ForeignKey("IDQuizz")]
        public string IDUser { get; set; }
        [ForeignKey("IDUser")]
        public User User { get; set; }
    }
}

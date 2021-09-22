using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class ClassDTO
    {
        public string IDClass { get; set; }
        public string ClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string TeacherName { get; set; }
        public string CreatorName { get; set; }
    }
}

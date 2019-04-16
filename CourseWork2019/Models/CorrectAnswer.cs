using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class CorrectAnswer
    {
        public int CorrectAnswerID { get; set; }
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }

        public Answer Answer { get; set; }
        public Question Question { get; set; }
    }
}

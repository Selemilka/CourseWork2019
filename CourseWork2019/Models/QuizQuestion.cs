using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionID { get; set; }
        public int QuizID { get; set; }
        public int QuestionID { get; set; }

        public Quiz Quiz { get; set; }
        public Question Question { get; set; }
    }
}

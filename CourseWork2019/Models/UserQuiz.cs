using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class UserQuiz
    {
        public int UserQuizID { get; set; }
        public int UserID { get; set; }
        public int QuizID { get; set; }
        
        public User User { get; set; }
        public Quiz Quiz { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        public ICollection<QuizQuestion> QuizQusetions { get; set; }
        public ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}

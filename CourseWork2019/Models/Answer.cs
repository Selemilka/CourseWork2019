using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public string QuestionID { get; set; }
        
        public Question Question { get; set; }
        public ICollection<CorrectAnswer> CorrectAnswers { get; set; }
    }
}

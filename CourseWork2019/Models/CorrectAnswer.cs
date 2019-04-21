using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class CorrectAnswer
    {
        public int CorrectAnswerID { get; set; }
        [Required]
        public int AnswerID { get; set; }
        [Required]
        public int QuestionID { get; set; }

        public Answer Answer { get; set; }
        public Question Question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        [Required]
        [StringLength(500)]
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public int RubricID { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public Rubric Rubric { get; set; }
    }
}

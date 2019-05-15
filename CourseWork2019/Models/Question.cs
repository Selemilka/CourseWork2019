using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Question
    {
        [Display(Name = "ID вопроса")]
        public int QuestionID { get; set; }
        [Required]
        [StringLength(500)]
        [Display(Name = "Название вопроса")]
        public string QuestionName { get; set; }
        [StringLength(5000)]
        [Display(Name = "Текст вопроса")]
        public string QuestionText { get; set; }
        [Display(Name = "Рубрика (тема вопроса)")]
        public int RubricID { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Rubric Rubric { get; set; }
    }
}

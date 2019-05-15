using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Answer
    {
        [Display(Name = "ID ответа")]
        public int AnswerID { get; set; }
        [StringLength(500)]
        [Display(Name = "Текст ответа")]
        public string AnswerText { get; set; }
        [Display(Name = "ID вопроса")]
        public int QuestionID { get; set; }
        [Display(Name = "Является ли ответ правильным?")]
        public bool IsCorrectAnswer { get; set; }

        public Question Question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Название викторины")]
        public string QuizName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}

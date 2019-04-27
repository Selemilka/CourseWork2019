using CourseWork2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.ViewModels
{
    public class QuestionDetailsModel
    {
        public QuestionDetailsModel(Question question, List<Answer> answers)
        {
            Question = question;
            Answers = answers;
        }

        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork2019.Models;

namespace CourseWork2019.ViewModels
{
    public class TaskModel
    {
        public Quiz Quiz { get; set; }
        public List<Question> Questions { get; set; }

        public TaskModel(Quiz quiz, List<Question> questions)
        {
            Quiz = quiz;
            Questions = questions;
        }
    }
}

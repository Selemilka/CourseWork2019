using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuizContext context)
        {
            context.Database.EnsureCreated();
             
            if (context.Questions.Any())
            {
                return;
            }
        }
    }
}

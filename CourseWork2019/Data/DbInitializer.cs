using CourseWork2019.Models;
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
            
            if (!context.Roles.Any() || context.Roles.Any())
            {
                    context.Roles.Add(new Role() { Name = "user" });
                    context.Roles.Add(new Role() { Name = "admin" });
                context.SaveChanges();
            }

            if (context.Questions.Any())
            {
                return;
            }
        }
    }
}

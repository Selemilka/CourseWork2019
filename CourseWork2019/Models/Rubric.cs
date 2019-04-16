using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Rubric
    {
        public int RubricID { get; set; }
        public string RubricName { get; set; }
        
        public ICollection<Question> Questions { get; set; }
    }
}

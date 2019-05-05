using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Counter
    {
        public int CounterID { get; set; }
        public string Message { get; set; }
        public int? UserID { get; set; }

        public User User { get; set; }
    }
}

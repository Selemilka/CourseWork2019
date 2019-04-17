using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public IEnumerable<UserQuiz> UserQuizzes { get; set; }
    }
}

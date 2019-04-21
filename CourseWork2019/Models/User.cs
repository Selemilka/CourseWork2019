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
        [Required]
        [StringLength(25)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstMidName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public IEnumerable<UserQuiz> UserQuizzes { get; set; }
    }
}

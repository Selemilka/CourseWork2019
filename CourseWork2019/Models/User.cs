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
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [StringLength(50)]
        [Display(Name = "Отчество")]
        public string FirstMidName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Адрес электронной почты")]
        public string EmailAddress { get; set; }

        public UserStatus Status { get; set; }

        public IEnumerable<UserQuiz> UserQuizzes { get; set; }
    }

    public enum UserStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}

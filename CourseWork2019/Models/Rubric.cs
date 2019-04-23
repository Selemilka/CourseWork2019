using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Models
{
    public class Rubric
    {
        public int RubricID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Название рубрики")]
        public string RubricName { get; set; }
        
        public ICollection<Question> Questions { get; set; }
    }
}

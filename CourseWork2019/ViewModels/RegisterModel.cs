﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан e-mail!")]
        [DataType(DataType.Password, ErrorMessage = "E-mail не соответствует формату!")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Не указан пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

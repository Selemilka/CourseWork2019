using CourseWork2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.ViewModels
{
    public class UsersListModel
    {
        public UsersListModel(List<User> users)
        {
            Users = users;
        }
        public List<User> Users { get; set; }
    }
}

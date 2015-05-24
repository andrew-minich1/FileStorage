using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileStorage.Models.AccountViewModel
{
    public class UserViewModel
    {
        public enum Role
        {
            User = 1,
            Admin
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
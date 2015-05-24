using System;
using System.Collections.Generic;

namespace BLL_Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public  List<RoleEntity> Roles { get; set; }

        public List<FileEntity> Files { get; set; }
    }
}

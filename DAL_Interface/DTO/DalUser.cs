using System;
using System.Collections.Generic;

namespace DAL_Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }

        public string Login {get;set;}

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public  List<DalRole> Roles { get; set; }

        public List<DalFile> Files { get; set; }

    }
}

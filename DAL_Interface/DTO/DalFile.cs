
using System;

namespace DAL_Interface.DTO
{
    public class DalFile:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDelete { get; set; }

        public bool IsOpen { get; set; }

        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public double Size { get; set; }
    }
}

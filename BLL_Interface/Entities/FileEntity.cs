using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Interface.Entities
{
    public class FileEntity
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

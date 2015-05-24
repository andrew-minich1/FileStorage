using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileStorage.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public int UserId { get; set; }

        public bool IsOpen { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DateCreated { get; set; }

        public string Type { get; set; }

        public double Size { get; set; }
    }

    public class FilesViewModel
    {
        public List<FileViewModel> Files;
        public List<FileViewModel> DeleteFiles;
        public int CountFiles;
        public int CountDeleteFiles;
        public double TotalSize;
    }
}
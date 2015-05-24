namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool IsDelete { get; set; }

        public bool IsOpen { get; set; }

        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(500)]
        public string Path { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public double Size { get; set; }

        public virtual User User { get; set; }
    }
}

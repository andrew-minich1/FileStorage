namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(300)]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}

namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FileStorageEntityModel : DbContext
    {
        public FileStorageEntityModel()
            : base("name=FileStorageEntityModel")
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserInRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}

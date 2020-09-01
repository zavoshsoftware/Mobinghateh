using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Role:BaseEntity
    {
        internal class Configuration : EntityTypeConfiguration<User>
        {
            public Configuration()
            {
                HasRequired(p => p.Role)
                    .WithMany(j => j.Users)
                    .HasForeignKey(p => p.RoleId);
            }
        }
        [Required]
        public string Title { get; set; }
        public string Name { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
        public ICollection<User> Users { get; set; }
    }
}
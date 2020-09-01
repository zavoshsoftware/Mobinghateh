using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
             System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
            
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //   modelBuilder.Entity<User>().HasMany(current=>current.Marketer).WithRequired().WillCascadeOnDelete(false);
        //  //  modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<TextType> TextTypes { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ProducImage> ProducImages { get; set; }
        public DbSet<ProductImageType> ProductImageTypes { get; set; }
        public DbSet<ResumeForm> ResumeForms { get; set; }
        public DbSet<TextTypeItem> TextTypeItems { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ContactusForm> ContactusForms { get; set; }

        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
    }
}

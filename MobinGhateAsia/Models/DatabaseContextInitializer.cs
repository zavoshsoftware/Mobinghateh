using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal static class DatabaseContextInitializer
    {
        static DatabaseContextInitializer()
        {

        }

        internal static void Seed(DatabaseContext databaseContext)
        {
            Guid roleId=Guid.NewGuid();
            
            InsertInnerSlider(databaseContext);
            InsertBaseRole(databaseContext, roleId);
            InsertBaseUser(databaseContext, roleId);
            databaseContext.SaveChanges();

        }

        internal static void InsertBaseRole(DatabaseContext databaseContext, Guid roleId)
        {
            Role role = new Role()
            {
                Id = roleId,
                Title = "مدیر وب سایت",
                Name = "Administrator",
                SubmitDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.Roles.Add(role);
        }

        internal static void InsertBaseUser(DatabaseContext databaseContext, Guid roleId)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = roleId,
                Username = "baseuser",
                Password = "user2rcv@1!3#",
                FirstName = "baseuser",
                LastName = "baseuser",
                SubmitDate = DateTime.Now,
                IsDelete = false
            };

            databaseContext.Users.Add(user);
        }
       
        internal static void InsertInnerSlider(DatabaseContext databaseContext)
        {
            TextType slideType = new TextType()
            {
                //
                Id = new Guid("2f4d1d33-5566-48c8-bfdd-1d15f93f7898"),
                Title ="اسلاید داخلی",
                IsDelete=false,
                SubmitDate=DateTime.Now,

            };
            databaseContext.TextTypes.Add(slideType);

            TextTypeItem slide = new TextTypeItem()
            {
                Id = new Guid("3be879cd-a54c-4014-a165-bba718252d7c"),
                Title = "اسلاید داخلی",
                IsDelete = false,
                SubmitDate = DateTime.Now,
                TextTypeId=slideType.Id,
                ImageUrl= "/images/"
            };
            databaseContext.TextTypeItems.Add(slide);
        }

     
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Gallery:BaseEntity
    {
        public Gallery()
        {
            GalleryImages=new List<GalleryImage>();
        }

        //internal class Configuration : EntityTypeConfiguration<GalleryImage>
        //{
        //    public Configuration()
        //    {
        //       HasOptional(p => p.Gallery)
        //            .WithMany(j => j.GalleryImages)
        //            .HasForeignKey(p => p.GalleryId);
        //    }
            
        //}
        [Display(Name = "عنوان گروه")]
        [MaxLength(200,ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [MaxLength(200,ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        [Display(Name = "عنوان گروه انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }
        public ICollection<GalleryImage> GalleryImages { get; set; }


        Helpers.GetCulture oGetCulture = new Helpers.GetCulture();

     [NotMapped]
        public string TitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TitleEn;
                    case "fa-ir":
                        return this.Title;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
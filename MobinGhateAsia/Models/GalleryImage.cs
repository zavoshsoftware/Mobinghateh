using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class GalleryImage:BaseEntity
    {
        [Display(Name = "عنوان")]
        [MaxLength(200,ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(200,ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }

        [Display(Name = "عنوان گروه")]
        public Guid GalleryId { get; set; }

        public Gallery Gallery { get; set; }
        internal class configuration : EntityTypeConfiguration<GalleryImage>
        {
            public configuration()
            {
                HasRequired(p => p.Gallery).WithMany(t => t.GalleryImages).HasForeignKey(p => p.GalleryId);
            }
        }

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
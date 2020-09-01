using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Product:BaseEntity
    {
        [Display(Name = "گروه محصول")]
        public Guid ProductGroupId { get; set; }
        [Display(Name = "عنوان محصول")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "خلاصه")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        [Display(Name = "توضیحات")]
        [UIHint("RichText")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        [Display(Name = "تصویر سه بعدی")]
        [MaxLength(200)]
        public string FlashImageUrl { get; set; }

        [Display(Name = "عنوان محصول انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }
        [Display(Name = "خلاصه انگلیسی")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string SummaryEn { get; set; }
        [Display(Name = "توضیحات انگلیسی")]
        [UIHint("RichText")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string BodyEn { get; set; }

        [Display(Name = "در صفحه اصلی باشد؟")]
        public bool IsInHome { get; set; }
        public ProductGroup ProductGroup { get; set; }

        internal class configuration : EntityTypeConfiguration<Product>
        {
            public configuration()
            {
                HasRequired(p => p.ProductGroup).WithMany(t => t.Products).HasForeignKey(p => p.ProductGroupId);
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

        [NotMapped]
        public string BodySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.BodyEn;
                    case "fa-ir":
                        return this.Body;
                    default:
                        return String.Empty;
                }
            }
        }


        [NotMapped]
        public string SummarySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SummaryEn;
                    case "fa-ir":
                        return this.Summary;
                    default:
                        return String.Empty;
                }
            }
        }

        [Display(Name = "توضیحات متا")]
        [MaxLength(320, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

        [Display(Name = "توضیحات متا انگلیسی")]
        [MaxLength(320, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string MetaDescriptionEn { get; set; }

        [NotMapped]
        public string MetaDescriptionSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.MetaDescriptionEn;
                    case "fa-ir":
                        return this.MetaDescription;
                    default:
                        return String.Empty;
                }
            }
        }

    }
}
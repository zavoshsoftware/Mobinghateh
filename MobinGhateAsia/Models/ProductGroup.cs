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
    public class ProductGroup : BaseEntity
    {
        public ProductGroup()
        {
            Products = new List<Product>();
            ProductGroups = new List<ProductGroup>();
        }
        [Display(Name = "عنوان گروه محصول")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "عنوان گروه محصول انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }

        [Display(Name = "توضیحات گروه محصول")]
        [MaxLength(4000, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [UIHint("RichText")]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "توضیحات گروه محصول انگلیسی")]
        [MaxLength(2000, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [UIHint("RichText")]
        [AllowHtml]
        public string DescriptionEn { get; set; }
   
        [Display(Name = "اولویت نمایش")]
        public int Priority { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }

        public Guid? ParentId { get; set; }
        public virtual ProductGroup Parent { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
      

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

        internal class configuration : EntityTypeConfiguration<ProductGroup>
        {
            public configuration()
            {
                HasOptional(p => p.Parent).WithMany(t => t.ProductGroups).HasForeignKey(p => p.ParentId);
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
        public string DescriptionSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.DescriptionEn;
                    case "fa-ir":
                        return this.Description;
                    default:
                        return String.Empty;
                }
            }
        }

    }
}
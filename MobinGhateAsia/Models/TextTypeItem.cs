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
    public class TextTypeItem:BaseEntity
    {
        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "خلاصه اول")]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summary1 { get; set; }
        [Display(Name = "خلاصه دوم")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summary2 { get; set; }
        [Display(Name = "متن توضیحات")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Body { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        public Guid TextTypeId { get; set; }

        public TextType TextType { get; set; }


        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }
        [Display(Name = "خلاصه اول انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summary1En { get; set; }
        [Display(Name = "خلاصه دوم انگلیسی")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summary2En { get; set; }
        [Display(Name = "متن توضیحات انگلیسی")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string BodyEn { get; set; }
        internal class configuration : EntityTypeConfiguration<TextTypeItem>
        {
            public configuration()
            {
                HasRequired(p => p.TextType).WithMany(t => t.TextTypeItems).HasForeignKey(p => p.TextTypeId);
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


        public string Summary1Srt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.Summary1En;
                    case "fa-ir":
                        return this.Summary1;
                    default:
                        return String.Empty;
                }
            }
        }
        public string Summary2Srt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.Summary2En;
                    case "fa-ir":
                        return this.Summary2;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Blog : BaseEntity
    {

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "خلاصه")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Summery { get; set; }
        [Display(Name = "توضیحات")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        [MaxLength(200)]
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int VisitNumber { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }
        [Display(Name = "خلاصه انگلیسی")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string SummeryEn { get; set; }
        [Display(Name = "توضیحات انگلیسی")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string DetailEn { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

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
        public string DetailSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.DetailEn;
                    case "fa-ir":
                        return this.Detail;
                    default:
                        return String.Empty;
                }
            }
        }


        [NotMapped]
        public string SummerySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SummeryEn;
                    case "fa-ir":
                        return this.Summery;
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
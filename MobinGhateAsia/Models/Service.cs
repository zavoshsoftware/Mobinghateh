using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Service:BaseEntity
    {
        [Display(Name = "عنوان خدمت")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا عنوان {0} را وارد نمایید")]
        public string Title { get; set; }
        [Display(Name = "اولویت نمایش")]
        public int? Priority { get; set; }

        [Display(Name = "عنوان انگلیسی خدمت")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا عنوان {0} را وارد نمایید")]
        public string TitleEn { get; set; }

        [Display(Name = "توضیحات")]
        [UIHint("RichText")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }

        [Display(Name = "توضیحات انگلیسی")]
        [UIHint("RichText")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string BodyEn { get; set; }

        [Display(Name = "تصویر هدر")]
        public string HeaderImageUrl { get; set; }

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

        public virtual ICollection<ServiceImage> ServiceImages { get; set; }

        [Display(Name="در صفحه اصلی باشد؟")]
        public bool IsInHome { get; set; }
        [Display(Name="توضیحات صفحه اصلی")]
        [DataType(DataType.MultilineText)]
        public string HomeSummery { get; set; }
        [Display(Name="توضیحات صفحه اصلی انگلیسی")]
        [DataType(DataType.MultilineText)]
        public string HomeSummeryEn { get; set; }


        [NotMapped]
        public string HomeSummerySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.HomeSummeryEn;
                    case "fa-ir":
                        return this.HomeSummery;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
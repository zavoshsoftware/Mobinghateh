using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Certificate : BaseEntity
    {
   

        [Display(Name = "شرکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Company { get; set; }

        [Display(Name = " شرکت انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string CompanyEn { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
       
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        
        public string TitleEn { get; set; }

        [Display(Name = "تصویر")]
        
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        
        public string Subject { get; set; }

        [Display(Name = "موضوع انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string SubjectEn { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        Helpers.GetCulture oGetCulture = new Helpers.GetCulture();
        public string CompanySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.CompanyEn;
                    case "fa-ir":
                        return this.Company;
                    default:
                        return String.Empty;
                }
            }
        }

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
        public string SubjectSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SubjectEn;
                    case "fa-ir":
                        return this.Subject;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
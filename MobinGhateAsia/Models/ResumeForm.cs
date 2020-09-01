using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class ResumeForm:BaseEntity
    {
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string NationalCode { get; set; }
        [Display(Name = "تلفن")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Phone { get; set; }
        [Display(Name = "موبایل")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Mobile { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(260, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Email { get; set; }
        [Display(Name = "رزومه")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ResumeFile { get; set; }
    }
}
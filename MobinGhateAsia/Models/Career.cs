using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Career:BaseEntity
    {
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا عنوان {0} را وارد نمایید")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا عنوان {0} را وارد نمایید")]
        public string LastName { get; set; }
        [Display(Name = "تلفن همراه")]
        [MaxLength(25, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا عنوان {0} را وارد نمایید")]
        [RegularExpression(@"(^(09|9)[1][1-9]\d{7}$)|(^(09|9)[3][12456]\d{7}$)", ErrorMessage = "شماره موبایل وارد شده صحیح نمی باشد")]
        public string CellNumber { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(300, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Email { get; set; }
    }
}
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class ContactusForm : BaseEntity
    {
        [Display(Name = "نام کاربر")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        public string Fullname { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        public string Email { get; set; }
        [Display(Name = "پیغام")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        public string Message { get; set; }
    }
}
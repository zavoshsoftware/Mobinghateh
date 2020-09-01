using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class NewsLetter:BaseEntity
    {
        [Required(ErrorMessage ="ایمیل خود را وارد نمایید.")]
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
    }
}
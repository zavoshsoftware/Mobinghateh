using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Customer:BaseEntity
    {
        [Display(Name = "مشتری")]
        public string Title { get; set; }
         
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
    }
}
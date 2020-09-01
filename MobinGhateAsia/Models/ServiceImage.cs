using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ServiceImage:BaseEntity
    {
        [Display(Name = "عنوان خدمت")]
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }

        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
    }
}
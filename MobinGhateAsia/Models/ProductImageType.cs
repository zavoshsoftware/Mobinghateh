using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductImageType:BaseEntity
    {
        public ProductImageType()
        {
            ProducImages=new List<ProducImage>();
        }
        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        public ICollection<ProducImage> ProducImages { get; set; }
    }
}
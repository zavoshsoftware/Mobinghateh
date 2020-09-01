using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime SubmitDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
    }
}
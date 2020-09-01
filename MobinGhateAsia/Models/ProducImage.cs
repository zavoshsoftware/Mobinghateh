using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProducImage:BaseEntity
    {
        //public ProducImage()
        //{
        //    ProducImages=new List<ProducImage>();
        //}
        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductImageTypeId { get; set; }

        public Product Product { get; set; }
        public ProductImageType ProductImageType { get; set; }
        // public ICollection<ProducImage> ProducImages { get; set; }
        internal class configuration : EntityTypeConfiguration<ProducImage>
        {
            public configuration()
            {
                HasRequired(p => p.ProductImageType).WithMany(t => t.ProducImages).HasForeignKey(p => p.ProductImageTypeId);
            }
        }
    }
}
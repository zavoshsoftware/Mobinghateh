using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductDetailViewModel : _LayoutViewModel
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<ProducImage> DetailProductImages { get; set; }
        public List<ProducImage> TechnicalProductImages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductListViewModel:_LayoutViewModel
    {
        public List<Product> Products { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}
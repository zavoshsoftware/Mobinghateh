using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductGroupListViewModel:_LayoutViewModel
    {
        public List<ProductGroup> ProductGroups { get; set; }
        public ProductGroup CurrentProductGroup { get; set; }
    }
}
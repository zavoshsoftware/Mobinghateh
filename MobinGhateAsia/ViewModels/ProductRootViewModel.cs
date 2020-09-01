using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ProductRootViewModel:_LayoutViewModel
    {
        public List<ProductGroupItem> ProductGroups { get; set; }
        public TextTypeItem PageTextItem { get; set; }
    }

    public class ProductGroupItem
    {
        public Guid Id { get; set; }
        public string TitleSrt { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
    }
    
}
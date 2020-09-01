using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class _LayoutViewModel
    {
        public List<MenuProductGroup> MenuProductGroupViewModel { get; set; }
        public List<Service> Services { get; set; }
        public _FooterViewModel Footer { get; set; }
        public TextTypeItem InnerSlide { get; set; }
        public List<Blog> FooterBlogs { get; set; }
    }

    public class MenuProductGroup
    {
        public ProductGroup ParentProductGroup { get; set; }
        public List<ProductGroup> ChildProductGroups { get; set; }
    }
}
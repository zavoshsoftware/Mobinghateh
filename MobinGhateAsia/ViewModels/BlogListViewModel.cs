using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class BlogListViewModel:_LayoutViewModel
    {
        public List<Blog> Blogs { get; set; }
    }
}
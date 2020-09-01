using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ServiceListViewModel : _LayoutViewModel
    {
        public List<Service> Services { get; set; }
        public TextTypeItem ServiceText { get; set; }
    }
}
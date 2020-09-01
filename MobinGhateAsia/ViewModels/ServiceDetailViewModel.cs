using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class ServiceDetailViewModel : _LayoutViewModel
    {
        public Service Service { get; set; }
        public List<ServiceImage> ServiceImages { get; set; }
    }
}
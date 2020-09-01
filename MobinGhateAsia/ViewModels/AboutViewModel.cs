using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class AboutViewModel : _LayoutViewModel
    {
        public TextTypeItem AboutCompanyText { get; set; }
        public TextTypeItem CompanyVisionText { get; set; }
        public List<Certificate> Certificates { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class _FooterViewModel
    {
        public TextTypeItem FooterText { get; set; }
        public List<Blog> Blogs { get; set; }

        public TextTypeItem AddressText { get; set; }
        public TextTypeItem PhoneText { get; set; }
        public TextTypeItem FaxText { get; set; }
        public TextTypeItem EmailText { get; set; }
        public string ZavoshLink { get; set; }
    }
}
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Helpers
{
    public class MenuData
    {
        DatabaseContext db = new DatabaseContext();

        #region Menu

       
        public List<MenuProductGroup> MenuProductGroup()
        {
            List<MenuProductGroup> menuGroups=new List<MenuProductGroup>();

            List<ProductGroup> parentProductGroups =
                db.ProductGroups.Where(c => c.ParentId == null && c.IsDelete == false).OrderBy(c=>c.Priority).ToList();

            foreach (ProductGroup productGroup in parentProductGroups)
            {
                menuGroups.Add(new MenuProductGroup()
                {
                    ParentProductGroup = productGroup,
                    ChildProductGroups =
                        db.ProductGroups.Where(c => c.ParentId == productGroup.Id && c.IsDelete == false).OrderBy(c => c.Priority).ToList()
                });
            }
            return menuGroups;
        }
     
        public List<Service> GetMenuServices()
        {
            return db.Services.Where(current => current.IsDelete == false).OrderByDescending(current=>current.Priority).ToList();
        }
      
        public List<Blog> GetFooterBlog()
        {
            return db.Blogs.Where(current => current.IsDelete == false).OrderByDescending(current=>current.SubmitDate).ToList();
        }

        public _FooterViewModel ReturnFooterViewModel()
        {
            _FooterViewModel footer = new _FooterViewModel()
            {
                Blogs = ReturnRecentBlog(),
                FooterText = ReternFooterText("226992e0-ce36-46d2-9fe9-b3740c53c5ab"),
                AddressText = ReternFooterText("5d0b59b0-f5ba-4c13-943c-fdd002cd3dc9"),
                PhoneText = ReternFooterText("638e0195-568f-4df3-ad0a-c179cfebc409"),
                FaxText = ReternFooterText("9169d906-4894-4386-b1ee-321a3fbdd80b"),
                EmailText = ReternFooterText("ad3f9a5f-7202-406a-b0b7-91672366c3ac"),
                ZavoshLink = GetFooterLink()
            };
            return footer;
        }

        public string GetFooterLink()
        {
            string url = HttpContext.Current.Request.Url.PathAndQuery;
            if (url.ToLower() == "/fa")
                return
                    "<a target='_blank' href='http://www.zavosh.org/service/information-technology-solutions/website-portal'>زاوش</a>";
            if (url.ToLower() == "/en")
                return
                    "<a target='_blank' href='http://www.zavosh.org/service/information-technology-solutions/website-portal'>ZAVOSH</a>";

            else
                return "zavosh";
        }
      
    

    public List<Blog> ReturnRecentBlog()
        {
            return db.Blogs.Where(current => current.IsDelete == false).OrderBy(current => current.Order).ThenByDescending(current => current.SubmitDate)
                .Take(3).ToList();
        }

        public TextTypeItem ReternFooterText(string id)
        {
            return db.TextTypeItems.Find(new Guid(id));
        }

        public TextTypeItem ReturnInnerSlider()
        {
            return db.TextTypeItems.Find(new Guid("3be879cd-a54c-4014-a165-bba718252d7c"));
        }

        #endregion

        #region Culture

        public void SetCurrentCulture(string language)
        {
            if (language == null)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fa-IR");
                SetCookie("fa_IR");
            }
            else
            {
                if (language.ToLower() == "fa")
                {
                    SetCookie("fa_IR");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fa-IR");

                }
                else
                {
                    SetCookie("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
                }
            }
        }

        private const string LanguageCookieName = "MyLanguageCookieName";

        private void SetCookie(string niputCoockie)
        {
            var lang = niputCoockie;
            var httpCookie = new HttpCookie(LanguageCookieName, lang) { Expires = DateTime.Now.AddYears(1) };
            HttpContext.Current.Response.SetCookie(httpCookie);
        }

        #endregion

    }
}
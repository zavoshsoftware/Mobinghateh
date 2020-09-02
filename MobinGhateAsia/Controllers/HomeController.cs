using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using Models.ViewModels;

namespace MobinGhateAsia.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        [Route("{language?}")]
        public ActionResult Index(string language)
        {
            if (language == null)
                return RedirectPermanent("/fa");

            else if (language.ToLower() == "contact")
                return RedirectToAction("contact", "Home", new { language = "fa" });

            else if (language.ToLower() == "about")
                return RedirectToAction("about", "Home", new { language = "fa" });

            else if (language.ToLower() == "service")
                return RedirectToAction("List", "Services", new { language = "fa" });

            else if (language.ToLower() == "blog")
                return RedirectToAction("List", "Blogs", new { language = "fa" });

            else if (language.ToLower() == "gallery")
                return RedirectToAction("List", "Galleries", new { language = "fa" });

            else if (language.ToLower() == "career")
                return RedirectToAction("Request", "Careers", new { language = "fa" });

            else if (language.ToLower() == "login")
                return RedirectToAction("login", "Account");

            Helpers.MenuData menuData = new MenuData();

            menuData.SetCurrentCulture(language);

            HomeViewModel homeViewModel = new HomeViewModel
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                WhyTextItems = ReturnTextItems(new Guid("7c1e6a81-8a09-449b-bcc0-2ce5c55affbe"), new Guid("cbbc2db6-49bd-42a2-9e95-053eb5672b9d")),
                AboutTextItem = ReturnSigleTextItem(new Guid("10d2aaa2-ab6b-4be8-ab77-609b6729fd8e")),
                AboutTextItemSecond = ReturnSigleTextItem(new Guid("8d543118-881f-4d01-bbe7-8e6ec5559e6a")),
                RecentBlogs = ReturnRecentBlog(),
                Footer = menuData.ReturnFooterViewModel(),
                Products = ReturnTopProducts(),
                WhyTitle = db.TextTypeItems.Find(new Guid("cbbc2db6-49bd-42a2-9e95-053eb5672b9d")),
                InnerSlide = menuData.ReturnInnerSlider(),
                Sliders = db.Sliders.Where(p => p.IsDelete == false).OrderBy(t => t.Priority).ToList(),
                Services = menuData.GetMenuServices(),
                Customers = db.Customers.Where(c=>c.IsDelete==false).ToList(),
                FooterBlogs = menuData.GetFooterBlog(),
                HomeServices = db.Services.Where(c=>c.IsInHome&&c.IsDelete==false).Take(4).ToList(),
                AfterSliderFirst = db.TextTypeItems.Find(new Guid("7a8377cd-cff8-4b9b-9925-2d86ace80475")),
                AfterSliderSecond = db.TextTypeItems.Find(new Guid("951ca777-155a-4756-941d-dbc5d1c878fa"))
            };
            
            return View(homeViewModel);
        }


        public List<Blog> ReturnRecentBlog()
        {
            return db.Blogs.Where(current => current.IsDelete == false).OrderBy(current => current.Order)
                .ThenByDescending(current => current.SubmitDate).Take(3).ToList();
        }
        public List<TextTypeItem> ReturnTextItems(Guid typeId, Guid? constraint)
        {
            if (constraint == null)
                return db.TextTypeItems.Where(current => current.TextTypeId == typeId).ToList();
            else
                return db.TextTypeItems.Where(current => current.TextTypeId == typeId && current.Id != constraint).ToList();

        }
        public TextTypeItem ReturnSigleTextItem(Guid typeId)
        {
            return db.TextTypeItems.Find(typeId);
        }

        public List<Product> ReturnTopProducts()
        {
            return db.Products.Where(current => current.IsInHome == true && current.IsDelete == false).ToList();
        }
        [Route("{language}/contact")]
        public ActionResult Contact(string language)
        {
            Helpers.MenuData menuData = new MenuData();

            menuData.SetCurrentCulture(language);

            ContactViewModel contactViewModel = new ContactViewModel
            {
                Footer = menuData.ReturnFooterViewModel(),
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                InnerSlide = menuData.ReturnInnerSlider(),
                Services = menuData.GetMenuServices(),
                FooterBlogs = menuData.GetFooterBlog()
            };

            return View(contactViewModel);
        }

        [Route("{language}/about")]
        public ActionResult About(string language)
        {
            Helpers.MenuData menuData = new MenuData();
            menuData.SetCurrentCulture(language);

            AboutViewModel aboutViewModel = new AboutViewModel
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                Footer = menuData.ReturnFooterViewModel(),
                AboutCompanyText = db.TextTypeItems.Find(new Guid("afdd0f19-9f09-4b54-af87-9b05ae38e28a")),
                CompanyVisionText = db.TextTypeItems.Find(new Guid("ce2f47cf-acc9-4160-8dd1-f46edc8161ff")),
                InnerSlide = menuData.ReturnInnerSlider(),
                Certificates = db.Certificates.Where(p => p.IsDelete == false).OrderByDescending(c=>c.Order).ToList(),
                Services = menuData.GetMenuServices(),
                FooterBlogs = menuData.GetFooterBlog()

            };

            return View(aboutViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RequestPostNewsLetter(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
            }
            else
            {
                InsertNewsletter(email);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
        }

        public void InsertNewsletter(string Email)
        {
            NewsLetter nl = new Models.NewsLetter()
            {
                Id = Guid.NewGuid(),
                IsDelete = false,
                SubmitDate = DateTime.Now,
                Email = Email

            };
            db.NewsLetters.Add(nl);
            db.SaveChanges();
        }


    }
}
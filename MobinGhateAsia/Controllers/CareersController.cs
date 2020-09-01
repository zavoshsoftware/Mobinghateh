using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.ViewModels;
using System.Text.RegularExpressions;

namespace MobinGhateAsia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CareersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Careers
        public ActionResult Index()
        {
            return View(db.Careers.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }
 
        // GET: Careers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = db.Careers.Find(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Career career = db.Careers.Find(id);
			career.IsDelete=true;
			career.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        [Route("{language}/career")]
        public ActionResult Request(string language)
        {
            CareerViewModel career=new CareerViewModel();
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);
            career.MenuProductGroupViewModel = menuData.MenuProductGroup();
            career.Career = null;
            career.Footer = menuData.ReturnFooterViewModel();
            career.InnerSlide = menuData.ReturnInnerSlider();
            return View(career);
        }


        [AllowAnonymous]
        public ActionResult RequestPost(string firstName,string lastName,string cellNumber,string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
            bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[1][1-9]\d{7}$)|(^(09|9)[3][12456]\d{7}$)", RegexOptions.IgnoreCase);

            if (!isEmail)
                return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
            if (!isValidMobile)
                return Json("InvalidMobile", JsonRequestBehavior.AllowGet);

            if(isEmail&&isValidMobile)
            {
                Career cf = new Career()
                {
                    CellNumber = cellNumber,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    SubmitDate = DateTime.Now,
                    IsDelete = false,
                    Id = Guid.NewGuid(),
                };
               

                db.Careers.Add(cf);
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else 
                return Json("false", JsonRequestBehavior.AllowGet);
        }
    }
}

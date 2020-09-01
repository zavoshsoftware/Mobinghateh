using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.ViewModels;

namespace MobinGhateAsia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Services
        public ActionResult Index()
        {
            ViewBag.Title = "فهرست خدمات";
            return View(db.Services.Where(a => a.IsDelete == false).OrderBy(a => a.SubmitDate).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Title = "افزودن خدمت";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    service.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                service.IsDelete = false;
                service.SubmitDate = DateTime.Now;
                service.Id = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Title = "افزودن خدمت";
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = "ویرایش خدمت";
            return View(service);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    service.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                service.LastModificationDate = DateTime.Now;
                service.IsDelete = false;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Title = "ویرایش خدمت";
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "حذف خدمت";
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service service = db.Services.Find(id);
            service.IsDelete = true;
            service.DeleteDate = DateTime.Now;

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
        [Route("{language}/service")]
        public ActionResult List(string language)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();

            menuData.SetCurrentCulture(language);

            ServiceListViewModel service = new ServiceListViewModel();
            service.Services = db.Services.Where(current => current.IsDelete == false)
                 .OrderBy(current => current.Priority).ToList();
            service.MenuProductGroupViewModel = menuData.MenuProductGroup();
            service.Footer = menuData.ReturnFooterViewModel();
            service.ServiceText = db.TextTypeItems.Find(new Guid("3488bb81-93f7-4c9e-8852-f140e9af8855"));
            service.InnerSlide = menuData.ReturnInnerSlider();
            service.Services = menuData.GetMenuServices();
            service.FooterBlogs = menuData.GetFooterBlog();


            if (language == "fa")
            {
                ViewBag.Title = "مبین قطعه آسیا";
                ViewBag.Description = "مبین قطعه آسیا";
            }
            else
            {
                ViewBag.Title = "MobinGhateAsia";
            }

            return View(service);
        }

        [AllowAnonymous]
        [Route("{language}/service/{id:Guid}")]
        public ActionResult Details(string language, Guid id)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            Service ser = db.Services.FirstOrDefault(current => current.Id == id && current.IsDelete == false);
            menuData.SetCurrentCulture(language);

            ServiceDetailViewModel service = new ServiceDetailViewModel();

            service.Services = db.Services.Where(current => current.IsDelete == false)
                .OrderBy(current => current.Priority).ToList();
            service.MenuProductGroupViewModel = menuData.MenuProductGroup();
            service.Footer = menuData.ReturnFooterViewModel();
            service.InnerSlide = menuData.ReturnInnerSlider();
            service.Services = menuData.GetMenuServices();
            service.Service = ser;
            service.ServiceImages = db.ServiceImages.Where(c => c.IsDelete == false && c.ServiceId == ser.Id).ToList();
            service.FooterBlogs = menuData.GetFooterBlog();



            if (language == "fa")
            {
                ViewBag.Title = "مبین قطعه آسیا";
                ViewBag.Description = "مبین قطعه آسیا";
            }
            else
            {
                ViewBag.Title = "MobinGhateAsia";
            }


            return View(service);
        }
    }
}

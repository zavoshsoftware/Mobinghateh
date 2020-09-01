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

namespace MobinGhateAsia.Controllers
{
    public class ServiceImagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceImages
        public ActionResult Index()
        {
            var serviceImages = db.ServiceImages.Include(s => s.Service).Where(s=>s.IsDelete==false).OrderByDescending(s=>s.SubmitDate);
            return View(serviceImages.ToList());
        }

        // GET: ServiceImages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = db.ServiceImages.Find(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }
            return View(serviceImage);
        }

        // GET: ServiceImages/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title");
            return View();
        }

        // POST: ServiceImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ServiceImage serviceImage, HttpPostedFileBase fileupload)
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

                    serviceImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                serviceImage.IsDelete=false;
				serviceImage.SubmitDate= DateTime.Now; 
                serviceImage.Id = Guid.NewGuid();
                db.ServiceImages.Add(serviceImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceImage.ServiceId);
            return View(serviceImage);
        }

        // GET: ServiceImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = db.ServiceImages.Find(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceImage.ServiceId);
            return View(serviceImage);
        }

        // POST: ServiceImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceImage serviceImage, HttpPostedFileBase fileupload)
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

                    serviceImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                serviceImage.IsDelete=false;
                db.Entry(serviceImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Title", serviceImage.ServiceId);
            return View(serviceImage);
        }

        // GET: ServiceImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = db.ServiceImages.Find(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }
            return View(serviceImage);
        }

        // POST: ServiceImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceImage serviceImage = db.ServiceImages.Find(id);
			serviceImage.IsDelete=true;
			serviceImage.DeleteDate=DateTime.Now;
 
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
    }
}

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
    public class GalleryImagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: GalleryImages
        public ActionResult Index()
        {
            var galleryImages = db.GalleryImages.Include(g => g.Gallery).Where(g=>g.IsDelete==false).OrderByDescending(g=>g.SubmitDate);
            return View(galleryImages.ToList());
        }

        // GET: GalleryImages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            return View(galleryImage);
        }

        // GET: GalleryImages/Create
        public ActionResult Create()
        {
            ViewBag.GalleryId = new SelectList(db.Galleries, "Id", "Title");
            return View();
        }

        // POST: GalleryImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryImage galleryImage, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/GalleryImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    galleryImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                galleryImage.IsDelete=false;
				galleryImage.SubmitDate= DateTime.Now; 
                galleryImage.Id = Guid.NewGuid();
                db.GalleryImages.Add(galleryImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GalleryId = new SelectList(db.Galleries, "Id", "Title", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.GalleryId = new SelectList(db.Galleries, "Id", "Title", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // POST: GalleryImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryImage galleryImage, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/GalleryImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    galleryImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                galleryImage.IsDelete=false;
                galleryImage.LastModificationDate=DateTime.Now;
                db.Entry(galleryImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryId = new SelectList(db.Galleries, "Id", "Title", galleryImage.GalleryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            return View(galleryImage);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GalleryImage galleryImage = db.GalleryImages.Find(id);
			galleryImage.IsDelete=true;
			galleryImage.DeleteDate=DateTime.Now;
 
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
        [Route("{language}/gallery/{id:Guid}")]
        public ActionResult List(string language,Guid id)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            GalleryImageListViewModel galleryImageListViewModel = new GalleryImageListViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                GalleryImages = db.GalleryImages.Where(current => current.IsDelete == false&&current.GalleryId==id).Include(current => current.Gallery).OrderByDescending(current => current.SubmitDate).ToList(),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider()
            };

            ViewBag.Title = db.Galleries.Find(id).TitleSrt;

            return View(galleryImageListViewModel);
        }

        [AllowAnonymous]
        [Route("gallery/{id:Guid}")]
        public ActionResult ListForRout(Guid id)
        {
                return RedirectToAction("List", "GalleryImages", new { language = "fa" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;
using Models.ViewModels;

namespace MobinGhateAsia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CatalogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Catalogs
        //[Route("Catalogs")]
        public ActionResult Index()
        {
            return View(db.Catalogs.Where(a => a.IsDelete == false).OrderByDescending(a => a.SubmitDate).ToList());
        }

       

        // GET: Catalogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog catalog, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Catalog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    catalog.File = newFilenameUrl;
                }
                #endregion
                catalog.IsDelete = false;
                catalog.SubmitDate = DateTime.Now;
                catalog.Id = Guid.NewGuid();
                db.Catalogs.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalog);
        }

        // GET: Catalogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalog catalog,HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Catalog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    catalog.File = newFilenameUrl;
                }
                #endregion
                catalog.IsDelete = false;
                db.Entry(catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Catalog catalog = db.Catalogs.Find(id);
            catalog.IsDelete = true;
            catalog.DeleteDate = DateTime.Now;

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

        [Route("{language}/catalog")]
        public ActionResult List(string language)
        {
           
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);
            CatalogListViewModel catalogListViewModel = new CatalogListViewModel()
            {
                Catalogs= GetCatalogListByCulture(),
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                Footer = menuData.ReturnFooterViewModel()
            };
            return View(catalogListViewModel);
        }

        [Route("catalog")]
        public ActionResult ListForRout()
        {
            return RedirectToAction("List", "Catalogs", new { language = "fa" });
        }
        public List<Catalog> GetCatalogListByCulture()
        {
            Helpers.GetCulture oGetCulture = new Helpers.GetCulture();

            string currentCulture = oGetCulture.CurrentLang();

            if (currentCulture.ToLower() == "en-us")
            {
                return db.Catalogs.Where(p => p.IsDelete == false && p.IsEn == true).ToList();
            }
            else
            {
                return db.Catalogs.Where(p => p.IsDelete == false && p.IsEn == false).ToList();
            }
        }
        public ActionResult DownloadFile(Guid id)
        {
            var catalog = db.Catalogs.Find(id);
            string contentType = "pdf";

            if (catalog.File != null)
                return File(catalog.File, contentType, "Catalogue.pdf");
            else
                return HttpNotFound();
        }
    }
}

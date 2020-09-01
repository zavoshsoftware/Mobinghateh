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
    [Authorize(Roles = "Administrator")]
    public class ProducImagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ProducImages
        public ActionResult Index(Guid id, Guid imageTypeId)
        {
            ViewBag.Title = ReturnTitle(id, imageTypeId);
            var producImages = db.ProducImages.Include(p => p.Product).Where(p => p.IsDelete == false && p.ProductId == id && p.ProductImageTypeId == imageTypeId).OrderByDescending(p => p.SubmitDate).Include(p => p.ProductImageType);
            return View(producImages.ToList());
        }

        public string ReturnTitle(Guid productId,Guid imageTypeId)
        {
            Product product = db.Products.Find(productId);

            ProductImageType productImageType = db.ProductImageTypes.Find(imageTypeId);

            return productImageType.Title + " محصول " + product.Title;
        }


        // GET: ProducImages/Create
        public ActionResult Create(Guid id, Guid imageTypeId)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductImageTypeId = imageTypeId;
            ViewBag.Title = ReturnTitle(id, imageTypeId);
            return View();
        }

        // POST: ProducImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProducImage producImage, Guid id, string imageTypeId, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Product/ProductImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    producImage.ImageUrl = newFilenameUrl;
                }
                #endregion

                producImage.ProductId = id;
                producImage.ProductImageTypeId = new Guid(imageTypeId);
                producImage.IsDelete = false;
                producImage.SubmitDate = DateTime.Now;
                producImage.Id = Guid.NewGuid();
                db.ProducImages.Add(producImage);
                db.SaveChanges();
                return RedirectToAction("Index",new{id=id, imageTypeId = imageTypeId });
            }

            ViewBag.ProductId = id;
            ViewBag.ProductImageTypeId = imageTypeId;
            ViewBag.Title = ReturnTitle(id, new Guid(imageTypeId));
            return View(producImage);
        }

        // GET: ProducImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProducImage producImage = db.ProducImages.Find(id);
            if (producImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = producImage.ProductId;
            ViewBag.ProductImageTypeId = producImage.ProductImageTypeId;
            ViewBag.Title = ReturnTitle(producImage.ProductId, producImage.ProductImageTypeId);
            return View(producImage);
        }

        // POST: ProducImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProducImage producImage, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                producImage.IsDelete = false;
                db.Entry(producImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = producImage.ProductId, imageTypeId = producImage.ProductImageTypeId });
            }
            ViewBag.ProductId = producImage.ProductId;
            ViewBag.ProductImageTypeId = producImage.ProductImageTypeId;
            ViewBag.Title = ReturnTitle(producImage.ProductId, producImage.ProductImageTypeId);
            return View(producImage);
        }

        // GET: ProducImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProducImage producImage = db.ProducImages.Find(id);
            if (producImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = producImage.ProductId;
            ViewBag.ProductImageTypeId = producImage.ProductImageTypeId;

            ViewBag.Title = ReturnTitle(producImage.ProductId, producImage.ProductImageTypeId);
            return View(producImage);
        }

        // POST: ProducImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProducImage producImage = db.ProducImages.Find(id);
            producImage.IsDelete = true;
            producImage.DeleteDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = producImage.ProductId, imageTypeId = producImage.ProductImageTypeId });
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

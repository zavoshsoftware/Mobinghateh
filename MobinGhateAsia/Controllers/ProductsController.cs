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
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductGroup).Where(p => p.IsDelete == false).OrderByDescending(p => p.SubmitDate);
            return View(products.ToList());
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(p => p.IsDelete == false&&p.ParentId==null).OrderByDescending(c=>c.SubmitDate), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase fileupload, HttpPostedFileBase fileUploadFlash)
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

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }


                string newFilenameUrl_Flash = string.Empty;
                if (fileUploadFlash != null)
                {
                    string filename = Path.GetFileName(fileUploadFlash.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl_Flash = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl_Flash);

                    fileUploadFlash.SaveAs(physicalFilename);

                    product.FlashImageUrl = newFilenameUrl_Flash;
                }
                #endregion
                product.IsDelete = false;
                product.SubmitDate = DateTime.Now;
                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(p => p.IsDelete == false && p.ParentId == null).OrderByDescending(c => c.SubmitDate), "Id", "Title", product.ProductGroupId);
            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(p => p.IsDelete == false && p.ParentId == null).OrderByDescending(c => c.SubmitDate), "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase fileupload, HttpPostedFileBase fileUploadFlash)
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

                    newFilenameUrl = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    product.ImageUrl = newFilenameUrl;
                }

                string newFilenameUrl_Flash = string.Empty;
                if (fileUploadFlash != null)
                {
                    string filename = Path.GetFileName(fileUploadFlash.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl_Flash = "/Uploads/Product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl_Flash);

                    fileUploadFlash.SaveAs(physicalFilename);

                    product.FlashImageUrl = newFilenameUrl_Flash;
                }
                #endregion
                product.LastModificationDate = DateTime.Now;

                product.IsDelete = false;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups.Where(p => p.IsDelete == false && p.ParentId == null).OrderByDescending(c => c.SubmitDate), "Id", "Title", product.ProductGroupId);
            return View(product);
        }




        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            product.IsDelete = true;
            product.DeleteDate = DateTime.Now;

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
        [Route("{language}/product/{id:Guid}")]
        public ActionResult List(string language, Guid id)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            ProductListViewModel productListViewModel = new ProductListViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                ProductGroup = db.ProductGroups.Find(id),
                Products = db.Products.Where(current => current.IsDelete == false && current.ProductGroupId == id).ToList(),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider(),
                Services = menuData.GetMenuServices(),
                FooterBlogs = menuData.GetFooterBlog()
        };

         
            return View(productListViewModel);
        }
        [AllowAnonymous]
        [Route("product/{id:Guid}")]
        public ActionResult ListfForRout(Guid id)
        {
            return RedirectToAction("List", "Products", new { language = "fa" });

        }
        [AllowAnonymous]
        [Route("{language}/product/{productGroupId:Guid}/{id:Guid}")]
        public ActionResult Details(string language, Guid productGroupId, Guid? id)
        {
            Guid productDetailType = new Guid("2be0a03b-4495-4fc2-bab3-be4d72e8ab1f");
            Guid productThechnicalType = new Guid("70d6da13-e72f-453c-a981-e5cc5283cfa2");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            ProductDetailViewModel productDetail = new ProductDetailViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                Product = ReturnProduct(id),
                RelatedProducts = ReturnRelatedProducts(id, productGroupId),
                DetailProductImages = ReturnProductImages(productDetailType, id),
                TechnicalProductImages = ReturnProductImages(productThechnicalType, id),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider(),
                Services = menuData.GetMenuServices(),
                FooterBlogs = menuData.GetFooterBlog()
            };

            if (language == "fa")
            {
                ViewBag.Title = product.Title + " | مبین قطعه آسیا";
                ViewBag.Description = product.MetaDescription;
            }
            else
            {
                ViewBag.Title = product.TitleEn + " | MobinGhateAsia";
                ViewBag.Description = product.MetaDescriptionEn;
            }

            return View(productDetail);
        }
        [AllowAnonymous]
        [Route("product/{productGroupId:Guid}/{id:Guid}")]
        public ActionResult DetailsforRout(Guid productGroupId, Guid? id)
        {
            return RedirectToAction("Details", "Products", new { language = "fa" });
        }

        [AllowAnonymous]
        public List<ProducImage> ReturnProductImages(Guid typeId, Guid? productId)
        {
            List<ProducImage> productImages = db.ProducImages
                .Where(current => current.ProductId == productId && current.ProductImageTypeId == typeId).ToList();

            return productImages;
        }

        [AllowAnonymous]
        public Product ReturnProduct(Guid? id)
        {
            Product product = db.Products.Where(current => current.Id == id).Include(current => current.ProductGroup)
                .FirstOrDefault();

            return product;
        }

        [AllowAnonymous]
        public List<Product> ReturnRelatedProducts(Guid? productId, Guid productGroupId)
        {
            List<Product> products = db.Products.Where(current =>
                current.ProductGroupId == productGroupId && current.Id != productId).Take(4).ToList();

            return products;
        }

    }
}

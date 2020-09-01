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
    public class ProductGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ProductGroups
        public ActionResult Index(Guid? id)
        {
            List<ProductGroup> productGroups = new List<ProductGroup>();
            if (id == null)
            {
                productGroups = db.ProductGroups.Where(a => a.IsDelete == false && a.ParentId == null).OrderBy(a => a.Priority)
                    .ToList();
                ViewBag.Title = "مدیریت گروه محصولات";
                ViewBag.hidden = "html-hidden";
            }
            else
            {
                productGroups = db.ProductGroups.Where(a => a.IsDelete == false && a.ParentId == id)
                    .OrderBy(a => a.Priority)
                    .ToList();
                ViewBag.Title = $"مدیریت زیر گروه محصولات {db.ProductGroups.Find(id)?.Title}";
                ViewBag.parentId = id;
                ViewBag.classItem = "html-hidden";
            }
            return View(productGroups);
        }

        // GET: ProductGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public ActionResult Create(Guid? id)
        {
            ViewBag.Title = id == null ? "افزودن گروه محصول" : $"افزودن زیر گروه به گروه {db.ProductGroups.Find(id)?.Title}";
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductGroup productGroup, Guid? id, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/ProductGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    productGroup.ImageUrl = newFilenameUrl;
                }


                #endregion
                productGroup.ParentId = id;
                productGroup.IsDelete = false;
                productGroup.SubmitDate = DateTime.Now;
                productGroup.Id = Guid.NewGuid();
                db.ProductGroups.Add(productGroup);
                db.SaveChanges();

                if (id == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", new { id = id });

            }

            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
        public ActionResult Edit(Guid? id, Guid? parentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentId = parentId;
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductGroup productGroup, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/ProductGroup/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    productGroup.ImageUrl = newFilenameUrl;
                }


                #endregion
                productGroup.IsDelete = false;
                productGroup.LastModificationDate = DateTime.Now;
                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();
                if (productGroup.ParentId == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", new { id = productGroup.ParentId });

            }
            ViewBag.parentId = productGroup.ParentId;
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentId = productGroup.ParentId;
            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            productGroup.IsDelete = true;
            productGroup.DeleteDate = DateTime.Now;

            if (db.ProductGroups.Any(current => current.ParentId == id && current.IsDelete == false))
            {
                List<ProductGroup> oProductGroups = db.ProductGroups
                    .Where(current => current.ParentId == id && current.IsDelete == false).ToList();
                foreach (ProductGroup item in oProductGroups)
                {
                    item.IsDelete = true;
                    item.DeleteDate = DateTime.Now;
                }
            }
            db.SaveChanges();
            if (productGroup.ParentId == null)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index", new { id = productGroup.ParentId });
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
        [Route("{language}/productgroup/{id:Guid}")]
        public ActionResult List(string language,Guid id)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            ProductGroupListViewModel productGroupListViewModel = new ProductGroupListViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                ProductGroups = ReturnChildProductGroups(id),
                CurrentProductGroup=db.ProductGroups.Find(id),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider()
            };

            if (language == "fa")
            {
                ViewBag.Title = productGroupListViewModel.CurrentProductGroup.Title 
                    ;
                ViewBag.Description = productGroupListViewModel.CurrentProductGroup.MetaDescription;

            }
            else
            {
                ViewBag.Title = productGroupListViewModel.CurrentProductGroup.TitleEn;
                ViewBag.Description = productGroupListViewModel.CurrentProductGroup.MetaDescriptionEn;
            }
            return View(productGroupListViewModel);
        }

        [AllowAnonymous]
        [Route("productgroup/{id:Guid}")]
        public ActionResult ListForRout(Guid id)
        {
                return RedirectToAction("List", "ProductGroups", new { language = "fa" });

        }

        [AllowAnonymous]
        public List<ProductGroup> ReturnChildProductGroups(Guid? parentId)
        {
            List<ProductGroup> productGroups= db.ProductGroups.Where(current => current.ParentId == parentId && current.IsDelete == false).ToList();

            return productGroups;
        }


        [AllowAnonymous]
        [Route("{language}/productgroup")]
        public ActionResult ListForParent(string language)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            ProductRootViewModel productGroupListViewModel = new ProductRootViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                ProductGroups = GetProductGroupForRoot(),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider(),
                PageTextItem = db.TextTypeItems.Find(new Guid("95532CE0-DF04-4E6A-B75E-EB4A92CFA5E9"))
            };


            return View(productGroupListViewModel);
        }

        public List<ProductGroupItem> GetProductGroupForRoot()
        {
            List<ProductGroup> productGroups = ReturnChildProductGroups(null);

            List<ProductGroupItem> productGroupItems=new List<ProductGroupItem>();

            foreach (ProductGroup productGroup in productGroups)
            {
                productGroupItems.Add(new ProductGroupItem()
                {
                    Id = productGroup.Id,
                    TitleSrt = productGroup.TitleSrt,
                    ImageUrl = productGroup.ImageUrl,
                    LinkUrl = GetLinkUrl(productGroup.Id)
                });
            }

            return productGroupItems;
        }

        public string GetLinkUrl(Guid productGroupId)
        {
            ProductGroup productGroup = db.ProductGroups.FirstOrDefault(current => current.ParentId == productGroupId);

            if (productGroup == null)
                return "product/" + productGroupId;
            else
                return "productgroup/" + productGroupId;

        }
    }
}

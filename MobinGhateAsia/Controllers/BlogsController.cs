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
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Blogs
        //[Route("Blogs")]
        public ActionResult Index()
        {
            var blogs = db.Blogs.Where(b => b.IsDelete == false).OrderByDescending(b => b.SubmitDate);
            return View(blogs.ToList());
        }



        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileupload)
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

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion

                blog.VisitNumber = 0;
                blog.IsDelete = false;
                blog.SubmitDate = DateTime.Now;
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileupload)
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

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDelete = false;
                blog.LastModificationDate = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
            blog.IsDelete = true;
            blog.DeleteDate = DateTime.Now;

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
        [Route("{language}/blog")]
        public ActionResult List(string language)
        {
            Helpers.MenuData menuData = new Helpers.MenuData();
            menuData.SetCurrentCulture(language);

            BlogListViewModel blogListViewModel = new BlogListViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                Blogs = db.Blogs.Where(current => current.IsDelete == false).OrderBy(current => current.Order).ThenByDescending(current => current.SubmitDate).ToList(),
                Footer = menuData.ReturnFooterViewModel(),
                InnerSlide = menuData.ReturnInnerSlider(),
                Services = menuData.GetMenuServices(),
                FooterBlogs = menuData.GetFooterBlog()

            };
            if (language == "fa")
            {
                ViewBag.Title ="مبین قطعه آسیا";
            }
            else
            {
                ViewBag.Title = "Mobin ghateh news";
            }

            return View(blogListViewModel);
        }

        [AllowAnonymous]
        [Route("blog")]
        public ActionResult ListForRout()
        {
            return RedirectToAction("List", "Blogs", new { language = "fa" });
        }
        [AllowAnonymous]
        [Route("{language}/blog/{id:Guid}")]
        public ActionResult Details(string language, Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            Helpers.MenuData menuData = new Helpers.MenuData();

            menuData.SetCurrentCulture(language);

            blog.VisitNumber = blog.VisitNumber + 1;
            db.SaveChanges();
            BlogDetailViewModel blogDetailViewModel = new BlogDetailViewModel()
            {
                MenuProductGroupViewModel = menuData.MenuProductGroup(),
                Blog = blog,
                Footer = menuData.ReturnFooterViewModel(),
                Services = menuData.GetMenuServices(),
                InnerSlide = menuData.ReturnInnerSlider(),
                FooterBlogs = menuData.GetFooterBlog()

            };

            if (language == "fa")
            {
                ViewBag.Title = "مبین قطعه آسیا";
                ViewBag.Description = blog.MetaDescription;
            }
            else
            {
                ViewBag.Title = blog.TitleEn + " | MobinGhateAsia group news";
                ViewBag.Description = blog.MetaDescriptionEn;
            }
            return View(blogDetailViewModel);
        }

        [AllowAnonymous]
        [Route("blog/{id:Guid}")]
        public ActionResult DetailsForRout(Guid? id)
        {
            return RedirectToAction("Details", "Blogs", new { language = "fa" });

        }
    }
}

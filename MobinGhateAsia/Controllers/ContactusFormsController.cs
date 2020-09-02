using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Text.RegularExpressions;

namespace MobinGhateAsia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ContactusFormsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ContactusForms
        public ActionResult Index()
        {
            return View(db.ContactusForms.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }


       
        // GET: ContactusForms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactusForm contactusForm = db.ContactusForms.Find(id);
            if (contactusForm == null)
            {
                return HttpNotFound();
            }
            return View(contactusForm);
        }

        // POST: ContactusForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fullname,email,message,IsDelete,SubmitDate,DeleteDate")] ContactusForm contactusForm)
        {
            if (ModelState.IsValid)
            {
				contactusForm.IsDelete=false;
                db.Entry(contactusForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactusForm);
        }

        // GET: ContactusForms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactusForm contactusForm = db.ContactusForms.Find(id);
            if (contactusForm == null)
            {
                return HttpNotFound();
            }
            return View(contactusForm);
        }

        // POST: ContactusForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContactusForm contactusForm = db.ContactusForms.Find(id);
			contactusForm.IsDelete=true;
			contactusForm.DeleteDate=DateTime.Now;
 
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
        [HttpPost]
        public ActionResult RequestPost(string fullname, string email, string message)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            
            if (!isEmail)
                return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
            else
            {
                ContactusForm cuf = new ContactusForm()
                {
                    Fullname = fullname,
                    Email = email,
                    Message = message,
                    Id=Guid.NewGuid(),
                    IsDelete=false,
                    SubmitDate=DateTime.Now
                };
                db.ContactusForms.Add(cuf);
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            
           
        }
    }
}

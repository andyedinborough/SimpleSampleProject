using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Razor.Models;

namespace Mvc3Razor.Controllers {
    public class HomeController : Controller {

        ContactsDbContext context = new ContactsDbContext();

        public ActionResult Index() {
            return View(context.Contacts);
        }

        public PartialViewResult GetDetails(string id)
        {
            return PartialView("Details", context.GetContact(id));
        }

        public PartialViewResult EditContact(string id)
        {
            return PartialView("Edit", context.GetContact(id));
        }

        [HttpPost]
        public ActionResult EditContact(ContactModel cm) {

            if (!TryUpdateModel(cm)) {
                ViewBag.updateError = "Update Failure";
                return PartialView("Edit", cm);
            }

            context.Entry(cm).State = System.Data.EntityState.Modified;
            context.SaveChanges();

            return PartialView("PartialContacts", context.Contacts);
        }

        public PartialViewResult MakeUpdate(ContactModel cm)
        {
            context.Entry(cm).State = System.Data.EntityState.Modified;
            context.SaveChanges();

            return PartialView("Index");
        }

        //public ActionResult Update(ContactModel cm)
        //{
        //    if (!TryUpdateModel(cm))
        //    {
        //        ViewBag.updateError = "Update Failure";
        //        return PartialView("Edit", cm);
        //    }

        //    context.Entry(cm).State = System.Data.EntityState.Modified;
        //    context.SaveChanges();

        //    return PartialView("Edit");
        //}


        public ViewResult Create() {
            return View(new ContactModel());
        }

        [HttpPost]
        public ActionResult Create(ContactModel cm)
        {
            if (ModelState.IsValid)
            {
                context.Contacts.Add(cm);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(cm);
        }

        public PartialViewResult Delete(string id) {
            return PartialView(context.GetContact(id));
        }

        [HttpPost]
        public RedirectToRouteResult Delete(string id, FormCollection collection) {
            context.Remove(id);
            return RedirectToAction("Index");
        }

        public string Cancel()
        {
            return "";
        }


    }
}

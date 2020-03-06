using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssueTracker.DAL;
using IssueTracker.Models;

namespace IssueTracker.Controllers
{
    public class IssController : Controller
    {
        private WitContext db = new WitContext();

        // GET: Iss
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var issues = from i in db.Issues
                            select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(i => i.IssName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    issues = issues.OrderByDescending(i => i.IssName);
                    break;
                case "Date":
                    issues = issues.OrderBy(i => i.CreationDate);
                    break;
                case "date_desc":
                    issues = issues.OrderByDescending(i => i.CreationDate);
                    break;
                default:
                    issues = issues.OrderBy(i => i.IssName);
                    break;
            }
            return View(issues.ToList());
        }

        // GET: Iss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueModel issueModel = db.Issues.Find(id);
            if (issueModel == null)
            {
                return HttpNotFound();
            }
            return View(issueModel);
        }

        // GET: Iss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Iss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjID,IssName,CreationDate,IssDescription")] IssueModel issueModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Issues.Add(issueModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException excep)
            {
                ModelState.AddModelError("", "Error: " + excep + " Unable to save changes.");
            }
            return View(issueModel);
        }

        // GET: Iss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueModel issueModel = db.Issues.Find(id);
            if (issueModel == null)
            {
                return HttpNotFound();
            }
            return View(issueModel);
        }

        // POST: Iss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var issueToUpdate = db.Issues.Find(id);
            if(TryUpdateModel(issueToUpdate,"",
                new string[] { "ProjID","IssName","CreationDate","IssDescription" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");                
                }
            }
            return View(issueToUpdate);
        }

        // GET: Iss/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault()) 
            {
                ViewBag.ErrorMessage = "Delete failed. Try Again.";
            }
            IssueModel issueModel = db.Issues.Find(id);
            if (issueModel == null)
            {
                return HttpNotFound();
            }
            return View(issueModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                IssueModel issue = db.Issues.Find(id);
                db.Issues.Remove(issue);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction
                    ("Delete", new { id = id, saveChangesError = true });
            }
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

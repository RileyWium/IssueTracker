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
using PagedList;
using System.Data.Entity.Infrastructure;

namespace IssueTracker.Controllers
{
    [Authorize]
    public class IssueModelController : Controller
    {
        private WitContext db = new WitContext();

        // GET: IssueModel
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            //var issues = db.Issues.Include(i => i.Projects);
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
                    issues = issues.OrderBy(i => i.ReportDate);
                    break;
                case "date_desc":
                    issues = issues.OrderByDescending(i => i.ReportDate);
                    break;
                default:
                    issues = issues.OrderBy(i => i.IssName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(issues.ToPagedList(pageNumber,pageSize));
        }

        // GET: IssueModel/Details/5
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

        // GET: IssueModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create()
        {
            //ViewBag.ProjID = new SelectList(db.Projects, "ID", "ProjName");
            return View();
        }

        // POST: IssueModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjID,IssName,ReportDate,IssDescription,IssStatus,IssPriority")] IssueModel issueModel)
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
            catch (RetryLimitExceededException excep)
            {
                ModelState.AddModelError("", "Error: " + excep + " Unable to save change.");
            }            
            return View(issueModel);
        }

        // GET: IssueModel/Edit/5
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

        // POST: IssueModel/Edit/5
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
            if (TryUpdateModel(issueToUpdate, "",
                new string[] { "ProjID", "IssName", "ReportDat", 
                    "IssDescription", "IssStatus", "IssPriority" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View(issueToUpdate);
        }

        // GET: IssueModel/Delete/5
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

        // POST: IssueModel/Delete/5
        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IssueModel issueModel = db.Issues.Find(id);
            db.Issues.Remove(issueModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
            catch (RetryLimitExceededException)
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

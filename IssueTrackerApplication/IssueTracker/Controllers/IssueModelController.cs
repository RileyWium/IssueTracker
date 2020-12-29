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
using IssueTracker.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IssueTracker.Controllers
{
    [Authorize]
    public class IssueModelController : Controller
    {
        private WitContext db = new WitContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int projID, int? issID)
        {
            
            ViewBag.ProjID = projID;
            ViewBag.IssueID = issID;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                                    
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IQueryable<IssueModel> issues;
          
            issues = from i in db.Issues
                        where i.ProjID == projID
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
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(issues.ToPagedList(pageNumber,pageSize));
        }

        // GET: IssueModel/Details/5
        public ActionResult Details(int id, int projID)
        {
            ViewBag.ProjectID = projID;
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
        [ActionName("Create")]
        public ActionResult Create_Get(int projID)
        {
            IssueCreationViewModel icViewModel = new IssueCreationViewModel
            {
                ProjectID = projID,
                ReporterMainName = UserManager.FindByName(User.Identity.Name).MainName,
                CurrentDate = DateTime.Now
            };
            PopulateUserList(projID, icViewModel);

            return View(icViewModel);
        }        
        // POST: IssueModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Post(IssueCreationViewModel issueModel, int projID)
        {           
            issueModel.Issue.Project = db.Projects.Find(projID);
                    
            try
            {
                if (ModelState.IsValid)
                {                    
                    db.Issues.Add(issueModel.Issue);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { projID });
                }
            }
            catch (RetryLimitExceededException excep)
            {
                ModelState.AddModelError("", "Error: " + excep + " Unable to save change.");
            }
            return View(issueModel);
        }
        private void PopulateUserList(int projID, IssueCreationViewModel icViewModel)
        {
            var AssigneeListQuery = from i in db.IdenProjs
                                    where i.ProjID == projID
                                    orderby i.MainName
                                    select new SelectListItem
                                    {
                                        Text = i.MainName,
                                        Value = i.MainName
                                    };
           
            icViewModel.AssigneeUsers = AssigneeListQuery.AsEnumerable();
        }
        // GET: IssueModel/Edit/5
        public ActionResult Edit(int? id, int projID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueCreationViewModel icViewModel = new IssueCreationViewModel
            {
                ProjectID = projID,
                ReporterMainName = UserManager.FindByName(User.Identity.Name).MainName,
                CurrentDate = DateTime.Now
            };
            PopulateUserList(projID, icViewModel);
            icViewModel.Issue = db.Issues.Find(id);
            if (icViewModel.Issue == null)
            {
                return HttpNotFound();
            }
           
            return View(icViewModel);
        }

        // POST: IssueModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Post(int? id, IssueCreationViewModel issueModel, int projID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!ModelState.IsValid)
            {
                return View(issueModel);
            }
            var issueToUpdate = db.Issues.Find(id);
            issueToUpdate.IssName = issueModel.Issue.IssName;
            issueToUpdate.ReportDate = issueModel.Issue.ReportDate;
            issueToUpdate.DueDate = issueModel.Issue.DueDate;
            issueToUpdate.IssDescription = issueModel.Issue.IssDescription;
            issueToUpdate.IssAssigneeName = issueModel.Issue.IssAssigneeName;
            issueToUpdate.IssReporterName = issueModel.Issue.IssReporterName;
            issueToUpdate.IssStatus = issueModel.Issue.IssStatus;
            issueToUpdate.IssPriority = issueModel.Issue.IssPriority;

            try
            {
                db.Entry(issueToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { projID });
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }            
            return View(issueToUpdate);
        }

        // GET: IssueModel/Delete/5
        public ActionResult Delete(int projID, int? id, bool? saveChangesError = false)
        {
            ViewBag.ProjID = projID;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try Again.";
                return View();
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
        public ActionResult Delete(int? id, int projID)
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
                    ("Delete", new { id, saveChangesError = true, projID });
            }
            return RedirectToAction("Index", new { projID });
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

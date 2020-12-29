using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssueTracker.DAL;
using IssueTracker.Models;
using IssueTracker.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PagedList;

namespace IssueTracker.Controllers
{
    [Authorize]
    public class ProjectModelController : Controller
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

        // GET: ProjectModel
        public ActionResult Index(string currentFilter, string searchString, int? page)//might not need proj id, already got user id
        {            
            IEnumerable<ProjectModel> joinedModel = null;
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (User.IsInRole("Admin"))
            {
                joinedModel = (from p in db.Projects
                              select p).ToList();
                ViewBag.masterCnt = joinedModel.Count();
            }
            else
            {
                joinedModel =
                    (from p in db.Projects
                     join ip in db.IdenProjs
                     on p.ProjectID equals ip.ProjID
                     where ip.UserID.Contains(User.Identity.Name)
                     orderby ip.Master descending, p.ProjName ascending //how to get master count
                     select p).ToList();//.count

                ViewBag.masterCnt = (from p in db.Projects
                                 join ip in db.IdenProjs
                                 on p.ProjectID equals ip.ProjID
                                 where ip.UserID.Contains(User.Identity.Name) && ip.Master == true
                                 select p).Count();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                joinedModel = joinedModel.Where(i => i.ProjName.Contains(searchString));

                ViewBag.masterCnt = (from p in db.Projects
                                     join ip in db.IdenProjs
                                     on p.ProjectID equals ip.ProjID
                                     where ip.UserID.Contains(User.Identity.Name) && ip.Master == true && p.ProjName.Contains(searchString)
                                     select p).Count();
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            
            ViewBag.pageSize = pageSize;
            ViewBag.pageNum = pageNumber;

            return View(joinedModel.ToPagedList(pageNumber, pageSize));
        }        

        // GET: ProjectModel/Create        
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjName")]ProjectModel projectModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdenProjModel ipModel = new IdenProjModel();
                    ipModel.IdenProjID = db.IdenProjs.Count() + 1;
                    ipModel.ProjID = projectModel.ProjectID;
                    ipModel.Master = true;
                    ipModel.UserID = User.Identity.Name;
                    ipModel.Project = projectModel;//itterates ProjID count
                    ipModel.MainName = UserManager.FindByName(User.Identity.Name).MainName;
                    db.IdenProjs.Add(ipModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");
            }
            return View(projectModel);
        }
        
        // GET: ProjectModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectModel projectModel = db.Projects.Find(id);
            if (projectModel == null)
            {
                return HttpNotFound();
            }
            return View(projectModel);
        }

        // POST: ProjectModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectToUpdate = db.Projects.Find(id);
            if(TryUpdateModel(projectToUpdate, "",
                new string[] { "ProjName" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            return View(projectToUpdate);
        }

        public ActionResult Invite(int? id)
        {
            ViewBag.ProjName = db.Projects.Find(id).ProjName;
            ViewBag.JoinUrl = Url.Action("Join", "ProjectModel", new { joinID = id }, Request.Url.Scheme).ToString();
            return View();
        }

        //only reachable via using invite url through addressbar 
        public ActionResult Join(int? joinID)
        {
            if (joinID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectModel projectModel = db.Projects.Find(joinID);
            if (projectModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjName = projectModel.ProjName;
            ViewBag.joinID = joinID;
            ViewBag.UserName = UserManager.FindByName(User.Identity.Name).MainName;
            return View(projectModel);
        }

        [HttpPost, ActionName("Join")]
        [ValidateAntiForgeryToken]
        public ActionResult Join_Post(int? joinID)
        {
            //creates a IdenProj entry for given project id and current user if it doesn't already exist
            if (joinID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //edge case: make a join user a master if they are joining a project with 0 users
            foreach (IdenProjModel ip in db.IdenProjs)
            {
                if (ip.ProjID == joinID && ip.UserID == User.Identity.Name)
                {
                    return RedirectToAction("Index","Home", new { });
                }
            }
            
            IdenProjModel ipModel = new IdenProjModel();
            ipModel.IdenProjID = db.IdenProjs.Count()+1;
            ipModel.ProjID = (int) joinID;
            ipModel.Master = false;
            ipModel.UserID = User.Identity.Name;
            ipModel.Project = db.Projects.Find(joinID);//might itterate db ProjID count
            ipModel.MainName = UserManager.FindByName(User.Identity.Name).MainName;
            db.IdenProjs.Add(ipModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Leave(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectModel projectModel = db.Projects.Find(id);
            if (projectModel == null)
            {
                return HttpNotFound();
            }
            return View(projectModel);
        }

        [HttpPost,ActionName("Leave")]
        [ValidateAntiForgeryToken]
        public ActionResult Leave_Post(int? id)
        {
            IdenProjModel toLeave = null;
            foreach (IdenProjModel ip in db.IdenProjs)
            {
                if (ip.ProjID == id && ip.UserID == User.Identity.Name)
                {
                    toLeave = ip;
                }
            }
            try
            {
                if(toLeave != null)
                {
                    db.IdenProjs.Remove(toLeave);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction
                    ("Leave", new { id, saveChangesError = true });
            }            
            return View();
        }

        // GET: ProjectModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectModel projectModel = db.Projects.Find(id);
            if (projectModel == null)
            {
                return HttpNotFound();
            }
            return View(projectModel);
        }

        // POST: ProjectModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ProjectModel projectModel = db.Projects.Find(id);
            db.Projects.Remove(projectModel);
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
    }
}

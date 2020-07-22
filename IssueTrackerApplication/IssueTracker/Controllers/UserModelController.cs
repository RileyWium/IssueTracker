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
using IssueTracker.ViewModels;
using System.Data.Entity.Infrastructure;

namespace IssueTracker.Controllers
{
    public class UserModelController : Controller
    {
        private WitContext db = new WitContext();

        // GET: UserModel
        public ActionResult Index(int? id, int? projID)
        {
            var viewModel = new UserIndexData();
            viewModel.Users = db.Users
                .Include(u => u.Projects)
                .OrderBy(u => u.MainName);

            if(id != null)
            {
                ViewBag.UserID = id.Value;
                viewModel.Projects = viewModel.Users.Where(
                    u => u.ID == id.Value).Single().Projects;
            }

            if(projID != null)
            {
                ViewBag.ProjectID = projID.Value;
                viewModel.Issues = viewModel.Projects.Where(
                    p => p.ProjectID == projID.Value).Single().Issues;
            }
            return View(viewModel);

        }

        // GET: UserModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // GET: UserModel/Create
        public ActionResult Create()
        {
            var user = new UserModel();
            user.Projects = new List<ProjectModel>();
            PopulateAssignedProjectData(user);
            return View();
        }

        // POST: UserModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName")] UserModel userModel, string[] selectedProjects)
        {
            if (selectedProjects != null)
            {
                userModel.Projects = new List<ProjectModel>();
                foreach(var project in selectedProjects)
                {
                    var projectToAdd = db.Projects.Find(int.Parse(project));
                    userModel.Projects.Add(projectToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedProjectData(userModel);
            return View(userModel);            
        }

        // GET: UserModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UserModel userModel = db.Users.Find(id);
            UserModel userModel = db.Users
                .Include(u => u.Projects)
                .Where(u => u.ID == id)
                .Single();
            PopulateAssignedProjectData(userModel);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        private void PopulateAssignedProjectData(UserModel userModel)
        {
            var allProjects = db.Projects;
            var userProjects = new HashSet<int>(userModel.Projects.Select(p => p.ProjectID));
            var viewModel = new List<AssignedProjectData>();
            foreach (var project in allProjects)
            {
                viewModel.Add(new AssignedProjectData
                {
                    ProjectID = project.ProjectID,
                    Name = project.ProjName,
                    Assigned = userProjects.Contains(project.ProjectID)
                });
            }
            ViewBag.Projects = viewModel;
        }

        // POST: UserModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedProjects)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.Users
                .Include(u => u.Projects)
                .Where(u => u.ID == id)
                .Single();

            if (TryUpdateModel(userToUpdate, "",
                new string[] { "UserName" }))
            {
                try
                {
                    UpdateUserProjects(selectedProjects, userToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }                
            }
            PopulateAssignedProjectData(userToUpdate);
            return View(userToUpdate);
        }

        private void UpdateUserProjects(string[] selectedProjects, UserModel userToUpdate)
        {
            if(selectedProjects == null)
            {
                userToUpdate.Projects = new List<ProjectModel>();
                return;
            }

            var selectedProjectsHS = new HashSet<string>(selectedProjects);
            var userProjects = new HashSet<int>(userToUpdate.Projects.Select(p => p.ProjectID));
            foreach (var project in db.Projects)
            {
                if (selectedProjectsHS.Contains(project.ProjectID.ToString()))
                {
                    if (!userProjects.Contains(project.ProjectID))
                    {
                        userToUpdate.Projects.Add(project);
                    }
                }
                else
                {
                    if (userProjects.Contains(project.ProjectID))
                    {
                        userToUpdate.Projects.Remove(project);
                    }
                }
            }
        }

        // GET: UserModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: UserModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.Users.Find(id);
            db.Users.Remove(userModel);
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

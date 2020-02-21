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
        public ActionResult Index()
        {
            return View(db.Issues.ToList());
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
        public ActionResult Create([Bind(Include = "ID,ProjID,IssName,CreationDate,IssDescription")] IssueModel issueModel)
        {
            if (ModelState.IsValid)
            {
                db.Issues.Add(issueModel);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjID,IssName,CreationDate,IssDescription")] IssueModel issueModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issueModel);
        }

        // GET: Iss/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Iss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IssueModel issueModel = db.Issues.Find(id);
            db.Issues.Remove(issueModel);
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

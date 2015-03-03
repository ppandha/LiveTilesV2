using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiveTiles.Models;

namespace WebApplication3.Controllers
{
    public class NewsfeedsController : Controller
    {
        private NoticeboardDBContext db = new NoticeboardDBContext();

        // GET: Newsfeeds
        public ActionResult Index()
        {
            return View(db.Newsfeeds.ToList());
        }

        // GET: Newsfeeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            if (newsfeed == null)
            {
                return HttpNotFound();
            }
            return View(newsfeed);
        }

        // GET: Newsfeeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Newsfeeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,RssUrl")] Newsfeed newsfeed)
        {
            if (ModelState.IsValid)
            {
                db.Newsfeeds.Add(newsfeed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsfeed);
        }

        // GET: Newsfeeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            if (newsfeed == null)
            {
                return HttpNotFound();
            }
            return View(newsfeed);
        }

        // POST: Newsfeeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,RssUrl")] Newsfeed newsfeed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsfeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsfeed);
        }

        // GET: Newsfeeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            if (newsfeed == null)
            {
                return HttpNotFound();
            }
            return View(newsfeed);
        }

        // POST: Newsfeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            db.Newsfeeds.Remove(newsfeed);
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

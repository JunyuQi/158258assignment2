using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class QuTypesController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: QuTypes
        public ActionResult Index()
        {
            return View(db.QuTypes.OrderBy(c => c.Name).ToList());
        }

        // GET: QuTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuType quType = db.QuTypes.Find(id);
            if (quType == null)
            {
                return HttpNotFound();
            }
            return View(quType);
        }

        // GET: QuTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] QuType quType)
        {
            if (ModelState.IsValid)
            {
                db.QuTypes.Add(quType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quType);
        }

        // GET: QuTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuType quType = db.QuTypes.Find(id);
            if (quType == null)
            {
                return HttpNotFound();
            }
            return View(quType);
        }

        // POST: QuTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] QuType quType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quType);
        }

        // GET: QuTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuType quType = db.QuTypes.Find(id);
            if (quType == null)
            {
                return HttpNotFound();
            }
            return View(quType);
        }

        // POST: QuTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuType quType = db.QuTypes.Find(id);
            db.QuTypes.Remove(quType);
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

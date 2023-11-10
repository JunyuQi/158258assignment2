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
using static System.Net.WebRequestMethods;

namespace WebApplication1.Controllers
{
    public class QuestionsController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Questions
        public ActionResult Index(string quType, string search)
        {
            var questions = db.Questions.Include(q => q.QuType);
            if (!String.IsNullOrEmpty(quType))
            {
                questions = questions.Where(p => p.QuType.Name == quType);
            }
            if (!String.IsNullOrEmpty(search))
            {
                questions = questions.Where(p => p.Name.Contains(search) ||
                p.Description.Contains(search) ||
                p.QuType.Name.Contains(search));
            }

            if (!String.IsNullOrEmpty(search))
            {
                questions = questions.Where(p => p.Name.Contains(search) ||
                p.Description.Contains(search) ||
                p.QuType.Name.Contains(search));
                ViewBag.Search = search;
            }
            var quTypes = questions.OrderBy(p => p.QuType.Name).Select(p => p.QuType.Name).Distinct();
            if (!String.IsNullOrEmpty(quType))
            {
                questions = questions.Where(p => p.QuType.Name == quType);
            }
            ViewBag.QuType = new SelectList(quTypes);


            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.QuTypeID = new SelectList(db.QuTypes, "ID", "Name");
            return View();
        }

        // POST: Questions/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,QuTypeID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuTypeID = new SelectList(db.QuTypes, "ID", "Name", question.QuTypeID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuTypeID = new SelectList(db.QuTypes, "ID", "Name", question.QuTypeID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,QuTypeID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuTypeID = new SelectList(db.QuTypes, "ID", "Name", question.QuTypeID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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

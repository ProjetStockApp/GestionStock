using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;

namespace StockApp.Controllers
{
    public class ArticlesController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Articles/
        public ActionResult Index()
        {
            var tb_articles = db.TB_articles.Include(t => t.TB_categorie);
            return View(tb_articles.ToList());
        }

        // GET: /Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            return View(tb_articles);
        }

        // GET: /Articles/Create
        public ActionResult Create()
        {
            ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie");
            return View();
        }

        // POST: /Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_articles,Nom_articles,Qte_alerte,Description,Id_categorie,DateCreer,CreerPar")] TB_articles tb_articles)
        {
            if (ModelState.IsValid)
            {
                db.TB_articles.Add(tb_articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie", tb_articles.Id_categorie);
            return View(tb_articles);
        }

        // GET: /Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie", tb_articles.Id_categorie);
            return View(tb_articles);
        }

        // POST: /Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_articles,Nom_articles,Qte_alerte,Description,Id_categorie,DateCreer,CreerPar")] TB_articles tb_articles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie", tb_articles.Id_categorie);
            return View(tb_articles);
        }

        // GET: /Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            return View(tb_articles);
        }

        // POST: /Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_articles tb_articles = db.TB_articles.Find(id);
            db.TB_articles.Remove(tb_articles);
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

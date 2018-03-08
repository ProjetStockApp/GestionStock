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
    public class CategorieController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Categorie/
        public ActionResult Index()
        {
            return View(db.TB_categorie.ToList());
        }

        // GET: /Categorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // GET: /Categorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Categorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_categorie,Nom_categorie,Description,DateCreer,CreerPar")] TB_categorie tb_categorie)
        {
            if (ModelState.IsValid)
            {
                db.TB_categorie.Add(tb_categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_categorie);
        }

        // GET: /Categorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // POST: /Categorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_categorie,Nom_categorie,Description,DateCreer,CreerPar")] TB_categorie tb_categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_categorie);
        }

        // GET: /Categorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // POST: /Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            db.TB_categorie.Remove(tb_categorie);
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

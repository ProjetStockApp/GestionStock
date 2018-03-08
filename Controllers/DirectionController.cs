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
    public class DirectionController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Direction/
        public ActionResult Index()
        {
            return View(db.TB_direction.ToList());
        }

        // GET: /Direction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // GET: /Direction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Direction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_direction,Nom_direction,Description,DateCreer,CreerPar")] TB_direction tb_direction)
        {
            if (ModelState.IsValid)
            {
                db.TB_direction.Add(tb_direction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_direction);
        }

        // GET: /Direction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // POST: /Direction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_direction,Nom_direction,Description,DateCreer,CreerPar")] TB_direction tb_direction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_direction);
        }

        // GET: /Direction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // POST: /Direction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_direction tb_direction = db.TB_direction.Find(id);
            db.TB_direction.Remove(tb_direction);
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

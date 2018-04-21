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
    public class AspNetUsersController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /AspNetUsers/
        public ActionResult Index()
        {
            var aspnetusers = db.AspNetUsers.Include(a => a.TB_direction);
            return View(aspnetusers.ToList());
        }

        // GET: /AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuser);
        }

        // GET: /AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction");
            return View();
        }

        // POST: /AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,NomComplet,Id_direction,PasswordHash,SecurityStamp,Discriminator,CreerPar,DateCreer")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspnetuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", aspnetuser.Id_direction);
            return View(aspnetuser);
        }

        // GET: /AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", aspnetuser.Id_direction);
            return View(aspnetuser);
        }

        // POST: /AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,NomComplet,Id_direction,PasswordHash,SecurityStamp,Discriminator,CreerPar,DateCreer")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", aspnetuser.Id_direction);
            return View(aspnetuser);
        }

        // GET: /AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuser);
        }

        // POST: /AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspnetuser);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;
using Microsoft.AspNet.Identity;

namespace StockApp.Controllers
{
    public class DetailRequisitionController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /DetailRequisition/
        public ActionResult Index(int? id)
        {

            var tb_detail_requisition = db.TB_detail_requisition.Include(t => t.TB_articles).Include(t => t.TB_bonRequisition);
            tb_detail_requisition = tb_detail_requisition.Where(t => id == t.Id_bon_requisition);
            return View(tb_detail_requisition.ToList());
        }

        //============ IndecDetail===============
        public ActionResult IndexDetail(int? id)
        {
            var UserID = User.Identity.GetUserId();

            var userNom = from u in db.AspNetUsers
                          join req in db.TB_bonRequisition
                          on u.Id equals req.UserId
                          where req.Id_bon_requisition == id
                          select u.NomComplet;

            var nomEmploye = userNom.First();
            string Nomuser = Convert.ToString(nomEmploye);
            ViewBag.nomEmp = Nomuser;
            ViewBag.code = id;


            var tb_detail_requisition = db.TB_detail_requisition.Include(t => t.TB_articles).Include(t => t.TB_bonRequisition);
            tb_detail_requisition = tb_detail_requisition.Where(t => id == t.Id_bon_requisition);
            return View(tb_detail_requisition.ToList());
        }


        // =========== UpdateValider ==============
        public ActionResult UpdateValider(int? id)
        {
            var UserID = User.Identity.GetUserId();
            //var userRoles = db.AspNetRoles.Include(r => r.AspNetUsers).ToList();


            var userNom = from u in db.AspNetUsers
                          where u.Id == UserID
                          select u.NomComplet ;

            var nomDirect = userNom.First();
            // var Nomdirect = Convert.ToString(nomDirect);

            var requisition = from c in db.TB_bonRequisition where c.Id_bon_requisition == id select c;

            var req = requisition.FirstOrDefault();

            //req.IsSoumettre = "OUI";
            req.Validate = "1";
            req.Date_validation = DateTime.Now;
            req.Directeur = nomDirect;

            db.SaveChanges();

            //var Soumetre = from u in db.TB_bonRequisition
            //                  where u.Id_bon_requisition == id
            //                  select u.IsSoumettre;
            //    ViewBag.soum = Soumetre;
            return RedirectToAction("Index");
            //soumettre.
        }


        // GET: /DetailRequisition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_detail_requisition tb_detail_requisition = db.TB_detail_requisition.Find(id);
            if (tb_detail_requisition == null)
            {
                return HttpNotFound();
            }
            return View(tb_detail_requisition);
        }

        // GET: /DetailRequisition/Create
        public ActionResult Create()
        {
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles");
            ViewBag.Id_bon_requisition = new SelectList(db.TB_bonRequisition, "Id_bon_requisition", "Description");
            return View();
        }

        // POST: /DetailRequisition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_details_requisition,Id_bon_requisition,Id_articles,Quantite,Date_requisition")] TB_detail_requisition tb_detail_requisition)
        {
            if (ModelState.IsValid)
            {
                db.TB_detail_requisition.Add(tb_detail_requisition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_detail_requisition.Id_articles);
            ViewBag.Id_bon_requisition = new SelectList(db.TB_bonRequisition, "Id_bon_requisition", "Description", tb_detail_requisition.Id_bon_requisition);
            return View(tb_detail_requisition);
        }

        // GET: /DetailRequisition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_detail_requisition tb_detail_requisition = db.TB_detail_requisition.Find(id);
            if (tb_detail_requisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_detail_requisition.Id_articles);
            ViewBag.Id_bon_requisition = new SelectList(db.TB_bonRequisition, "Id_bon_requisition", "Description", tb_detail_requisition.Id_bon_requisition);
            return View(tb_detail_requisition);
        }

        // POST: /DetailRequisition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_details_requisition,Id_bon_requisition,Id_articles,Quantite,Date_requisition")] TB_detail_requisition tb_detail_requisition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_detail_requisition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_detail_requisition.Id_articles);
            ViewBag.Id_bon_requisition = new SelectList(db.TB_bonRequisition, "Id_bon_requisition", "Description", tb_detail_requisition.Id_bon_requisition);
            return View(tb_detail_requisition);
        }

        // GET: /DetailRequisition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_detail_requisition tb_detail_requisition = db.TB_detail_requisition.Find(id);
            if (tb_detail_requisition == null)
            {
                return HttpNotFound();
            }
            return View(tb_detail_requisition);
        }

        // POST: /DetailRequisition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_detail_requisition tb_detail_requisition = db.TB_detail_requisition.Find(id);
            db.TB_detail_requisition.Remove(tb_detail_requisition);
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

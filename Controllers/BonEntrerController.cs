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
    public class BonEntrerController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /BonEntrer/
        public ActionResult Index()
        {
            var tb_bonentre = db.TB_bonEntre.Include(t => t.TB_livraison);
            return View(tb_bonentre.ToList());
        }

        // GET: /BonEntrer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            if (tb_bonentre == null)
            {
                return HttpNotFound();
            }
            return View(tb_bonentre);
        }

        // GET: /BonEntrer/Create
        public ActionResult Create()
        {

            PopulateLivraisonDropDownList();

            // PopulateCategorieDropDownList1();

            ViewBag.Livraison = db.TB_livraison.ToList();
            ViewBag.Categorie = db.TB_categorie.ToList();
            ViewBag.Articles = new SelectList(db.TB_articles
                            .Where(c => c.Id_articles == 0), "Id_articles", "Nom_articles").ToList();

            var UserID = User.Identity.GetUserId();
            var userNom = from u in db.AspNetUsers
                          where u.Id == UserID
                          select u.NomComplet.First();

            //var Nom = userNom.First();



            ViewBag.nom = userNom;

            //ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche");
            return View();
        }

        //==================== Tous les Methode =========================\\

        private void PopulateLivraisonDropDownList(object selectedlivraison = null)
        {
            var LivraisonQuery = from d in db.TB_livraison orderby d.Code_fiche select d;
            ViewBag.Id_livraison = new SelectList(LivraisonQuery, "Id_livraison", "Code_fiche", selectedlivraison);
        }

        private void PopulateCategorieDropDownList1(object selectedlivraison = null)
        {
            var LivraisonQuery = from d in db.TB_categorie orderby d.Nom_categorie select d;
            ViewBag.Id_categorie = new SelectList(LivraisonQuery, "Id_categorie", "Nom_categorie", selectedlivraison);
        }


        // ============== Load article in dropdown list ====================

        public JsonResult LoadArticles(string CategorieID, string TempData)
        {
            if (HttpContext.Request.IsAjaxRequest() && CategorieID != "")
            {
                int CategID = Convert.ToInt32(CategorieID);
                var StatesData = db.TB_articles
                                 .Where(c => c.Id_categorie == CategID).ToList()
                                 .Select(m => new SelectListItem()
                                 {
                                     Text = m.Nom_articles,
                                     Value = m.Id_articles.ToString()
                                 });
                return Json(StatesData, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        // ==================== Load categorie in dropdown list ===================== 

        public JsonResult Loadcategorie()
        {
            //if (HttpContext.Request.IsAjaxRequest() && categID != "")
            // {
            // int CatID = Convert.ToInt32(categID);
            // CatID=0;
            var CategorieData = db.TB_categorie
                //.Where(c => c.Id_categorie != CatID && c.Id_categorie > 0).ToList()
                             .Select(m => new SelectListItem()
                             {
                                 Text = m.Nom_categorie,
                                 Value = m.Id_categorie.ToString()
                             });
            return Json(CategorieData, JsonRequestBehavior.AllowGet);
            //}
            //return null;
        }

        // save data in table BonEntrer

        public JsonResult Save(TB_bonEntre tb_bonentre)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.TB_bonEntre.Add(tb_bonentre);

                    db.SaveChanges();

                    return Json(new { Success = 1, BonentrerID = tb_bonentre.Id_bon_entrestock, ex = "" });

                }
            }
            catch (Exception ex)
            {

                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("les informations ne sont enregistrer").Message.ToString() });

        }


        //=====================fin de tous les methodes=====================\\

        // POST: /BonEntrer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_bon_entrestock,Date_entre,Description,Id_livraison,DateCreer,CreerPar")] TB_bonEntre tb_bonentre)
        {
            if (ModelState.IsValid)
            {
                db.TB_bonEntre.Add(tb_bonentre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            return View(tb_bonentre);
        }

        // GET: /BonEntrer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            if (tb_bonentre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            return View(tb_bonentre);
        }

        // POST: /BonEntrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_bon_entrestock,Date_entre,Description,Id_livraison,DateCreer,CreerPar")] TB_bonEntre tb_bonentre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_bonentre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            return View(tb_bonentre);
        }

        // GET: /BonEntrer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            if (tb_bonentre == null)
            {
                return HttpNotFound();
            }
            return View(tb_bonentre);
        }

        // POST: /BonEntrer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            db.TB_bonEntre.Remove(tb_bonentre);
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

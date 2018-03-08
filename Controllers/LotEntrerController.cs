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
    public class LotEntrerController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /LotEntrer/
        public ActionResult Index(int? id)
        {
            //var tb_lot_entrestock = db.TB_lot_entrestock.Include(t => t.TB_articles).Include(t => t.TB_bonEntre);
            //return View(tb_lot_entrestock.ToList());

            var tb_lot_entrestock = db.TB_lot_entrestock.Include(t => t.TB_articles).Include(t => t.TB_bonEntre);
            tb_lot_entrestock = tb_lot_entrestock.Where(t => id == t.Id_bon_entrestock);
            return View(tb_lot_entrestock.ToList());

        }

        // GET: /LotEntrer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = from art in db.TB_lot_entrestock
                          where (art.Id_lot_entrestock == id)
                          select art.Id_articles;

            var id_art = article.First();


            var query =
                    from qte in db.TB_lot_entrestock
                    where qte.Id_articles == id_art
                    select qte.Quantite;

            ViewBag.sumQte = query.Sum();

            var alerte = from alt in db.TB_articles
                         where (alt.Id_articles == id_art)
                         select alt.Qte_alerte;
            var QteAlerte = alerte.First();

            ViewBag._QteAlerte = QteAlerte;
            
            
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            return View(tb_lot_entrestock);
        }

        // GET: /LotEntrer/Create
        public ActionResult Create()
        {
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles");
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description");
            return View();
        }

        // POST: /LotEntrer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_lot_entrestock,Date_entrer,Id_articles,Id_bon_entrestock,Quantite,Total")] TB_lot_entrestock tb_lot_entrestock)
        {
            if (ModelState.IsValid)
            {
                db.TB_lot_entrestock.Add(tb_lot_entrestock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // GET: /LotEntrer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // POST: /LotEntrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_lot_entrestock,Date_entrer,Id_articles,Id_bon_entrestock,Quantite,Total")] TB_lot_entrestock tb_lot_entrestock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_lot_entrestock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // GET: /LotEntrer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            return View(tb_lot_entrestock);
        }

        // POST: /LotEntrer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            db.TB_lot_entrestock.Remove(tb_lot_entrestock);
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

        // ============== tous les Methodes suplementaire =============== \\

        //private void populateArticle(object selectedArticle = null)
        //{
        //    var data = from a in db.TB_articles
        //               join c in db.TB_categorie
        //               on a.Id_categorie equals c.Id_categorie
        //               select new
        //               {
        //                   Id_article = a.Id_articles,
        //                   Nom_article = a.Nom_articles,
        //                   Nom_categorie = c.Nom_categorie
        //               };

        // ViewBag.Id_articles = new SelectList(data, "Id_article", "Nom_article", "Nom_categorie", selectedArticle);


        //}

        public JsonResult LoadStatesByCountryID(string CountryID, string TempData)
        {
            if (HttpContext.Request.IsAjaxRequest() && CountryID != "")
            {
                int CntryID = Convert.ToInt32(CountryID);
                var StatesData = db.TB_articles
                                 .Where(c => c.Id_categorie == CntryID).ToList()
                                 .Select(m => new SelectListItem()
                                 {
                                     Text = m.Nom_articles,
                                     Value = m.Id_articles.ToString()
                                 });
                return Json(StatesData, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        

        //================ fin des  methodes =====================\\
    }
}

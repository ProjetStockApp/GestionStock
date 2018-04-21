using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using StockApp.Models;
using Microsoft.AspNet.Identity;


namespace StockApp.Controllers
{
    public class BonRequisitionController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /BonRequisition/
        public ActionResult Index()
        {

            var UserID = User.Identity.GetUserId();
            var tb_bonrequisition = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "NON" && t.Validate == "0").OrderBy(t=>t.DateCreer).Include(t => t.AspNetUser);

            var qteCour = (from row in db.TB_bonRequisition
                           where row.UserId == UserID && row.IsSoumettre == "OUI" && (row.Validate == "0" || row.Validate == "1")
                           select row).Count();

            ViewBag.count = qteCour;

            var qteApprouver = (from row in db.TB_bonRequisition
                                where row.UserId == UserID && row.IsSoumettre == "OUI" && row.Validate == "2"
                                select row).Count();


            ViewBag.Approuver = qteApprouver;

            // var entreStock = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "NON" && t.Validate == "0").Include(t => t.AspNetUser);

            //var tb_bonrequisition = db.TB_bonRequisition.Include(t => t.AspNetUser);
            return View(tb_bonrequisition.ToList());
        }

        // ===================== Deuxieme Index =========================

        public ActionResult IndexSoumet()
        {

            var UserID = User.Identity.GetUserId();
            var tb_bonrequisition = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && t.Validate == "0").Include(t => t.AspNetUser);
            var Direction = from u in db.AspNetUsers
                            where u.Id == UserID
                            select u.Id_direction;
            var dir = Direction.FirstOrDefault();

            int direct = Convert.ToInt32(dir);



            if (User.IsInRole("DirectUtilisateur"))
            {
                var entreStock = from breq in db.TB_bonRequisition
                                 join user in db.AspNetUsers
                                 on breq.UserId equals user.Id
                                 where (breq.IsSoumettre == "OUI" && user.Id_direction == direct)
                                 select breq;


                return View(entreStock.ToList());
            }

            else
            {
                var entreStock = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && (t.Validate == "0" || t.Validate == "1")).Include(t => t.AspNetUser);
                //  int qteCour = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && t.Validate == "0").Include(t => t.AspNetUser).Count();
                var qteCour = (from row in db.TB_bonRequisition
                               where row.UserId == UserID && row.IsSoumettre == "OUI" && row.Validate == "0"
                               select row).Count();



                //var row = qteCour.fi

                ViewData["QteEncour"] = qteCour;
                ViewBag.count = qteCour;


                return View(entreStock.ToList());
            }




            //  return View(entreStock.ToPagedList(pageNumber, (int)pageSize));

            // return View(tb_bonrequisition.ToList());
        }

        // ==================== Troisieme Index===============================

        public ActionResult IndexApprouver()
        {

            var UserID = User.Identity.GetUserId();
            // var tb_bonrequisition = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && t.Validate == "0").Include(t => t.AspNetUser);
            var Direction = from u in db.AspNetUsers
                            where u.Id == UserID
                            select u.Id_direction;
            var dir = Direction.FirstOrDefault();

            int direct = Convert.ToInt32(dir);


            if (User.IsInRole("DirectUtilisateur"))
            {
                var entreStock = from breq in db.TB_bonRequisition
                                 join user in db.AspNetUsers
                                 on breq.UserId equals user.Id
                                 where (breq.IsSoumettre == "OUI" && user.Id_direction == direct)
                                 select breq;

                return View(entreStock.ToList());
            }

            else
            {
                var entreStock = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && t.Validate == "2").Include(t => t.AspNetUser);
                //  int qteCour = db.TB_bonRequisition.Where(t => t.UserId == UserID && t.IsSoumettre == "OUI" && t.Validate == "0").Include(t => t.AspNetUser).Count();
                var qteValider = (from row in db.TB_bonRequisition
                                  where row.UserId == UserID && row.IsSoumettre == "OUI" && row.Validate == "2"
                                  select row).Count();




                ViewBag.valider = qteValider;


                return View(entreStock.ToList());
            }


        }




        // GET: /BonRequisition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonRequisition tb_bonrequisition = db.TB_bonRequisition.Find(id);
            if (tb_bonrequisition == null)
            {
                return HttpNotFound();
            }
            return View(tb_bonrequisition);
        }

        // GET: /BonRequisition/Create

        public ActionResult Create()
        {

            AfficherNomComplet();
           
            ViewBag.Categorie = db.TB_categorie.ToList();
            ViewBag.Articles = new SelectList(db.TB_articles
                            .Where(c => c.Id_articles == 0), "Id_articles", "Nom_articles").ToList();
            return View();
        }
        
        //// POST: /BonRequisition/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="Id_bon_requisition,Description,UserId,Date_requisition,DateCreer,CreerPar,IsSoumettre,Validate,Date_aprouver,Date_validation,Administrateur,Directeur")] TB_bonRequisition tb_bonrequisition)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TB_bonRequisition.Add(tb_bonrequisition);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", tb_bonrequisition.UserId);
        //    return View(tb_bonrequisition);
        //}

        // GET: /BonRequisition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonRequisition tb_bonrequisition = db.TB_bonRequisition.Find(id);
            if (tb_bonrequisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", tb_bonrequisition.UserId);
            return View(tb_bonrequisition);
        }

        // POST: /BonRequisition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_bon_requisition,Description,UserId,Date_requisition,DateCreer,CreerPar")] TB_bonRequisition tb_bonrequisition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_bonrequisition).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", tb_bonrequisition.UserId);
            return View(tb_bonrequisition);
        }

        // GET: /BonRequisition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonRequisition tb_bonrequisition = db.TB_bonRequisition.Find(id);
            if (tb_bonrequisition == null)
            {
                return HttpNotFound();
            }
            return View(tb_bonrequisition);
        }

        // POST: /BonRequisition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_bonRequisition tb_bonrequisition = db.TB_bonRequisition.Find(id);
            db.TB_bonRequisition.Remove(tb_bonrequisition);
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

        // ============tous les Methodes suplementaires==================\\
        public JsonResult Save(TB_bonRequisition tb_bonentre)
        {

            try
            {
                if (ModelState.IsValid)
                {

                db.TB_bonRequisition.Add(tb_bonentre);

                db.SaveChanges();

                return Json(new { Success = 1, BonentrerID = tb_bonentre.Id_bon_requisition, ex = "" });

                }
            }
            catch (Exception ex)
            {

                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("les informations ne sont enregistrer").Message.ToString() });

        }

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


        public ActionResult UpdateSoumettre(int? id, FormCollection formcollection)
        {
            string soumettre = formcollection["soumettre"];

            var requisition = from c in db.TB_bonRequisition where c.Id_bon_requisition == id select c;

            var req = requisition.FirstOrDefault();
            req.IsSoumettre = "OUI";

            db.SaveChanges();

            //var Soumetre = from u in db.TB_bonRequisition
            //                  where u.Id_bon_requisition == id
            //                  select u.IsSoumettre;
            //    ViewBag.soum = Soumetre;
            return RedirectToAction("Index");
            //soumettre.
        }

        public ActionResult UpdateValider(int? id)
        {
            var UserID = User.Identity.GetUserId();
            //var userRoles = db.AspNetRoles.Include(r => r.AspNetUsers).ToList();


            var userNom = from u in db.AspNetUsers
                          where u.Id == UserID
                          select u.NomComplet;

            var nomDirect = userNom.FirstOrDefault();
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
            return RedirectToAction("IndexSoumet");
            //soumettre.
        }

        public void AfficherNomComplet()
        {

            var UserID = User.Identity.GetUserId();
            var userNom = from u in db.AspNetUsers
                          where u.Id == UserID
                          select u.NomComplet;
            ViewBag.nomComplet = userNom.FirstOrDefault();
        }

        // ============ fin des Methodes suplementaires==================\\
    }
}

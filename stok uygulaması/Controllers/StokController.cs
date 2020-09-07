using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using stok_uygulaması.Models;
using stok_uygulaması.UnitOfWork;

namespace stok_uygulaması.Controllers
{
    public class StokController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        public ActionResult Index()
        {
            return View(uow.GetRepository<tbl_Stok>().GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ad,kod,aciklama,adet")] tbl_Stok tbl_Stok)
        {
            if (ModelState.IsValid)
            {
                uow.GetRepository<tbl_Stok>().Add(tbl_Stok);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Stok);
        }


        public ActionResult Edit(int id)
        {
            tbl_Stok tbl_Stok = uow.GetRepository<tbl_Stok>().GetById(id);
            if (tbl_Stok == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Stok);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ad,kod,aciklama,adet")] tbl_Stok tbl_Stok)
        {
            if (ModelState.IsValid)
            {
                uow.GetRepository<tbl_Stok>().Update(tbl_Stok);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Stok);
        }


        public ActionResult Delete(int id)
        {
            tbl_Stok tbl_Stok = uow.GetRepository<tbl_Stok>().GetById(id);
            if (tbl_Stok == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Stok);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Stok tbl_Stok = uow.GetRepository<tbl_Stok>().GetById(id);
            uow.GetRepository<tbl_Stok>().Delete(tbl_Stok);
            uow.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

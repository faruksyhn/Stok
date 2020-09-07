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
    public class StokHareketController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();

        // GET: StokHareket
        public ActionResult Index()
        {
            var tbl_StokHareket = uow.GetRepository<tbl_StokHareket>().GetAll();
            return View(tbl_StokHareket.ToList());
        }

        // GET: StokHareket/Create
        public ActionResult Create()
        {
            ViewBag.stokID = new SelectList(uow.GetRepository<tbl_Stok>().GetAll(), "id", "ad");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stokID,alinanAdet,tarih")] tbl_StokHareket tbl_StokHareket)
        {
            tbl_Stok stoktakiUrun = uow.GetRepository<tbl_Stok>().GetById((int)tbl_StokHareket.stokID);
            if (ModelState.IsValid && stoktakiUrun.adet>tbl_StokHareket.alinanAdet)
            {
                uow.GetRepository<tbl_StokHareket>().Add(tbl_StokHareket);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Hata = "Stokta Yeteri kadar ürün bulunmamaktadır";
            }

            ViewBag.stokID = new SelectList(uow.GetRepository<tbl_Stok>().GetAll(), "id", "ad", tbl_StokHareket.stokID);
            return View(tbl_StokHareket);
        }
        public ActionResult Edit(int id)
        {
            tbl_StokHareket tbl_StokHareket = uow.GetRepository<tbl_StokHareket>().GetById(id);
            if (tbl_StokHareket == null)
            {
                return HttpNotFound();
            }
            ViewBag.stokID = new SelectList(uow.GetRepository<tbl_Stok>().GetAll(), "id", "ad", tbl_StokHareket.stokID);
            return View(tbl_StokHareket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stokID,alinanAdet,tarih")] tbl_StokHareket tbl_StokHareket)
        {
            if (ModelState.IsValid)
            {
                uow.GetRepository<tbl_StokHareket>().Update(tbl_StokHareket);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stokID = new SelectList(uow.GetRepository<tbl_Stok>().GetAll(), "id", "ad", tbl_StokHareket.stokID);
            return View(tbl_StokHareket);
        }

        public ActionResult Delete(int id)
        {
            tbl_StokHareket tbl_StokHareket = uow.GetRepository<tbl_StokHareket>().GetById(id);
            if (tbl_StokHareket == null)
            {
                return HttpNotFound();
            }
            return View(tbl_StokHareket);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_StokHareket tbl_StokHareket = uow.GetRepository<tbl_StokHareket>().GetById(id);
            uow.GetRepository<tbl_StokHareket>().Delete(tbl_StokHareket);
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

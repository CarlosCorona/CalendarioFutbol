using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarioFutbol.DataAccess;

namespace CalendarioFutbol.Controllers
{
    public class EquiposController : Controller
    {
        private CalendarioFutbolEntities db = new CalendarioFutbolEntities();

        // GET: Equipos
        public ActionResult Index()
        {
            return View(db.Equipos.ToList());
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoID,Nombre,Imagen,Eliminado")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                //todos los equipos al crearse no son eliminados
                equipos.Eliminado = false;

                db.Equipos.Add(equipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoID,Nombre,Imagen,Eliminado")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // No existe la elimnación Fisca solo logica
            Equipos equipos = db.Equipos.Find(id);
            equipos.Eliminado = true;
            db.Entry(equipos).State = EntityState.Modified;
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

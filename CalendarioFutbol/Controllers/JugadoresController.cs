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
    public class JugadoresController : Controller
    {
        private CalendarioFutbolEntities db = new CalendarioFutbolEntities();

        // GET: Jugadores
        public ActionResult Index()
        {
            return View(db.Jugadores.ToList());
        }

        // GET: Jugadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugadores jugadores = db.Jugadores.Find(id);
            if (jugadores == null)
            {
                return HttpNotFound();
            }
            return View(jugadores);
        }

        // GET: Jugadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JugadorID,Nombre")] Jugadores jugadores)
        {
            if (ModelState.IsValid)
            {
                // Los jugadores siempre se crean sin la bandera de eliminado
                jugadores.Eliminado = false;
                db.Jugadores.Add(jugadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jugadores);
        }

        // GET: Jugadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugadores jugadores = db.Jugadores.Find(id);
            if (jugadores == null)
            {
                return HttpNotFound();
            }
            return View(jugadores);
        }

        // POST: Jugadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JugadorID,Nombre,Eliminado")] Jugadores jugadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jugadores);
        }

        // GET: Jugadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugadores jugadores = db.Jugadores.Find(id);
            if (jugadores == null)
            {
                return HttpNotFound();
            }
            return View(jugadores);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // No existe la elimnación Fisca solo logica
            Jugadores jugadores = db.Jugadores.Find(id);
            jugadores.Eliminado = true;

            db.Entry(jugadores).State = EntityState.Modified;
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

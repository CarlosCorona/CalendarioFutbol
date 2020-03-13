using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarioFutbol.DataAccess;
using CalendarioFutbol.Models;

namespace CalendarioFutbol.Controllers
{
    public class TorneosController : Controller
    {
        private CalendarioFutbolEntities db = new CalendarioFutbolEntities();

        // GET: Torneos
        public ActionResult Index()
        {
            return View(db.Torneo.ToList());
        }

        // GET: Torneos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // GET: Torneos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Torneos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TorneoID,Nombre")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Torneo.Add(torneo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(torneo);
        }

        // GET: Torneos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // POST: Torneos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TorneoID,Nombre")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torneo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(torneo);
        }

        // GET: Torneos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // POST: Torneos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Torneo torneo = db.Torneo.Find(id);
            db.Torneo.Remove(torneo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Tablero(int id)
        {
            var torneo = new TableroVista();
            torneo.NombreTorneo = db.Torneo.Find(id).Nombre;
            torneo.JuegosTorneo = new List<JuegosLista>();
            var juegos = db.Juegos.Where(x => x.TorneoID == id).ToList();
            juegos.ForEach(delegate (Juegos juegos1) {
                torneo.JuegosTorneo.Add(new JuegosLista()
                {
                    Local = db.Equipos.Find(juegos1.EquipoLocalID).Nombre,
                    Imagenlocal = db.Equipos.Find(juegos1.EquipoLocalID).Imagen,
                    Visitante = db.Equipos.Find(juegos1.EquipoVisitanteID).Nombre,
                    ImagenVisitante = db.Equipos.Find(juegos1.EquipoVisitanteID).Imagen,
                    Horario = juegos1.FechaHoraPartido.ToString("dd/MM/yyyy HH:mm")
                });
            });
            return View(torneo);
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

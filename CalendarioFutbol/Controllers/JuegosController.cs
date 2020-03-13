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
    public class JuegosController : Controller
    {
        private CalendarioFutbolEntities db = new CalendarioFutbolEntities();

        // GET: Juegos
        public ActionResult Index( int id)
        {
            // Obtenemos los jugadores del equpo
            var TorneoJuegos = db.Juegos.Where(x => x.TorneoID == id).ToList();

            // Obtenemos los datos del torneo en base a su id.
            ViewData["Torneo"] = db.Torneo.Find(id).Nombre;
            ViewData["TorneoID"] = id;

            // Obtenemos la lista de los equipos
            var equipos = db.Equipos.Select(x => new EquipoLista { ID = x.EquipoID, Nombre = x.Nombre }).ToList();
            ViewData["Equipos"] = equipos;

            return View(TorneoJuegos);
        }

        // POST: Juegos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JuegosID,TorneoID,EquipoLocalID,EquipoVisitanteID,FechaHoraPartido")] Juegos juegos)
        {
            if (ModelState.IsValid)
            {
                db.Juegos.Add(juegos);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = juegos.TorneoID });
            }

            return View(juegos);
        }

        // Juegos/Delete/5
        public ActionResult Delete(int id)
        {
            Juegos juegos = db.Juegos.Find(id);
            var torneoID = juegos.TorneoID;
            db.Juegos.Remove(juegos);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = torneoID });
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

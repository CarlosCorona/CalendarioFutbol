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
    public class EquiposJugadoresController : Controller
    {
        private CalendarioFutbolEntities db = new CalendarioFutbolEntities();

        // GET: EquiposJugadores
        public ActionResult Index(int id)
        {
            // Obtenemos los jugadores del equpo
            var EquiposJugadores = db.EquiposJugadores.Where(x => x.EquipoID == id).ToList();

            // Obtenemos los datos del equipo en base a su id.
            ViewData["Equipo"] = db.Equipos.Find(id).Nombre;
            ViewData["EquipoID"] = id;

            // Obtenemos la lista de los jugadores
            var jugadores = db.Jugadores.Select(x => new JugadoresLista { ID = x.JugadorID, Nombre = x.Nombre }).ToList();

            // Obtenemos los que se van a mostrar en la tabla
            ViewData["Jugadores"] = jugadores.Where(x => (EquiposJugadores.Select(y => y.JugadorID).Contains(x.ID))).ToList();

            // Removemos los jugadores ya seleccionados en el equipo
            jugadores.RemoveAll(x => (EquiposJugadores.Select(y => y.JugadorID).Contains(x.ID)));
            ViewData["JugadoresFiltrados"] = jugadores;

            return View(EquiposJugadores);
        }

        // POST: EquiposJugadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoJugadorID,EquipoID,JugadorID")] EquiposJugadores equiposJugadores)
        {
            if (ModelState.IsValid)
            {
                db.EquiposJugadores.Add(equiposJugadores);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = equiposJugadores.EquipoID });
            }

            return View(equiposJugadores);
        }

        // Delete: EquiposJugadores/Delete/5
        public ActionResult Delete(int id)
        {
            EquiposJugadores equiposJugadores = db.EquiposJugadores.Find(id);
            var EquipoId = equiposJugadores.EquipoID;
            db.EquiposJugadores.Remove(equiposJugadores);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = EquipoId });
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

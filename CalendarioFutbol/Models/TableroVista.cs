using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarioFutbol.Models
{
    public class TableroVista
    {
        public string NombreTorneo { get; set; }
        public List<JuegosLista> JuegosTorneo { get; set; } 
    }
}
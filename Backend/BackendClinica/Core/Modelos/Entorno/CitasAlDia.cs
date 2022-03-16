using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class CitasAlDia
    {
        public string fecha { get; set; }
        public string disponibilidad { get; set; }
    }
    public class Recordatorio
    {
        public string numCita { get; set; }
        public string idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string contacto { get; set; }
        public string firma { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string notaCita { get; set; }
        public string diasAntes { get; set; }
    }
}

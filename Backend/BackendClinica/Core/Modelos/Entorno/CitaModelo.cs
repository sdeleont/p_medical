using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class CitaModelo
    {
        public string id_cita { get; set; }
        public string id_paciente { get; set; }
        public string id_usuario { get; set; }
        public string id_especialidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string nota { get; set; }
        public string enviarMsj { get; set; }
        public string diasAntes { get; set; }
        public string id_consulta { get; set; }
        public string id_consulta_flebologia { get; set; }

    }
}

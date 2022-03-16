using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class UtilesCalendario
    {
        public string fecha { get; set; }
    }
    public class CitaCalendario
    {
        public string fecha { get; set; }
        public string numCitas { get; set; }
        public string status { get; set; }
    }
    public class TipUsuario {
        public string nombre { get; set; }
    }
    public class ConfiguracionColores
    {
        public string citasRojo { get; set; }
        public string citasAmarillo { get; set; }
        public string citasVerde { get; set; }
    }
    public class CitaProgramada 
    {
        public string CODIGO { get; set; }
        public string NOMBRECOMPLETO { get; set; }
        public string CUI { get; set; }
        public string FECHANAC { get; set; }
        public string GENERO { get; set; }
        public string FECHAULTIMACONSULTA { get; set; }
        public string HORA { get; set; }
        public string NOTA { get; set; }
        public string USUARIO { get; set; }
        public string FIRMA { get; set; }

        public string citacodigo { get; set; }

        public string idEspecialidad { get; set; }
        public string enviarMsj { get; set; }
        public string diasAntes { get; set; }

    }
}

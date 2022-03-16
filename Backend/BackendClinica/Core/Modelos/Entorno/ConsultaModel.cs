using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class ConsultaModelo
    {
        public string id_consulta { get; set; }
        public string fecha { get; set; }
        public string observaciones { get; set; }
        public string estado_observaciones { get; set; }
        public string plan { get; set; }
        public string estado_plan { get; set; }
        public string diagnostico { get; set; }
        public string motivo_consulta { get; set; }
        public string evolucion_clinica { get; set; }
        public string estado_evolucion_clinica { get; set; }
        public string especialidad { get; set; }
        public string localidad { get; set; }

        public string firma { get; set; }
        public string id_especialidad { get; set; }
        public string id_paciente { get; set; }
        public string id_usuario { get; set; }
        public string id_localidad { get; set; }
        public List<DetalleConsultaModelo> detalle { get; set; }
    }

    public class DetalleConsultaModelo
    {
        public string id_detalle { get; set; }
        public string numero { get; set; }
        public string cantidad { get; set; }
        public string polidocanol { get; set; }
        public string area_anatomica { get; set; }
        public string id_consulta { get; set; }
        public string id_procedimiento { get; set; }
        public string id_metodo { get; set; }
        public string id_tipo_esclerosante { get; set; }
        public string procedimiento { get; set; }
        public string metodo { get; set; }
        public string tipo_esclerosante { get; set; }
    }

    public class ConsultaResponseModelo
    {
        public string id_consulta { get; set; }
        public string fecha { get; set; }
    }
    public class CriterioConsultaModelo
    {
        public string textoGeneral { get; set; }
        public string fechaExacta { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
        public string id_especialidad { get; set; }
        public string id_usuario { get; set; }
        public string paciente { get; set; }
        public string id_localidad { get; set; }
        public int numero { get; set; }
        public int cantidad { get; set; }
        public string evolucion { get; set; }
        public string diagnostico { get; set; }
        public string plan { get; set; }
    }
    public class ConsultaResponse {
        public string id_consulta { get; set; }
        public string fecha { get; set; }
        public string especialidad { get; set; }
        public string pacienteC { get; set; }
        public string fecha_nacimiento { get; set; }
        public string firma { get; set; }
        public string localidad { get; set; }
        public string idPaciente { get; set; }
        public string genero { get; set; }
        public string observaciones { get; set; }
        public string estadoObservaciones { get; set; }
        public string plan { get; set; }
        public string estadoPlan { get; set; }
        public string diagnostico { get; set; }
        public List<DetalleConsultaModelo> detalle { get; set; }
    }
}

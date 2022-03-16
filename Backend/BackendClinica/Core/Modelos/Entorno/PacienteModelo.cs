using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class PacienteModelo
    {
        public string id_paciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string alias { get; set; }
        public string cui { get; set; }
        public string genero { get; set; }
        public string fecha_nacimiento { get; set; }
        public string direccion { get; set; }
        public string nota_importante { get; set; }
        public string fecha_primer_consulta { get; set; }
        public string fecha_ultima_consulta { get; set; }
        public string motivo_consulta { get; set; }
        public string historia_enf_actual { get; set; }
        public string antecedente { get; set; }
        public string alergia { get; set; }
        public string examen_fisico { get; set; }
        public string estado { get; set; }
        public string historial_previo { get; set; }

        public string id_pais { get; set; }
        public string id_departamento { get; set; }
        public string id_usuario { get; set; }
        public List<TelefonoModelo> telefonos { get; set; }
        public List<DepartamentoModelo> departamentos { get; set; }
    }
    public class CriterioPacienteModelo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cui { get; set; }
        public string fechaNacimiento { get; set; }
        public string id_departamento { get; set; }
        public string id_pais { get; set; }
        public string genero { get; set; }
        public string textoGeneral { get; set; }
        public int numero { get; set; }
        public int cantidad { get; set; }
    }
    public class PacienteResponse
    {
        public string codigo { get; set; }
        public string nombreCompleto { get; set; }
        public string cui { get; set; }
        public string fechaNac { get; set; }
        public string pais { get; set; }
        public string depto { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
        public string aliasPac { get; set; }
        public List<TelefonoModelo> telefonos { get; set; }
    }
    public class InfoNotas {
        public string idPaciente { get; set; }
        public List<NotaMedica> notas { get; set; }
    }
    public class NotaMedica
    {
        public string idPaciente { get; set; }
        public string nota { get; set; }
        public string fecha { get; set; }
        public string idUsuario { get; set; }
        public string orden { get; set; }
        public string importante { get; set; }
    }
}

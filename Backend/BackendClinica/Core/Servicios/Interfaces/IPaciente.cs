using Core.Modelos.Entorno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface IPaciente
    {
        Task<List<PacienteModelo>> ObtenerUltimoIDPaciente();
        Task<List<PacienteModelo>> ObtenerPaciente(string idPaciente);
        Task<List<NotaMedica>> ObtenerNotas(string idPaciente);
        Task<ResponseServer> ActualizaNotas(InfoNotas info);
        Task<ResponseServer> CrearPaciente(PacienteModelo paciente);
        Task<List<PacienteResponse>> ObtenerPacientes(CriterioPacienteModelo data);
        Task<ResponseServer> EliminarPacientte(string idPaciente);
        Task<ResponseServer> CrearHistorial(PacienteModelo paciente);
        Task<ResponseServer> EditarPaciente(PacienteModelo paciente);
        Task<ResponseServer> CargaMasiva();
    }
}

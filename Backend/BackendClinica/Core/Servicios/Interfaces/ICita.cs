using Core.Modelos.Entorno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface ICita
    {
        Task<ResponseServer> CrearCita(CitaModelo cita);
        Task<ResponseServer> EliminarCita(CitaModelo cita);
        Task<ResponseServer> ModificarCita(CitaModelo cita);
        Task<List<CitaCalendario>> ObtenerCalendario(string usuario, string month, string year);
        Task<List<CitaProgramada>> ObtenerPacientesCitados(string fecha);
        Task<int> EnvioDeRecordatorios();
        Task<ResponseServer> EnviarMSJ(MensajeTextoModelo msj);
    }
}

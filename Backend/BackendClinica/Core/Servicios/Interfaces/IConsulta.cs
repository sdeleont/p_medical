using Core.Modelos.Entorno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface IConsulta
    {
        Task<List<Catalogos>> ObtenerCatalogo(string catalogo);
        Task<List<ConsultaModelo>> ObtenerConsulta(string idConsulta, string tipoConsulta);
        Task<List<LocalidadModelo>> ObtenerLocalidad();
        Task<List<ConsultaResponse>> ObtenerConsultas(CriterioConsultaModelo data);
        Task<ResponseServer> CrearConsulta(ConsultaModelo consulta);
        Task<ResponseServer> EditarConsulta(ConsultaModelo consulta);
    }
}

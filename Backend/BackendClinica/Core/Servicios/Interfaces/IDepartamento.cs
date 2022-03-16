using Core.Modelos.Entorno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface IDepartamento
    {
        Task<List<DepartamentoModelo>> ObtenerDepartamentos(string idPais);
        Task<List<DepartamentoModelo>> ObtenerDepartamento(string idDepartamento);
    }
}

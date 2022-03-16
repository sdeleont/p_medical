using Core.Modelos.Entorno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface IPais
    {
        Task<List<PaisModelo>> ObtenerPais();
    }
}

using Core.Modelos.Entorno;
using Core.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Impl
{
    public class Pais : IPais
    {
        public Configuracion conf;
        public Pais(Configuracion conf)
        {
            this.conf = conf;
        }

        public async Task<List<PaisModelo>> ObtenerPais()
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Pais repo = new Core.Repositorios.Pais(_conn);
                        var response = await repo.ObtenerPais();
                        _conn.Close();
                        return response;
                    }
                    catch (Exception ex) {
                        _conn.Close();
                        throw new Exception(ex.Message);
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

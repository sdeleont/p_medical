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
    public class Rol: IRol
    {
        public Configuracion conf;
        public Rol(Configuracion conf)
        {
            this.conf = conf;
        }

        public async Task<List<RolModelo>> ObtenerRoles()
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Rol repo = new Core.Repositorios.Rol(_conn);
                        var response = await repo.ObtenerRoles();
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

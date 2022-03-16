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
    public class Departamento : IDepartamento
    {
        public Configuracion conf;
        public Departamento(Configuracion conf)
        {
            this.conf = conf;
        }

        public async Task<List<DepartamentoModelo>> ObtenerDepartamentos(string idPais)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Departamento repo = new Core.Repositorios.Departamento(_conn);
                        var response = await repo.ObtenerDepartamentos(idPais);
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

        public async Task<List<DepartamentoModelo>> ObtenerDepartamento(string idDepartamento)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Departamento repo = new Core.Repositorios.Departamento(_conn);
                        var response = await repo.ObtenerDepartamento(idDepartamento);
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

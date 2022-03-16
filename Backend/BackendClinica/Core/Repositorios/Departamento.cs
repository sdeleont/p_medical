using Core.Modelos.Entorno;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    class Departamento : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Departamento(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Departamento(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn;
            this.transaction = transaction;
        }
        public async Task<List<DepartamentoModelo>> ObtenerDepartamentos(string id_pais)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PAIS", id_pais);
            string sql = @"SELECT *  FROM DEPARTAMENTO WHERE ID_PAIS = @ID_PAIS";
            Consultas.clsQueryAsyncConn<DepartamentoModelo> objQuery = new Consultas.clsQueryAsyncConn<DepartamentoModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<List<DepartamentoModelo>> ObtenerDepartamento(string idDepartamento)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_DEPARTAMENTO", idDepartamento);
            string sql = @"SELECT *  FROM DEPARTAMENTO WHERE ID_DEPARTAMENTO = @ID_DEPARTAMENTO";
            Consultas.clsQueryAsyncConn<DepartamentoModelo> objQuery = new Consultas.clsQueryAsyncConn<DepartamentoModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
    }
}

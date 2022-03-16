using Core.Modelos.Entorno;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    class Pais : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Pais(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Pais(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn;
            this.transaction = transaction;
        }
        public async Task<List<PaisModelo>> ObtenerPais()
        {
            string sql = @"SELECT *  FROM PAIS";
            Consultas.clsQueryAsyncConn<PaisModelo> objQuery = new Consultas.clsQueryAsyncConn<PaisModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
    }
}

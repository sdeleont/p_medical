using Core.Modelos.Entorno;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    class Rol : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Rol(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Rol(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn;
            this.transaction = transaction;
        }
        public async Task<List<RolModelo>> ObtenerRoles()
        {
            string sql = @"SELECT *  FROM ROL";
            Consultas.clsQueryAsyncConn<RolModelo> objQuery = new Consultas.clsQueryAsyncConn<RolModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
    }
}

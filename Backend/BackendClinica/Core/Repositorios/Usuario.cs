using Core.Modelos.Entorno;
using Core.Modelos.Tests;
using Core.Utils.Security;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    public class Usuario: Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Usuario(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Usuario(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn;
            this.transaction = transaction;
        }

        public async Task<List<UsuarioModelo>> ObtenerUsuario(string usuario)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":USUARIO", usuario);
            string sql = @"SELECT USUARIO FROM USUARIO WHERE USUARIO = @USUARIO";
            Consultas.clsQueryAsyncConn<UsuarioModelo> objQuery = new Consultas.clsQueryAsyncConn<UsuarioModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);

            return existe.AsList();
        }

        public async Task<List<UsuarioModelo>> ObtenerUsuarioById(string idUsuario)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_USUARIO", idUsuario);
            string sql = @"SELECT * FROM USUARIO WHERE ID_USUARIO = @ID_USUARIO";
            Consultas.clsQueryAsyncConn<UsuarioModelo> objQuery = new Consultas.clsQueryAsyncConn<UsuarioModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);

            return existe.AsList();
        }

        public async Task<List<UsuarioModelo>> ObtenerUsuarios()
        {
            string sql = @"SELECT U.ID_USUARIO, U.USUARIO, U.FIRMA, R.NOMBRE AS ROL FROM USUARIO U, ROL R WHERE U.ID_ROL = R.ID_ROL AND ESTADO='A' AND U.USUARIO != 'ADMIN'";
            Consultas.clsQueryAsyncConn<UsuarioModelo> objQuery = new Consultas.clsQueryAsyncConn<UsuarioModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);

            return existe.AsList();
        }

        public async Task<int> CrearUsuario(UsuarioModelo usuario)
        {
                usuario.password = Encriptador.Encriptar(usuario.password);
                
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add(":USUARIO", usuario.usuario);
                dynamicParameters.Add(":PASSWORD", usuario.password);
                dynamicParameters.Add(":FIRMA", usuario.firma);
                dynamicParameters.Add(":ID_ROL", usuario.id_rol);

                string Query = @"INSERT INTO USUARIO (USUARIO, PASSWORD, FIRMA, ESTADO, ID_ROL) VALUES (@USUARIO, @PASSWORD, @FIRMA, 'A' ,@ID_ROL)";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
        
        public async Task<int> CambiarPassword(PasswordModelo password)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_USUARIO", password.id_usuario);
            dynamicParameters.Add(":PASSWORD", password.newPassword);

            string Query = @"UPDATE USUARIO SET PASSWORD = @PASSWORD WHERE ID_USUARIO = @ID_USUARIO";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<int> EliminarUsuario(string idUsuario)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_USUARIO", idUsuario);

            string Query = @"UPDATE USUARIO SET ESTADO='E' WHERE ID_USUARIO = @ID_USUARIO";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
        public async Task<List<UsuarioModelo>> ObtenerUsuarioAuth(string usuario)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":USUARIO", usuario);
            //string sql = @"SELECT * FROM USUARIO WHERE USUARIO = @USUARIO AND ESTADO = 'A'";
            string sql = @"SELECT *, (R.NOMBRE) AS ROL FROM USUARIO U, ROL R WHERE U.USUARIO = @USUARIO AND ESTADO = 'A' AND R.ID_ROL = U.ID_ROL;";
            Consultas.clsQueryAsyncConn<UsuarioModelo> objQuery = new Consultas.clsQueryAsyncConn<UsuarioModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);

            return existe.AsList();
        }

    }
}

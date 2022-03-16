using Core.Modelos.Entorno;
using Core.Modelos.Tests;
using Core.Servicios.Interfaces;
using Core.Utils.ExcelReader;
using Core.Utils.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Impl
{
    public class Usuario : IUsuario
    {
        public Configuracion conf;
        public Usuario(Configuracion conf)
        {
            this.conf = conf;
        }

        public async Task<List<UsuarioModelo>> ObtenerUsuario(string usuario)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Usuario repo = new Core.Repositorios.Usuario(_conn);
                        var response = await repo.ObtenerUsuario(usuario);
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

        public async Task<List<UsuarioModelo>> ObtenerUsuarios()
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Usuario repo = new Core.Repositorios.Usuario(_conn);
                        var response = await repo.ObtenerUsuarios();
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

        public async Task<ResponseServer> CrearUsuario(UsuarioModelo usuario) {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    _conn.Open();
                    using (var transaction = _conn.BeginTransaction())
                    {
                        try
                        {
                            ResponseServer response = new ResponseServer();
                            var existeUsuario = await ObtenerUsuario(usuario.usuario);
                            if (existeUsuario.Count == 0)
                            {
                                Repositorios.Usuario repo = new Repositorios.Usuario(_conn, transaction);
                                await repo.CrearUsuario(usuario);
                                response.status = "OK";
                                response.mensaje = "Usuario creado con éxito";


                                transaction.Commit();
                                _conn.Close();
                                return response;

                            }
                            else {
                                response.status = "ERROR";
                                response.mensaje = "El usuario ya existe en la base de datos";
                                transaction.Rollback();
                                _conn.Close();
                                return response;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _conn.Close();
                            throw new Exception(ex.Message);
                        }
                    }


                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseServer> EliminarUsuario(string idUsuario)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    _conn.Open();
                    using (var transaction = _conn.BeginTransaction())
                    {
                        try
                        {
                            Repositorios.Usuario repo = new Repositorios.Usuario(_conn, transaction);
                            ResponseServer response = new ResponseServer();
                            await repo.EliminarUsuario(idUsuario);
                            response.status = "OK";
                            response.mensaje = "Usuario eliminado con éxito";


                            transaction.Commit();
                            _conn.Close();
                            return response;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _conn.Close();
                            throw new Exception(ex.Message);
                        }
                    }


                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseServerAuth> AutenticarUsuario(UsuarioAuth usuario)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    _conn.Open();
                    using (var transaction = _conn.BeginTransaction())
                    {
                        try
                        {
                            Repositorios.Usuario repo = new Repositorios.Usuario(_conn, transaction);
                            ResponseServerAuth response = new ResponseServerAuth();
                            response.status = "Error";
                            response.mensaje = "Autenticación Fallida";

                            List<UsuarioModelo> usuarioAuth = await repo.ObtenerUsuarioAuth(usuario.usuario);
                            if (usuarioAuth.Count == 1) { //tiene que existir un solo usuario
                                string contra = Encriptador.Encriptar(usuario.pass);
                                if (contra.Equals(usuarioAuth[0].password)) { //autentico con exito
                                    response.status = "OK";
                                    response.mensaje = "Autenticación Correcta";
                                    response.user = new UsuarioModelo();
                                    response.user.id_rol = usuarioAuth[0].id_rol;
                                    response.user.usuario = usuarioAuth[0].usuario;
                                    response.user.firma = usuarioAuth[0].firma;
                                    response.user.id_usuario = usuarioAuth[0].id_usuario;
                                    response.user.rol = usuarioAuth[0].rol;
                                }
                            }
                            
                            transaction.Commit();
                            _conn.Close();
                            return response;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _conn.Close();
                            throw new Exception(ex.Message);
                        }
                    }


                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseServerAuth> ResetearPassword(PasswordModelo password)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    _conn.Open();
                    using (var transaction = _conn.BeginTransaction())
                    {
                        try
                        {
                            Repositorios.Usuario repo = new Repositorios.Usuario(_conn, transaction);
                            ResponseServerAuth response = new ResponseServerAuth();
                            response.status = "Error";
                            response.mensaje = "Actualización de Password Fallida";

                            List<UsuarioModelo> usuarioAuth = await repo.ObtenerUsuarioById(password.id_usuario);
                            if (usuarioAuth.Count == 1)
                            { //tiene que existir un solo usuario
                                password.newPassword = Encriptador.Encriptar(usuarioAuth[0].usuario.ToLower());
                                await repo.CambiarPassword(password);

                                response.status = "OK";
                                response.mensaje = "Actualización de Password Correcta";
                            }

                            transaction.Commit();
                            _conn.Close();
                            return response;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _conn.Close();
                            throw new Exception(ex.Message);
                        }
                    }


                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseServerAuth> CambiarPassword(PasswordModelo password)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    _conn.Open();
                    using (var transaction = _conn.BeginTransaction())
                    {
                        try
                        {
                            Repositorios.Usuario repo = new Repositorios.Usuario(_conn, transaction);
                            ResponseServerAuth response = new ResponseServerAuth();
                            response.status = "Error";
                            response.mensaje = "Actualización de Password Fallida";

                            List<UsuarioModelo> usuarioAuth = await repo.ObtenerUsuarioById(password.id_usuario);
                            if (usuarioAuth.Count == 1)
                            { //tiene que existir un solo usuario
                                string contra = Encriptador.Encriptar(password.oldPassword);
                                if (contra.Equals(usuarioAuth[0].password))
                                { //autentico con exito
                                    password.newPassword = Encriptador.Encriptar(password.newPassword);
                                    await repo.CambiarPassword(password);

                                    response.status = "OK";
                                    response.mensaje = "Actualización de Password Correcta";
                                }
                            }

                            transaction.Commit();
                            _conn.Close();
                            return response;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _conn.Close();
                            throw new Exception(ex.Message);
                        }
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

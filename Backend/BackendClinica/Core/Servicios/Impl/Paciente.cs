using Core.Modelos.Entorno;
using Core.Servicios.Interfaces;
using Core.Utils.ExcelReader;
using Core.Utils.UUID;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Impl
{
    public class Paciente : IPaciente
    {
        public Configuracion conf;
        public Paciente(Configuracion conf)
        {
            this.conf = conf;
        }

        public async Task<List<PacienteModelo>> ObtenerUltimoIDPaciente()
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Paciente repo = new Core.Repositorios.Paciente(_conn);
                        var response = await repo.ObtenerUltimoIDPaciente();
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
        public async Task<List<NotaMedica>> ObtenerNotas(string idPaciente)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Paciente repo = new Core.Repositorios.Paciente(_conn);
                        var response = await repo.ObtenerNotasMedicas(idPaciente);
                        _conn.Close();
                        return response;
                    }
                    catch (Exception ex)
                    {
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
        public async Task<ResponseServer> ActualizaNotas(InfoNotas info)
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
                            ResponseServer response = new ResponseServer();
                            response.status = "Error";
                            Repositorios.Paciente repo = new Repositorios.Paciente(_conn, transaction);
                            await repo.BorrarNotas(info.idPaciente);
                            for (int i = 0; i < info.notas.Count; i++) {
                                await repo.InsertarNota(info.notas[i], i+1);
                            }
                            response.status = "OK";
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
        public async Task<ResponseServer> CrearPaciente(PacienteModelo paciente)
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
                            ResponseServer response = new ResponseServer();
                            var ultimoIDPaciente = await ObtenerUltimoIDPaciente();
                            var idPaciente = UUID.generarUUID(ultimoIDPaciente[0].id_paciente);
                            if (idPaciente != null) {
                                paciente.id_paciente = idPaciente;
                                Repositorios.Paciente repo = new Repositorios.Paciente(_conn, transaction);
                                
                                await repo.CrearPaciente(paciente);
                                foreach(TelefonoModelo telefono in paciente.telefonos){
                                    telefono.id_paciente = idPaciente;
                                    await repo.CrearTelefono(telefono);
                                }
                                response.status = "OK";
                                response.mensaje = idPaciente;


                                transaction.Commit();
                                _conn.Close();
                                return response;
                            }
                            else
                            {
                                response.status = "OK";
                                response.mensaje = "Se alcanzo el límite de ID's validos para pacientes";
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
        public async Task<List<PacienteResponse>> ObtenerPacientes(CriterioPacienteModelo paciente)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        List<PacienteResponse> pacientes = new List<PacienteResponse>();
                        Repositorios.Paciente repo = new Repositorios.Paciente(_conn);
                        // el paciente.numero determina si la busqueda fue por todos los campos(0) o un criterio especifico(1)
                        if (paciente.numero == 1)
                            pacientes = await repo.ObtenerPacientesCriterio(paciente);
                        else
                            pacientes = await repo.ObtenerPacientesCoincidencias(paciente);
                        foreach (PacienteResponse pacienteResponse in pacientes) {
                            pacienteResponse.telefonos = await repo.ObtenerTelefonos(pacienteResponse.codigo);
                        }
                        _conn.Close();
                        return pacientes;
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

        public async Task<ResponseServer> EliminarPacientte(string idPaciente)
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
                            Repositorios.Paciente repo = new Repositorios.Paciente(_conn, transaction);
                            ResponseServer response = new ResponseServer();
                            await repo.EliminarPaciente(idPaciente);
                            response.status = "OK";
                            response.mensaje = "Paciente eliminado con éxito";


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

        public async Task<ResponseServer> CrearHistorial(PacienteModelo paciente)
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
                            ResponseServer response = new ResponseServer();
                            Repositorios.Paciente repo = new Repositorios.Paciente(_conn, transaction);
                            await repo.CrearHistorial(paciente);
                            response.status = "OK";
                            response.mensaje = "Historial creado con éxito";


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

        public async Task<ResponseServer> EditarPaciente(PacienteModelo paciente)
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
                            ResponseServer response = new ResponseServer();
                            Repositorios.Paciente repo = new Repositorios.Paciente(_conn, transaction);
                            await repo.EditarPaciente(paciente);

                            await repo.EliminarTelefonos(paciente.id_paciente);

                            foreach (TelefonoModelo telefono in paciente.telefonos)
                            {
                                telefono.id_paciente = paciente.id_paciente;
                                await repo.CrearTelefono(telefono);
                            }

                            response.status = "OK";
                            response.mensaje = paciente.id_paciente;


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

        public async Task<List<PacienteModelo>> ObtenerPaciente(string idPaciente)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Core.Repositorios.Paciente repo = new Core.Repositorios.Paciente(_conn);
                        var response = await repo.ObtenerPaciente(idPaciente);

                        if (response.Count > 0)
                        {
                            Core.Repositorios.Departamento repoDepto = new Core.Repositorios.Departamento(_conn);
                            var responseDepto = await repoDepto.ObtenerDepartamento(response[0].id_departamento);
                            response[0].id_pais = responseDepto[0].id_pais;

                            var responseDeptoList = await repoDepto.ObtenerDepartamentos(responseDepto[0].id_pais);
                            response[0].departamentos = responseDeptoList;

                            Core.Repositorios.Paciente repoTelefonos = new Core.Repositorios.Paciente(_conn);
                            var responseTelefonos = await repoTelefonos.ObtenerTelefonos(response[0].id_paciente);
                            response[0].telefonos = responseTelefonos;
                        }

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

        

        public async Task<ResponseServer> CargaMasiva()
        {
            String idPaciente = "AXXXX";
            String idDepartamento = "-1";
            try
            {
                ResponseServer response = new ResponseServer();
                response.status = "Error";
                response.mensaje = "Carga masiva fallida";
                var filePath = @"C:\carga_masiva.csv";
                var resultado = ExcelReader.loadData(filePath);

                foreach (PacienteModelo paciente in resultado)
                {
                    idPaciente = paciente.id_paciente;
                    idDepartamento = paciente.id_departamento;
                    await CrearPaciente(paciente);
                    await CrearHistorial(paciente);
                }
                
                response.status = "OK";
                response.mensaje = "Carga masiva completada";
                return response;
                
            }
            catch (Exception e)
            {
                throw new Exception("ID PACIENTE: " + idPaciente + "->" + e.Message);
            }
        }
    }
}

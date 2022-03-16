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
    public class Consulta: IConsulta
    {
        public Configuracion conf;
        public Consulta(Configuracion conf)
        {
            this.conf = conf;
        }
        public async Task<List<Catalogos>> ObtenerCatalogo(string catalogo)
        {
            try
            {
                List<Catalogos> catalogosReturn = new List<Catalogos>();
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Repositorios.Consulta repo = new Repositorios.Consulta(_conn);
                        if (catalogo.Equals("procedimiento"))
                        {
                            catalogosReturn = await repo.ObtenerProcedimientos();
                        }
                        else if (catalogo.Equals("metodo"))
                        {
                            catalogosReturn = await repo.ObtenerMetodos();
                        }
                        else if (catalogo.Equals("tipoesclerosante"))
                        {
                            catalogosReturn = await repo.ObtenerTipoEsclerosante();
                        }
                        else if (catalogo.Equals("especialidad"))
                        {
                            catalogosReturn = await repo.ObtenerEspecialidad();
                        }
                        else if (catalogo.Equals("firma"))
                        {
                            catalogosReturn = await repo.ObtenerFirmas();
                        }
                        else if (catalogo.Equals("localidad"))
                        {
                            catalogosReturn = await repo.ObtenerLocalidades();
                        }
                        _conn.Close();
                    }
                    catch (Exception ex)
                    {
                        _conn.Close();
                        throw new Exception(ex.Message);
                    }
                    
                }
                return catalogosReturn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<LocalidadModelo>> ObtenerLocalidad()
        {
            try
            {
                List<LocalidadModelo> localidades = new List<LocalidadModelo>();
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Repositorios.Consulta repo = new Repositorios.Consulta(_conn);
                        localidades = await repo.ObtenerLocalidad();
                        _conn.Close();
                    }
                    catch (Exception ex) {
                        _conn.Close();
                        throw new Exception(ex.Message);
                    }
                }
                return localidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ConsultaResponse>> ObtenerConsultas(CriterioConsultaModelo data)
        {
            try
            {
                List<ConsultaResponse> consultas = new List<ConsultaResponse>();
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Repositorios.Consulta repo = new Repositorios.Consulta(_conn);
                        // el paciente.numero determina si la busqueda fue por todos los campos(0) o un criterio especifico(1)
                        if (data.numero == 1)
                        {
                            consultas = await repo.ObtenerConsultasCriterio(data);

                            if (consultas.Count > 0)
                            {
                                foreach (ConsultaResponse consultaResponse in consultas)
                                {
                                    consultaResponse.detalle = await repo.ObtenerDetalleConsulta(consultaResponse.id_consulta);
                                }
                            }
                        }
                        else {
                            consultas = await repo.ObtenerConsultasCoincidencias(data);
                            
                            if (consultas.Count > 0)
                            {
                                foreach (ConsultaResponse consultaResponse in consultas)
                                {
                                    consultaResponse.detalle = await repo.ObtenerDetalleConsulta(consultaResponse.id_consulta);
                                }
                            }
                        }

                        _conn.Close();
                    }
                    catch (Exception ex) {
                        _conn.Close();
                        throw new Exception(ex.Message);
                    }
                }
                return consultas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ResponseServer> CrearConsulta(ConsultaModelo consulta)
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
                            Repositorios.Consulta repo = new Repositorios.Consulta(_conn, transaction);
                            Repositorios.Paciente pacienteRepo = new Repositorios.Paciente(_conn, transaction);
                            int id_localidad = await getLocalidad(consulta, repo);
                            consulta.id_localidad = id_localidad.ToString();
                            if (consulta.especialidad != null && consulta.especialidad.ToUpper().Equals("FLEBOLOGIA"))
                            {
                                int idConsulta = await repo.CrearConsultaFlebologia(consulta);
                                await pacienteRepo.ActualizarFechaUltimaConsulta(consulta.id_paciente);
                                if (idConsulta > 0)
                                {
                                    foreach (DetalleConsultaModelo detalle in consulta.detalle)
                                    {
                                        int idProcedimiento = await getProcedimiento(detalle, repo);
                                        int idMetodo = await getMetodo(detalle, repo);
                                        int idTipoEsclerosante = await getTipoEsclerosante(detalle, repo);
                                        detalle.id_consulta = idConsulta.ToString();
                                        detalle.id_procedimiento = idProcedimiento.ToString();
                                        detalle.id_metodo = idMetodo.ToString();
                                        detalle.id_tipo_esclerosante = idTipoEsclerosante.ToString();
                                        await repo.CrearDetalleConsultaFlebologia(detalle);
                                    }
                                    List<ConsultaModelo> nuevaConsulta = await repo.ObtenerConsultaFlebologia(idConsulta.ToString());
                                    if (nuevaConsulta.Count > 0) {
                                        response.datos.Add(nuevaConsulta[0].fecha);
                                    }
                                    response.status = "OK";
                                    response.mensaje = "Se ingreso la consulta correctamente";
                                    transaction.Commit();
                                }
                            }
                            else {
                                int idEspecialidad = await getEspecialidad(consulta, repo);
                                consulta.id_especialidad = idEspecialidad.ToString();

                                int idConsulta = await repo.CrearConsultaGeneral(consulta);
                                await pacienteRepo.ActualizarFechaUltimaConsulta(consulta.id_paciente);

                                List<ConsultaModelo> nuevaConsulta = await repo.ObtenerConsultaGeneral(idConsulta.ToString());
                                if (nuevaConsulta.Count > 0)
                                {
                                    response.datos.Add(nuevaConsulta[0].fecha);
                                }
                                response.status = "OK";
                                response.mensaje = "Se ingreso la consulta correctamente";
                                transaction.Commit();
                            }

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

        private async Task<int> getLocalidad(ConsultaModelo consulta, Repositorios.Consulta repo)
        {
            if (consulta.id_localidad.Equals("-1"))
            {
                List<LocalidadModelo> localidades = await repo.ObtenerLocalidadByName(consulta.localidad.Trim());
                return localidades.Count > 0 ? Int16.Parse(localidades[0].id_localidad) : await repo.CrearLocalidad(consulta.localidad.Trim());
            }
            return Int16.Parse(consulta.id_localidad);
        }

        private async Task<int> getEspecialidad(ConsultaModelo consulta, Repositorios.Consulta repo) {
            if (consulta.id_especialidad.Equals("-1")) {
                List<Catalogos> especialidades = await repo.ObtenerEspecialidadByName(consulta.especialidad.Trim());
                return especialidades.Count > 0 ? Int16.Parse(especialidades[0].id) : await repo.CrearEspecialidad(consulta.especialidad.Trim());
            }
            return Int16.Parse(consulta.id_especialidad);
        }

        private async Task<int> getProcedimiento(DetalleConsultaModelo detalle, Repositorios.Consulta repo)
        {
            if (detalle.id_procedimiento.Equals("-1"))
            {
                List<Catalogos> procedimientos = await repo.ObtenerProcedimientosByName(detalle.procedimiento.Trim());
                return procedimientos.Count > 0 ? Int16.Parse(procedimientos[0].id) : await repo.CrearProcedimiento(detalle.procedimiento.Trim());
            }
            return Int16.Parse(detalle.id_procedimiento);
        }

        private async Task<int> getMetodo(DetalleConsultaModelo detalle, Repositorios.Consulta repo)
        {
            if (detalle.id_metodo.Equals("-1"))
            {
                List<Catalogos> metodos = await repo.ObtenerMetodosByName(detalle.metodo.Trim());
                return metodos.Count > 0 ? Int16.Parse(metodos[0].id) : await repo.CrearMetodo(detalle.metodo.Trim());
            }
            return Int16.Parse(detalle.id_metodo);
        }

        private async Task<int> getTipoEsclerosante(DetalleConsultaModelo detalle, Repositorios.Consulta repo)
        {
            if (detalle.id_tipo_esclerosante.Equals("-1"))
            {
                List<Catalogos> tipoEsclerosantes = await repo.ObtenerTipoEsclerosanteByName(detalle.tipo_esclerosante.Trim());
                return tipoEsclerosantes.Count > 0 ? Int16.Parse(tipoEsclerosantes[0].id) : await repo.CrearTipoEsclerosante(detalle.tipo_esclerosante.Trim());
            }
            return Int16.Parse(detalle.id_tipo_esclerosante);
        }

        public async Task<List<ConsultaModelo>> ObtenerConsulta(string idConsulta, string tipoConsulta)
        {
            try
            {
                List<ConsultaModelo> consultas = new List<ConsultaModelo>();
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        Repositorios.Consulta repo = new Repositorios.Consulta(_conn);
                        if (tipoConsulta.ToUpper().Equals("FLEBOLOGIA"))
                        {
                            consultas = await repo.ObtenerConsultaFlebologia(idConsulta);
                            if (consultas.Count > 0)
                            {
                                consultas[0].detalle = await repo.ObtenerDetalleConsulta(idConsulta);
                            }
                        }
                        else
                        {
                            consultas = await repo.ObtenerConsultaGeneral(idConsulta);
                        }
                        _conn.Close();
                    }
                    catch (Exception ex) {
                        _conn.Close();
                        throw new Exception(ex.Message);
                    }
                }
                return consultas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseServer> EditarConsulta(ConsultaModelo consulta)
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
                            Repositorios.Consulta repo = new Repositorios.Consulta(_conn, transaction);
                            Repositorios.Paciente pacienteRepo = new Repositorios.Paciente(_conn, transaction);

                            if (consulta.especialidad != null && consulta.especialidad.ToUpper().Equals("FLEBOLOGIA"))
                            {
                                await repo.EditarConsultaFlebologia(consulta);
                                //await pacienteRepo.ActualizarFechaUltimaConsulta(consulta.id_paciente);
                                await repo.EliminarTratamientos(consulta.id_consulta);

                                foreach (DetalleConsultaModelo detalle in consulta.detalle)
                                {
                                    int idProcedimiento = await getProcedimiento(detalle, repo);
                                    int idMetodo = await getMetodo(detalle, repo);
                                    int idTipoEsclerosante = await getTipoEsclerosante(detalle, repo);
                                    detalle.id_consulta = consulta.id_consulta;
                                    detalle.id_procedimiento = idProcedimiento.ToString();
                                    detalle.id_metodo = idMetodo.ToString();
                                    detalle.id_tipo_esclerosante = idTipoEsclerosante.ToString();
                                    await repo.CrearDetalleConsultaFlebologia(detalle);
                                }
                                
                                response.status = "OK";
                                response.mensaje = "Se actualizo la consulta correctamente";
                                transaction.Commit();
                                
                            }
                            else
                            {
                                /*int idEspecialidad = await getEspecialidad(consulta, repo);
                                consulta.id_especialidad = idEspecialidad.ToString();
                                */
                                await repo.EditarConsultaGeneral(consulta);
                                //await pacienteRepo.ActualizarFechaUltimaConsulta(consulta.id_paciente);
                                
                                response.status = "OK";
                                response.mensaje = "Se ingreso la consulta correctamente";
                                transaction.Commit();
                            }

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

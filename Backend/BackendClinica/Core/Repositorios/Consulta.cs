using Core.Modelos.Entorno;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    public class Consulta : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Consulta(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Consulta(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn; this.transaction = transaction;

        }
        public async Task<List<Catalogos>> ObtenerProcedimientos()
        {
            string sql = @"SELECT ID_PROCEDIMIENTO ID , NOMBRE FROM PROCEDIMIENTO";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerProcedimientosByName(string nombre)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOMBRE", nombre);

            string sql = @"SELECT ID_PROCEDIMIENTO ID , NOMBRE FROM PROCEDIMIENTO WHERE NOMBRE = @NOMBRE";

            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerMetodos()
        {
            string sql = @"SELECT ID_METODO ID , NOMBRE FROM METODO";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerMetodosByName(string nombre)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOMBRE", nombre);

            string sql = @"SELECT ID_METODO ID , NOMBRE FROM METODO WHERE NOMBRE = @NOMBRE";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<Catalogos>> ObtenerTipoEsclerosante()
        {
            string sql = @"SELECT ID_TIPO_ESCLEROSANTE ID , NOMBRE FROM TIPO_ESCLEROSANTE";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerTipoEsclerosanteByName(string nombre)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOMBRE", nombre);

            string sql = @"SELECT ID_TIPO_ESCLEROSANTE ID , NOMBRE FROM TIPO_ESCLEROSANTE WHERE NOMBRE = @NOMBRE";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<Catalogos>> ObtenerEspecialidad()
        {
            string sql = @"SELECT ID_ESPECIALIDAD ID , NOMBRE FROM ESPECIALIDAD";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerEspecialidadByName(string nombre)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOMBRE", nombre);

            string sql = @"SELECT ID_ESPECIALIDAD ID , NOMBRE FROM ESPECIALIDAD WHERE NOMBRE = @NOMBRE";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<List<Catalogos>> ObtenerFirmas()
        {
            string sql = @"select id_usuario ID, firma NOMBRE from 
                            usuario inner join rol on usuario.id_rol = rol.id_rol where rol.nombre = 'DOCTOR'";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<Catalogos>> ObtenerLocalidades()
        {
            string sql = @"select id_localidad ID, nombre NOMBRE from localidad";
            Consultas.clsQueryAsyncConn<Catalogos> objQuery = new Consultas.clsQueryAsyncConn<Catalogos>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<LocalidadModelo>>ObtenerLocalidad()
        {
            string sql = @"SELECT * FROM LOCALIDAD";
            Consultas.clsQueryAsyncConn<LocalidadModelo> objQuery = new Consultas.clsQueryAsyncConn<LocalidadModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<LocalidadModelo>> ObtenerLocalidadByName(string nombre)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOMBRE", nombre);

            string sql = @"select * from localidad WHERE NOMBRE = @NOMBRE";
            Consultas.clsQueryAsyncConn<LocalidadModelo> objQuery = new Consultas.clsQueryAsyncConn<LocalidadModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<ConsultaModelo>> ObtenerConsultaFlebologia(string idConsulta)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_CONSULTA_FLEBOLOGIA", idConsulta);

            string sql = @"SELECT CONSULTA.ID_CONSULTA_FLEBOLOGIA AS ID_CONSULTA, USUARIO.FIRMA AS FIRMA, CONSULTA.* FROM CONSULTA_FLEBOLOGIA CONSULTA, USUARIO USUARIO WHERE ID_CONSULTA_FLEBOLOGIA = @ID_CONSULTA_FLEBOLOGIA AND 
            USUARIO.ID_USUARIO = CONSULTA.ID_USUARIO";
            Consultas.clsQueryAsyncConn<ConsultaModelo> objQuery = new Consultas.clsQueryAsyncConn<ConsultaModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<List<DetalleConsultaModelo>> ObtenerDetalleConsulta(string idConsulta)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_CONSULTA", idConsulta);

            string sql = @"SELECT DETALLE.*, PROCEDIMIENTO.NOMBRE AS PROCEDIMIENTO, METODO.NOMBRE AS METODO, TIPO.NOMBRE AS TIPO_ESCLEROSANTE
                        FROM DETALLE_CONSULTA DETALLE, PROCEDIMIENTO PROCEDIMIENTO, TIPO_ESCLEROSANTE TIPO, METODO METODO
                        WHERE ID_CONSULTA = @ID_CONSULTA
                        AND DETALLE.ID_PROCEDIMIENTO = PROCEDIMIENTO.ID_PROCEDIMIENTO
                        AND DETALLE.ID_TIPO_ESCLEROSANTE = TIPO.ID_TIPO_ESCLEROSANTE
                        AND DETALLE.ID_METODO = METODO.ID_METODO;";
            Consultas.clsQueryAsyncConn<DetalleConsultaModelo> objQuery = new Consultas.clsQueryAsyncConn<DetalleConsultaModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<ConsultaModelo>> ObtenerConsultaGeneral(string idConsulta)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_CONSULTA", idConsulta);

            string sql = @"SELECT  USUARIO.FIRMA AS FIRMA, CONSULTA.* FROM CONSULTA_GENERAL CONSULTA, USUARIO USUARIO WHERE ID_CONSULTA = @ID_CONSULTA AND
            USUARIO.ID_USUARIO = CONSULTA.ID_USUARIO";
            Consultas.clsQueryAsyncConn<ConsultaModelo> objQuery = new Consultas.clsQueryAsyncConn<ConsultaModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<int> CrearConsultaFlebologia(ConsultaModelo consulta)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":OBSERVACIONES", consulta.observaciones);
            dynamicParameters.Add(":ESTADOOBSERV", consulta.estado_observaciones);
            dynamicParameters.Add(":PLAN", consulta.plan);
            dynamicParameters.Add(":ESTADOPLAN", consulta.estado_plan);
            dynamicParameters.Add(":DIAGNOSTICO", consulta.diagnostico);
            dynamicParameters.Add(":MOTIVO_CONSULTA", consulta.motivo_consulta);
            dynamicParameters.Add(":IDESPECIALIDAD", consulta.id_especialidad);
            dynamicParameters.Add(":IDPACIENTE", consulta.id_paciente);
            dynamicParameters.Add(":IDUSUARIO", consulta.id_usuario);
            dynamicParameters.Add(":ID_LOCALIDAD", consulta.id_localidad);

            string Query = @"INSERT INTO CONSULTA_FLEBOLOGIA (fecha, observaciones, estado_observaciones, ""plan"", estado_plan, diagnostico, motivo_consulta, id_especialidad, id_paciente, id_usuario, id_localidad) 
            VALUES (SYSDATETIME(), @OBSERVACIONES, @ESTADOOBSERV, @PLAN, @ESTADOPLAN, @DIAGNOSTICO, @MOTIVO_CONSULTA, @IDESPECIALIDAD, @IDPACIENTE, @IDUSUARIO, @ID_LOCALIDAD) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }

        public async Task<int> CrearConsultaGeneral (ConsultaModelo consulta)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":EVOLUCION", consulta.evolucion_clinica);
            dynamicParameters.Add(":ESTADO_EVOLUCION", consulta.estado_evolucion_clinica);
            dynamicParameters.Add(":PLANC", consulta.plan);
            dynamicParameters.Add(":ESTADOPLAN", consulta.estado_plan);
            dynamicParameters.Add(":DIAGNOSTICO", consulta.diagnostico);
            dynamicParameters.Add(":MOTIVO_CONSULTA", consulta.motivo_consulta);
            dynamicParameters.Add(":IDESPECIALIDAD", consulta.id_especialidad);
            dynamicParameters.Add(":IDPACIENTE", consulta.id_paciente);
            dynamicParameters.Add(":IDUSUARIO", consulta.id_usuario);
            dynamicParameters.Add(":ID_LOCALIDAD", consulta.id_localidad);

            string Query = @"INSERT INTO CONSULTA_GENERAL (fecha, evolucion_clinica, estado_evolucion_clinica,  ""plan"" , estado_plan, diagnostico, motivo_consulta, id_especialidad, id_paciente, id_usuario, id_localidad)
            VALUES (SYSDATETIME(), @EVOLUCION, @ESTADO_EVOLUCION, @PLANC, @ESTADOPLAN, @DIAGNOSTICO, @MOTIVO_CONSULTA, @IDESPECIALIDAD, @IDPACIENTE, @IDUSUARIO, @ID_LOCALIDAD) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }
        public async Task<int> CrearDetalleConsultaFlebologia(DetalleConsultaModelo detalle)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NUMERO", detalle.numero);
            dynamicParameters.Add(":CANTIDAD", detalle.cantidad);
            dynamicParameters.Add(":POLIDOCANOL", detalle.polidocanol);
            dynamicParameters.Add(":AREATOMICA", detalle.area_anatomica);
            dynamicParameters.Add(":IDCONSULTA", detalle.id_consulta);
            dynamicParameters.Add(":IDPROCEDIMIENTO", detalle.id_procedimiento);
            dynamicParameters.Add(":IDMETODO", detalle.id_metodo);
            dynamicParameters.Add(":IDTIPOESCLERO", detalle.id_tipo_esclerosante);
            

            string Query = @"INSERT INTO DETALLE_CONSULTA (numero, cantidad, polidocanol, area_anatomica, id_consulta, id_procedimiento, id_metodo, id_tipo_esclerosante) 
            VALUES (@NUMERO, @CANTIDAD, @POLIDOCANOL, @AREATOMICA, @IDCONSULTA, @IDPROCEDIMIENTO, @IDMETODO, @IDTIPOESCLERO) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<int> CrearLocalidad(string localidad)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NOMBRE", localidad);

            string Query = @"INSERT INTO LOCALIDAD (nombre) 
            VALUES (@NOMBRE) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }

        public async Task<int> CrearEspecialidad(string especialidad)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NOMBRE", especialidad);

            string Query = @"INSERT INTO ESPECIALIDAD (nombre) 
            VALUES (@NOMBRE) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }

        public async Task<int> CrearProcedimiento(string procedimiento)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NOMBRE", procedimiento);

            string Query = @"INSERT INTO PROCEDIMIENTO (nombre) 
            VALUES (@NOMBRE) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }

        public async Task<int> CrearMetodo(string metodo)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NOMBRE", metodo);

            string Query = @"INSERT INTO METODO (nombre) 
            VALUES (@NOMBRE) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }

        public async Task<int> CrearTipoEsclerosante(string tipoEsclerosante)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":NOMBRE", tipoEsclerosante);

            string Query = @"INSERT INTO TIPO_ESCLEROSANTE (nombre) 
            VALUES (@NOMBRE) SELECT SCOPE_IDENTITY()";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
            return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
        }
        public async Task<List<ConsultaResponse>> ObtenerConsultasCriterio(CriterioConsultaModelo criterio)
        {
            string wherecondition = @" WHERE 1=1 ";
            if (!criterio.fechaExacta.Trim().Equals(""))
            {
                wherecondition += " AND Convert(date, cf.fecha) = '" + criterio.fechaExacta.ToUpper() + "'";
            }
            if (!criterio.fechaInicial.Trim().Equals("") && !criterio.fechaFinal.Trim().Equals(""))
            {
                wherecondition += " AND (Convert(date,fecha) >= '" + criterio.fechaInicial + "' AND Convert(date,fecha) <= '" + criterio.fechaFinal + "') ";
            }
            if (!criterio.id_especialidad.Trim().Equals("0"))
            {
                wherecondition += " AND e.id_especialidad = " + criterio.id_especialidad + " ";
            }
            if (!criterio.id_localidad.Trim().Equals("0"))
            {
                wherecondition += " AND l.id_localidad = " + criterio.id_localidad + " ";
            }
            if (!criterio.id_usuario.Trim().Equals("0"))
            {
                wherecondition += " AND u.id_usuario = " + criterio.id_usuario + " ";
            }
            if (!criterio.evolucion.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(cf.evolucion_clinica) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.evolucion + "%' ";
            }
            if (!criterio.diagnostico.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(cf.diagnostico) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.diagnostico + "%' ";
            }
            if (!criterio.plan.Trim().Equals(""))
            {
                wherecondition += @" AND ""plan"" COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.plan + "%' ";
            }


            if (!criterio.paciente.Trim().Equals(""))
            {
                wherecondition += " AND (UPPER(p.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.paciente.ToUpper() + "%' " +
                                     "OR UPPER(p.apellido) COLLATE SQL_Latin1_General_Cp1_CI_AI like  '%" + criterio.paciente.ToUpper() + "%' " +
                                     "OR UPPER(p.id_paciente) COLLATE SQL_Latin1_General_Cp1_CI_AI like  '%" + criterio.paciente.ToUpper() + "%') ";
            }

            string sql = @"select top " + criterio.cantidad + @" * from (
                                select cf.id_consulta_flebologia id_consulta, CONVERT(char(10), cf.fecha,103) fecha,  cf.fecha fec,  
                                e.nombre especialidad, u.firma,CONCAT(CONCAT(p.nombre,' '), p.apellido) pacienteC, p.fecha_nacimiento, l.nombre localidad, p.id_paciente idPaciente, p.genero ,
                                cf.observaciones, cf.estado_observaciones estadoObservaciones, ""plan"", cf.estado_plan estadoPlan, cf.diagnostico
                                from consulta_flebologia cf 
                                inner join especialidad e on cf.id_especialidad = e.id_especialidad
                                inner join usuario u on cf.id_usuario = u.id_usuario
                                inner join localidad l on cf.id_localidad= l.id_localidad 
                                inner join paciente p on cf.id_paciente = p.id_paciente " +
                                wherecondition + @"
                            union all
                                select cf.id_consulta id_consulta, CONVERT(char(10), cf.fecha,103) fecha,  cf.fecha fec,  
                                e.nombre especialidad, u.firma,CONCAT(CONCAT(p.nombre,' '), p.apellido) pacienteC, p.fecha_nacimiento, l.nombre localidad, p.id_paciente idPaciente, p.genero ,
                                cf.evolucion_clinica observaciones, cf.estado_evolucion_clinica estadoObservaciones, ""plan"", cf.estado_plan estadoPlan, cf.diagnostico
                                from consulta_general cf 
                                inner join especialidad e on cf.id_especialidad = e.id_especialidad
                                inner join usuario u on cf.id_usuario = u.id_usuario
                                inner join localidad l on cf.id_localidad= l.id_localidad 
                                inner join paciente p on cf.id_paciente = p.id_paciente " +
                                wherecondition + @"
                        )
                        tabla order by fec desc";

            Consultas.clsQueryAsyncConn<ConsultaResponse> objQuery = new Consultas.clsQueryAsyncConn<ConsultaResponse>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<ConsultaResponse>> ObtenerConsultasCoincidencias(CriterioConsultaModelo criterio)
        {
            string wherecondition = @" where UPPER(e.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(u.firma) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(l.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(p.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(p.apellido) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(cf.fecha) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%' ";
            wherecondition += " OR UPPER(p.id_paciente) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%' ";
            wherecondition += @" OR ""plan"" COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%' ";
            wherecondition += " OR UPPER(cf.evolucion_clinica) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%' ";
            wherecondition += " OR UPPER(cf.diagnostico) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%' ";


            string sql = @"select top " + criterio.cantidad + @" * from (
                                select cf.id_consulta_flebologia id_consulta, CONVERT(char(10), cf.fecha,103) fecha,  cf.fecha fec,  
                                e.nombre especialidad, u.firma,CONCAT(CONCAT(p.nombre,' '), p.apellido) pacienteC, p.fecha_nacimiento, l.nombre localidad, p.id_paciente idPaciente, p.genero ,
                                cf.observaciones, cf.estado_observaciones estadoObservaciones, ""plan"", cf.estado_plan estadoPlan, cf.diagnostico
                                from consulta_flebologia cf 
                                inner join especialidad e on cf.id_especialidad = e.id_especialidad
                                inner join usuario u on cf.id_usuario = u.id_usuario
                                inner join localidad l on cf.id_localidad= l.id_localidad 
                                inner join paciente p on cf.id_paciente = p.id_paciente" + 
                                wherecondition+ @"
                            union all
                                select cf.id_consulta id_consulta, CONVERT(char(10), cf.fecha,103) fecha,  cf.fecha fec,  
                                e.nombre especialidad, u.firma,CONCAT(CONCAT(p.nombre,' '), p.apellido) pacienteC, p.fecha_nacimiento, l.nombre localidad, p.id_paciente idPaciente, p.genero ,
                                cf.evolucion_clinica observaciones, cf.estado_evolucion_clinica estadoObservaciones, ""plan"", cf.estado_plan estadoPlan, cf.diagnostico
                                from consulta_general cf 
                                inner join especialidad e on cf.id_especialidad = e.id_especialidad
                                inner join usuario u on cf.id_usuario = u.id_usuario
                                inner join localidad l on cf.id_localidad= l.id_localidad 
                                inner join paciente p on cf.id_paciente = p.id_paciente" +
                                wherecondition + @"
                        )
                        tabla order by fec desc
                        ";
            Consultas.clsQueryAsyncConn<ConsultaResponse> objQuery = new Consultas.clsQueryAsyncConn<ConsultaResponse>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }


        public async Task<int> EditarConsultaFlebologia(ConsultaModelo consulta)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":OBSERVACIONES", consulta.observaciones);
            dynamicParameters.Add(":ESTADOOBSERV", consulta.estado_observaciones);
            dynamicParameters.Add(":PLAN", consulta.plan);
            dynamicParameters.Add(":ESTADOPLAN", consulta.estado_plan);
            dynamicParameters.Add(":DIAGNOSTICO", consulta.diagnostico);
            dynamicParameters.Add(":MOTIVO_CONSULTA", consulta.motivo_consulta);
            //dynamicParameters.Add(":IDESPECIALIDAD", consulta.id_especialidad);
            //dynamicParameters.Add(":IDPACIENTE", consulta.id_paciente);
            //dynamicParameters.Add(":IDUSUARIO", consulta.id_usuario);
            dynamicParameters.Add(":ID_LOCALIDAD", consulta.id_localidad);
            dynamicParameters.Add(":ID_CONSULTA_FLEBOLOGIA", consulta.id_consulta);

            string Query = @"UPDATE CONSULTA_FLEBOLOGIA SET observaciones = @OBSERVACIONES, estado_observaciones = @ESTADOOBSERV, ""plan"" = @PLAN, estado_plan = @ESTADOPLAN, diagnostico = @DIAGNOSTICO, motivo_consulta = @MOTIVO_CONSULTA, id_localidad = @ID_LOCALIDAD
            WHERE ID_CONSULTA_FLEBOLOGIA = @ID_CONSULTA_FLEBOLOGIA";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return  await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<int> EditarConsultaGeneral(ConsultaModelo consulta)
        {
            var dynamicParameters = new DynamicParameters();
            // dynamicParameters.Add(":FECHA", consulta.fecha);
            dynamicParameters.Add(":EVOLUCION_CLINICA", consulta.evolucion_clinica);
            dynamicParameters.Add(":ESTADO_EVOLUCION", consulta.estado_evolucion_clinica);
            dynamicParameters.Add(":PLAN", consulta.plan);
            dynamicParameters.Add(":ESTADOPLAN", consulta.estado_plan);
            dynamicParameters.Add(":DIAGNOSTICO", consulta.diagnostico);
            dynamicParameters.Add(":MOTIVO_CONSULTA", consulta.motivo_consulta);
            //dynamicParameters.Add(":IDESPECIALIDAD", consulta.id_especialidad);
            //dynamicParameters.Add(":IDPACIENTE", consulta.id_paciente);
            //dynamicParameters.Add(":IDUSUARIO", consulta.id_usuario);
            dynamicParameters.Add(":ID_LOCALIDAD", consulta.id_localidad);
            dynamicParameters.Add(":ID_CONSULTA", consulta.id_consulta);

            string Query = @"UPDATE CONSULTA_GENERAL SET evolucion_clinica = @EVOLUCION_CLINICA, estado_evolucion_clinica = @ESTADO_EVOLUCION, ""plan"" = @PLAN, estado_plan = @ESTADOPLAN, diagnostico = @DIAGNOSTICO, motivo_consulta = @MOTIVO_CONSULTA, id_localidad = @ID_LOCALIDAD
            WHERE ID_CONSULTA = @ID_CONSULTA";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<List<DetalleConsultaModelo>> EliminarTratamientos(string idConsulta)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_CONSULTA", idConsulta);

            string sql = @"DELETE FROM DETALLE_CONSULTA WHERE ID_CONSULTA = @ID_CONSULTA";
            Consultas.clsQueryAsyncConn<DetalleConsultaModelo> objQuery = new Consultas.clsQueryAsyncConn<DetalleConsultaModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
    }
}

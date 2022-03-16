using Core.Modelos.Entorno;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    public class Cita : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Cita(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Cita(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn; this.transaction = transaction;
        }
        public async Task<int> CrearCita(CitaModelo cita)
        {
            try {
                var dynamicParameters = new DynamicParameters();
                // dynamicParameters.Add(":FECHA", consulta.fecha);
                dynamicParameters.Add(":ID_PACIENTE", cita.id_paciente);
                dynamicParameters.Add(":ID_USUARIO", cita.id_usuario);
                dynamicParameters.Add(":ID_ESPECIALIDAD", cita.id_especialidad);
                dynamicParameters.Add(":FECHA", cita.fecha);
                dynamicParameters.Add(":HORA", cita.hora);
                dynamicParameters.Add(":NOTA", cita.nota);
                dynamicParameters.Add(":ENVIARMSJ", cita.enviarMsj);
                dynamicParameters.Add(":DIASANTES", cita.diasAntes);
                //dynamicParameters.Add(":ID_CONSULTA", cita.id_consulta);
                //dynamicParameters.Add(":ID_CONSULTA_FLEBOLOGIA", cita.id_consulta_flebologia);

                string Query = @"INSERT INTO CITA (ID_PACIENTE, ID_USUARIO, ID_ESPECIALIDAD, FECHA, HORA, NOTA, ENVIARMSJ, DIASANTES) 
            VALUES (@ID_PACIENTE, @ID_USUARIO, @ID_ESPECIALIDAD, @FECHA, @HORA, @NOTA, @ENVIARMSJ, @DIASANTES) SELECT SCOPE_IDENTITY()";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
                return lista.AsList().Count > 0 ? lista.AsList()[0] : -1;
            } catch (Exception e) {
                return -1;
            }
            
        }
        public async Task<int> EliminarCita(CitaModelo cita)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                // dynamicParameters.Add(":FECHA", consulta.fecha);
                dynamicParameters.Add(":ID_CITA", cita.id_cita);

                string Query = @"DELETE FROM CITA WHERE ID_CITA= @ID_CITA";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                return await objQuery.ExecuteAsync(Query, dynamicParameters);
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        public async Task<int> ModificarCita(CitaModelo cita)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                // dynamicParameters.Add(":FECHA", consulta.fecha);
                dynamicParameters.Add(":ID_CITA", cita.id_cita);
                dynamicParameters.Add(":ID_USUARIO", cita.id_usuario);
                dynamicParameters.Add(":ID_ESPECIALIDAD", cita.id_especialidad);
                dynamicParameters.Add(":FECHA", cita.fecha);
                dynamicParameters.Add(":HORA", cita.hora);
                dynamicParameters.Add(":NOTA", cita.nota);
                dynamicParameters.Add(":ENVIARMSJ", cita.enviarMsj);
                dynamicParameters.Add(":DIASANTES", cita.diasAntes);

                string Query = @"UPDATE CITA SET ID_USUARIO=@ID_USUARIO, id_especialidad=@ID_ESPECIALIDAD,
                                    FECHA=@FECHA, HORA=@HORA, NOTA=@NOTA, ENVIARMSJ=@ENVIARMSJ, DIASANTES=@DIASANTES 
                                    WHERE ID_CITA=@ID_CITA";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                return await objQuery.ExecuteAsync(Query, dynamicParameters);
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        public async Task<int> LogMensaje(Recordatorio rec, string code, string mensaje, string statusM)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                // dynamicParameters.Add(":FECHA", consulta.fecha);
                dynamicParameters.Add(":TELEFONO", rec.telefono);
                dynamicParameters.Add(":STATUSM", statusM);
                dynamicParameters.Add(":CODE", code);
                dynamicParameters.Add(":MENSAJE", mensaje);
                dynamicParameters.Add(":IDPACIENTE", rec.idPaciente);
                dynamicParameters.Add(":IDCITA", Convert.ToInt32(rec.numCita));

                string Query = @"INSERT INTO BitacoraMensajes (FECHA, TELEFONO, STATUSM, CODE, MENSAJE, IDPACIENTE, IDCITA ) 
            VALUES (SYSDATETIME(), @TELEFONO, @STATUSM, @CODE, @MENSAJE, @IDPACIENTE, @IDCITA)";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
                return lista.AsList().Count > 0 ? 1 : -1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        public async Task<int> LogMensajeManual(string code, string mensaje, string statusM, string telefono, string idPaciente, string contacto, string texto)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add(":TELEFONO", telefono);
                dynamicParameters.Add(":STATUSM", statusM);
                dynamicParameters.Add(":CODE", code);
                dynamicParameters.Add(":MENSAJE", mensaje);
                dynamicParameters.Add(":IDPACIENTE", idPaciente);
                dynamicParameters.Add(":CONTACTO", contacto);
                dynamicParameters.Add(":TEXTOENVIADO", texto);
                

                string Query = @"INSERT INTO BitacoraMensajesManuales (FECHA, TELEFONO, STATUSM, CODE, MENSAJE, IDPACIENTE, CONTACTO, TEXTOENVIADO ) 
            VALUES (SYSDATETIME(), @TELEFONO, @STATUSM, @CODE, @MENSAJE, @IDPACIENTE, @CONTACTO, @TEXTOENVIADO)";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                var lista = await objQuery.QuerySelectAsync(Query, dynamicParameters);
                return lista.AsList().Count > 0 ? 1 : -1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        public async Task<List<CitaCalendario>> ObtenerCitasPorFecha(string usuario, string month, string year)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":USUARIO", usuario);
            dynamicParameters.Add(":MONTH", month);
            dynamicParameters.Add(":YEAR", year);

            string sql = @"SELECT FECHA, count(*) numCitas from CITA group by FECHA, ID_USUARIO
                           HAVING MONTH(FECHA) = @MONTH AND YEAR(FECHA)= @YEAR AND ID_USUARIO= @USUARIO";
            Consultas.clsQueryAsyncConn<CitaCalendario> objQuery = new Consultas.clsQueryAsyncConn<CitaCalendario>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<CitaCalendario>> ObtenerCitasPorFecha(string month, string year)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":MONTH", month);
            dynamicParameters.Add(":YEAR", year);

            string sql = @"SELECT FECHA, count(*) numCitas from CITA group by FECHA, ID_USUARIO
                           HAVING MONTH(FECHA) = @MONTH AND YEAR(FECHA)= @YEAR";
            Consultas.clsQueryAsyncConn<CitaCalendario> objQuery = new Consultas.clsQueryAsyncConn<CitaCalendario>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<TipUsuario>> GetTipoUsuario(string usuario)
        {
            var dynamicParameters = new DynamicParameters();


            string sql = @"select rol.nombre from usuario inner join rol on 
                            usuario.id_rol = rol.id_rol
                            where usuario.id_usuario= " + usuario;
            Consultas.clsQueryAsyncConn<TipUsuario> objQuery = new Consultas.clsQueryAsyncConn<TipUsuario>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<ConfiguracionColores>> ObtenerConfColores()
        {
            var dynamicParameters = new DynamicParameters();
            

            string sql = @"select citasRojo, citasAmarillo, citasVerde from ConfiguracionProcMsjs";
            Consultas.clsQueryAsyncConn<ConfiguracionColores> objQuery = new Consultas.clsQueryAsyncConn<ConfiguracionColores>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<CitaProgramada>> ObtenerPacientesCitados(string fecha)
        {
            try {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add(":FECHA", fecha);

                string sql = @"select paciente.id_paciente CODIGO, CONCAT(paciente.nombre, ' ', paciente.apellido) nombrecompleto,
                            paciente.cui, paciente.fecha_nacimiento FECHANAC, paciente.genero, FORMAT (paciente.fecha_ultima_consulta, 'dd/MM/yyyy ') FECHAULTIMACONSULTA,
                            cita.hora, cita.nota, cita.id_usuario usuario, usuario.firma , cita.id_cita citacodigo, cita.id_especialidad idespecialidad,
                            cita.enviarMsj, cita.diasAntes
                            from cita inner join paciente on cita.id_paciente=paciente.id_paciente 
                                      inner join usuario on usuario.id_usuario = cita.id_usuario
                            where Convert(date, cita.fecha)= '" + fecha + "'";
                Consultas.clsQueryAsyncConn<CitaProgramada> objQuery = new Consultas.clsQueryAsyncConn<CitaProgramada>(_conn, transaction);
                var existe = await objQuery.QuerySelectAsync(sql);
                return existe.AsList();
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        public async Task<List<Recordatorio>> ObtenerCitasProgramadas()
        {
            try
            {
                var dynamicParameters = new DynamicParameters();

                string sql = @"SELECT CITA.id_cita numCita, PACIENTE.id_paciente idPaciente, CONCAT(paciente.nombre, ' ', paciente.apellido) nombrePaciente,
                                paciente.nombre nombres, paciente.apellido apellidos,
                                telefono.telefono, telefono.nota contacto, usuario.firma, FORMAT (cita.fecha,'dd/MM/yyyy') fecha, cita.hora, cita.nota notaCita,
                                cita.diasAntes
                                from CITA 
                                inner join paciente on Cita.id_paciente = Paciente.id_paciente
                                inner join telefono on Paciente.id_paciente = telefono.id_paciente
                                inner join usuario on cita.id_usuario = usuario.id_usuario
                                WHERE FECHA = FORMAT (DATEADD(hh, -6, getUTCDate()+ diasAntes), 'yyyy-MM-dd')
                                AND cita.enviarMsj='S'
                                AND telefono.enviarMsj = 'S'";

                Consultas.clsQueryAsyncConn<Recordatorio> objQuery = new Consultas.clsQueryAsyncConn<Recordatorio>(_conn, transaction);
                var existe = await objQuery.QuerySelectAsync(sql);
                return existe.AsList();
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}

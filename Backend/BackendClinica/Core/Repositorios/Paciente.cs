using Core.Modelos.Entorno;
using Core.Utils.Security;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    class Paciente : Consultas.clsQuery
    {
        public IDbConnection _conn;
        public IDbTransaction transaction;
        public Paciente(IDbConnection conn) : base(conn)
        {
            this._conn = conn;
        }
        public Paciente(IDbConnection conn, IDbTransaction transaction) : base(conn)
        {
            this._conn = conn;            this.transaction = transaction;

        }
        public async Task<int> BorrarNotas(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string Query = @"DELETE FROM NOTAMEDICA WHERE IDPACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
        public async Task<int> InsertarNota(NotaMedica nota, int orden)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", nota.idPaciente);
            dynamicParameters.Add(":NOTA", nota.nota);
            dynamicParameters.Add(":FECHA", nota.fecha);
            dynamicParameters.Add(":ORDEN", orden);

            string Query = @"INSERT INTO NOTAMEDICA (IDPACIENTE, NOTA, FECHA, ORDEN) VALUES (@ID_PACIENTE, @NOTA, @FECHA, @ORDEN)";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
        public async Task<int> CrearPaciente(PacienteModelo paciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", paciente.id_paciente);
            dynamicParameters.Add(":NOMBRE", paciente.nombre != null ? paciente.nombre.ToUpper() : paciente.nombre);
            dynamicParameters.Add(":APELLIDO", paciente.apellido != null ? paciente.apellido.ToUpper() : paciente.apellido);
            dynamicParameters.Add(":ALIAS", paciente.alias != null ? paciente.alias.ToUpper() : paciente.alias);
            dynamicParameters.Add(":CUI", paciente.cui != null ? paciente.cui.ToUpper() : paciente.cui);
            dynamicParameters.Add(":GENERO", paciente.genero);
            dynamicParameters.Add(":FECHA_NACIMIENTO", paciente.fecha_nacimiento);
            dynamicParameters.Add(":DIRECCION", paciente.direccion != null ? paciente.direccion.ToUpper() : paciente.direccion);
            dynamicParameters.Add(":NOTA_IMPORTANTE", paciente.nota_importante != null ? paciente.nota_importante.ToUpper() : paciente.nota_importante);
            dynamicParameters.Add(":ID_DEPARTAMENTO", paciente.id_departamento);
            dynamicParameters.Add(":ID_USUARIO", paciente.id_usuario);

            if (paciente.fecha_primer_consulta.Trim() == "" && paciente.fecha_ultima_consulta.Trim() == "")
            {
                string Query = @"INSERT INTO PACIENTE (ID_PACIENTE, NOMBRE, APELLIDO, ALIAS, CUI, GENERO, FECHA_NACIMIENTO, DIRECCION, NOTA_IMPORTANTE, FECHA_PRIMER_CONSULTA, FECHA_ULTIMA_CONSULTA, ESTADO, ID_DEPARTAMENTO, ID_USUARIO) 
                VALUES (@ID_PACIENTE, @NOMBRE, @APELLIDO, @ALIAS, @CUI, @GENERO, @FECHA_NACIMIENTO, @DIRECCION, @NOTA_IMPORTANTE, SYSDATETIME(), SYSDATETIME(), 'A', @ID_DEPARTAMENTO, @ID_USUARIO)";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                return await objQuery.ExecuteAsync(Query, dynamicParameters);
            }
            else {
                dynamicParameters.Add(":FECHA_PRIMER_CONSULTA", paciente.fecha_primer_consulta);
                dynamicParameters.Add(":FECHA_ULTIMA_CONSULTA", paciente.fecha_ultima_consulta);

                string Query = @"INSERT INTO PACIENTE (ID_PACIENTE, NOMBRE, APELLIDO, ALIAS, CUI, GENERO, FECHA_NACIMIENTO, DIRECCION, NOTA_IMPORTANTE, FECHA_PRIMER_CONSULTA, FECHA_ULTIMA_CONSULTA, ESTADO, ID_DEPARTAMENTO, ID_USUARIO) 
                VALUES (@ID_PACIENTE, @NOMBRE, @APELLIDO, @ALIAS, @CUI, @GENERO, @FECHA_NACIMIENTO, @DIRECCION, @NOTA_IMPORTANTE, @FECHA_PRIMER_CONSULTA, @FECHA_ULTIMA_CONSULTA, 'A', @ID_DEPARTAMENTO, @ID_USUARIO)";
                Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
                return await objQuery.ExecuteAsync(Query, dynamicParameters);
            }
            
        }


        public async Task<int> EliminarPaciente(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string Query = @"UPDATE PACIENTE SET ESTADO='E' WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
        public async Task<List<PacienteResponse>> ObtenerPacientesCriterio(CriterioPacienteModelo criterio)
        {
            string wherecondition = @"WHERE paciente.estado='A' ";
            if (!criterio.codigo.Trim().Equals("")) {
                wherecondition += " AND UPPER(paciente.id_paciente) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.codigo.ToUpper() + "%'";
            }
            if (!criterio.nombre.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(paciente.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.nombre.ToUpper() + "%'";
            }
            if (!criterio.apellido.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(paciente.apellido) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.apellido.ToUpper() + "%'";
            }
            if (!criterio.cui.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(paciente.cui) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.cui.ToUpper() + "%'";
            }
            if (!criterio.fechaNacimiento.Trim().Equals(""))
            {
                wherecondition += " AND Convert(date, paciente.fecha_nacimiento) = '" + criterio.fechaNacimiento.ToUpper() + "'";
            }
            if (!criterio.genero.Trim().Equals(""))
            {
                wherecondition += " AND UPPER(paciente.genero) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.genero.ToUpper() + "%'";
            }
            if (!criterio.id_departamento.Trim().Equals("0"))
            {
                wherecondition += " AND paciente.id_departamento = " + criterio.id_departamento.ToUpper() + "";
            }
            if (!criterio.id_pais.Trim().Equals("0"))
            {
                wherecondition += " AND pais.id_pais = " + criterio.id_pais.ToUpper() + "";
            }
            string sql = @"select top "+ criterio.cantidad + @" paciente.id_paciente codigo, CONCAT(paciente.nombre, ' ', paciente.apellido) nombrecompleto, 
                            paciente.cui, paciente.fecha_nacimiento fechanac, pais.nombre pais, departamento.nombre depto, paciente.genero, paciente.direccion, paciente.alias aliasPac
                             from paciente inner join departamento on paciente.id_departamento= departamento.id_departamento
                             inner join pais  on departamento.id_pais = pais.id_pais " + wherecondition;
            Consultas.clsQueryAsyncConn<PacienteResponse> objQuery = new Consultas.clsQueryAsyncConn<PacienteResponse>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<PacienteResponse>> ObtenerPacientesCoincidencias(CriterioPacienteModelo criterio)
        {
            string wherecondition = @"WHERE paciente.estado='A' AND ( ";
            wherecondition += " UPPER(paciente.id_paciente) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(paciente.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(paciente.apellido) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(paciente.cui) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(paciente.fecha_nacimiento) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            // wherecondition += " OR UPPER(paciente.genero) like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(departamento.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            wherecondition += " OR UPPER(pais.nombre) COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + criterio.textoGeneral.ToUpper() + "%'";
            

            string sql = @"select top " + criterio.cantidad + @" paciente.id_paciente codigo, CONCAT(paciente.nombre, ' ', paciente.apellido) nombrecompleto, 
                            paciente.cui, paciente.fecha_nacimiento fechanac, pais.nombre pais, departamento.nombre depto, paciente.genero, paciente.direccion, paciente.alias aliasPac
                             from paciente inner join departamento on paciente.id_departamento= departamento.id_departamento
                             inner join pais  on departamento.id_pais = pais.id_pais " + wherecondition + ")";
            Consultas.clsQueryAsyncConn<PacienteResponse> objQuery = new Consultas.clsQueryAsyncConn<PacienteResponse>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }
        public async Task<List<PacienteModelo>> ObtenerUltimoIDPaciente()
        {
            string sql = @"SELECT MAX(ID_PACIENTE) AS ID_PACIENTE FROM PACIENTE";
            Consultas.clsQueryAsyncConn<PacienteModelo> objQuery = new Consultas.clsQueryAsyncConn<PacienteModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql);
            return existe.AsList();
        }

        public async Task<List<PacienteModelo>> ObtenerPaciente(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string sql = @"SELECT * FROM PACIENTE WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<PacienteModelo> objQuery = new Consultas.clsQueryAsyncConn<PacienteModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }
        public async Task<List<NotaMedica>> ObtenerNotasMedicas(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string sql = @"SELECT * FROM NOTAMEDICA WHERE IDPACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<NotaMedica> objQuery = new Consultas.clsQueryAsyncConn<NotaMedica>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<int> CrearHistorial(PacienteModelo paciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", paciente.id_paciente);
            dynamicParameters.Add(":MOTIVO_CONSULTA", paciente.motivo_consulta);
            dynamicParameters.Add(":HISTORIA_ENF_ACTUAL", paciente.historia_enf_actual);
            dynamicParameters.Add(":ANTECEDENTE", paciente.antecedente);
            dynamicParameters.Add(":ALERGIA", paciente.alergia);
            dynamicParameters.Add(":EXAMEN_FISICO", paciente.examen_fisico);
            dynamicParameters.Add(":HISTORIAL_PREVIO", paciente.historial_previo);

            string Query = @"UPDATE PACIENTE SET MOTIVO_CONSULTA = @MOTIVO_CONSULTA, HISTORIA_ENF_ACTUAL = @HISTORIA_ENF_ACTUAL, 
            ANTECEDENTE = @ANTECEDENTE, ALERGIA = @ALERGIA, EXAMEN_FISICO = @EXAMEN_FISICO, HISTORIAL_PREVIO = @HISTORIAL_PREVIO WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<int> ActualizarFechaUltimaConsulta(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);
            
            string Query = @"UPDATE PACIENTE SET FECHA_ULTIMA_CONSULTA = SYSDATETIME() WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<int> CrearTelefono(TelefonoModelo telefono)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":NOTA", telefono.nota.ToUpper());
            dynamicParameters.Add(":TELEFONO", telefono.telefono);
            dynamicParameters.Add(":ID_PACIENTE", telefono.id_paciente);
            dynamicParameters.Add(":ENVIARMSJ", telefono.enviarMsj);

            string Query = @"INSERT INTO TELEFONO (NOTA, TELEFONO, ID_PACIENTE, ENVIARMSJ)  VALUES  (@NOTA, @TELEFONO, @ID_PACIENTE, @ENVIARMSJ)";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }

        public async Task<List<TelefonoModelo>> ObtenerTelefonos(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string sql = @"SELECT * FROM TELEFONO WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<TelefonoModelo> objQuery = new Consultas.clsQueryAsyncConn<TelefonoModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<List<TelefonoModelo>> EliminarTelefonos(string idPaciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", idPaciente);

            string sql = @"DELETE FROM TELEFONO WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<TelefonoModelo> objQuery = new Consultas.clsQueryAsyncConn<TelefonoModelo>(_conn, transaction);
            var existe = await objQuery.QuerySelectAsync(sql, dynamicParameters);
            return existe.AsList();
        }

        public async Task<int> EditarPaciente(PacienteModelo paciente)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(":ID_PACIENTE", paciente.id_paciente);

            dynamicParameters.Add(":NOMBRE", paciente.nombre != null ? paciente.nombre.ToUpper() : paciente.nombre);
            dynamicParameters.Add(":APELLIDO", paciente.apellido != null ? paciente.apellido.ToUpper() : paciente.apellido);
            dynamicParameters.Add(":ALIAS", paciente.alias != null ? paciente.alias.ToUpper() : paciente.alias);
            dynamicParameters.Add(":CUI", paciente.cui != null ? paciente.cui.ToUpper() : paciente.cui);
            dynamicParameters.Add(":GENERO", paciente.genero);
            dynamicParameters.Add(":FECHA_NACIMIENTO", paciente.fecha_nacimiento);
            dynamicParameters.Add(":DIRECCION", paciente.direccion != null ? paciente.direccion.ToUpper() : paciente.direccion);
            dynamicParameters.Add(":NOTA_IMPORTANTE", paciente.nota_importante != null ? paciente.nota_importante.ToUpper() : paciente.nota_importante);
            dynamicParameters.Add(":ID_DEPARTAMENTO", paciente.id_departamento);


            string Query = @"UPDATE PACIENTE SET NOMBRE = @NOMBRE, APELLIDO = @APELLIDO, ALIAS = @ALIAS, 
            CUI = @CUI, GENERO = @GENERO, FECHA_NACIMIENTO = @FECHA_NACIMIENTO,
            DIRECCION = @DIRECCION, NOTA_IMPORTANTE = @NOTA_IMPORTANTE, ID_DEPARTAMENTO = @ID_DEPARTAMENTO  
            WHERE ID_PACIENTE = @ID_PACIENTE";
            Consultas.clsQueryAsyncConn<int> objQuery = new Consultas.clsQueryAsyncConn<int>(_conn, transaction);
            return await objQuery.ExecuteAsync(Query, dynamicParameters);
        }
    }
}

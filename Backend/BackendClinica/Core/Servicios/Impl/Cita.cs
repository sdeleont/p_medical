using Core.Modelos.Entorno;
using Core.Servicios.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Impl
{
    public class Cita: ICita
    {
        public Configuracion conf;
        public Cita(Configuracion conf)
        {
            this.conf = conf;
        }
        public async Task<ResponseServer> CrearCita(CitaModelo cita)
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
                            Repositorios.Cita repo = new Repositorios.Cita(_conn, transaction);
                            int codigo = await repo.CrearCita(cita);
                            if (codigo > 0) {
                                response.status = "OK";
                                response.mensaje = "Se registro la cita correctamente";
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
        public async Task<ResponseServer> EliminarCita(CitaModelo cita)
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
                            Repositorios.Cita repo = new Repositorios.Cita(_conn, transaction);
                            await repo.EliminarCita(cita);
                            response.status = "OK";
                            response.mensaje = "Se elimino la cita correctamente";
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
        public async Task<ResponseServer> ModificarCita(CitaModelo cita)
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
                            Repositorios.Cita repo = new Repositorios.Cita(_conn, transaction);
                            await repo.ModificarCita(cita);
                            response.status = "OK";
                            response.mensaje = "Se modifico la cita correctamente";
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
        public async Task<List<CitaCalendario>> ObtenerCalendario(string usuario, string month, string year)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                     try {
                            List<CitaCalendario> citasAlDia = new List<CitaCalendario>();
                            Repositorios.Cita repo = new Repositorios.Cita(_conn);
                            List<TipUsuario>  tUsuario = await repo.GetTipoUsuario(usuario);
                            if (tUsuario[0].nombre.Equals("SECRETARIA"))
                            {
                                citasAlDia = await repo.ObtenerCitasPorFecha(month, year);
                            }
                            else {
                                citasAlDia = await repo.ObtenerCitasPorFecha(usuario, month, year);
                            }
                            // citasAlDia = await repo.ObtenerCitasPorFecha(usuario, month, year);
                            List<ConfiguracionColores> config = await repo.ObtenerConfColores();
                            foreach (CitaCalendario citaCalendario in citasAlDia) {
                                int numCitas = Convert.ToInt32(citaCalendario.numCitas);

                                if (numCitas > -1 && numCitas <= Convert.ToInt32(config[0].citasVerde)) {
                                    citaCalendario.status = "greenStatus";
                                }
                                if (numCitas > Convert.ToInt32(config[0].citasVerde) && numCitas <= Convert.ToInt32(config[0].citasAmarillo))
                                {
                                    citaCalendario.status = "yellowStatus";
                                }
                                if (numCitas > Convert.ToInt32(config[0].citasAmarillo) && numCitas <= Convert.ToInt32(config[0].citasRojo))
                                {
                                    citaCalendario.status = "redStatus";
                                }
                            }
                            _conn.Close();
                            return citasAlDia;
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
        public async Task<List<CitaProgramada>> ObtenerPacientesCitados(string fecha)
        {
            try
            {
                using (IDbConnection _conn = new SqlConnection(conf.SQLServerPool))
                {
                    try
                    {
                        List<CitaProgramada> citasProgramadas = new List<CitaProgramada>();
                        Repositorios.Cita repo = new Repositorios.Cita(_conn);
                        citasProgramadas = await repo.ObtenerPacientesCitados(fecha);
                        _conn.Close();
                        return citasProgramadas;
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
        public async Task<int> EnviarMensajeTexto(Recordatorio recordatorio, Repositorios.Cita repo) {
            try{
                string[] nombres = recordatorio.nombres.Split(' ');
                string[] apellidos = recordatorio.apellidos.Split(' ');
                string paciente = "";
                if (nombres.Length > 0)
                {
                    paciente += nombres[0] + " ";
                }
                if (apellidos.Length > 0)
                {
                    paciente += apellidos[0] + " ";
                }


                string mensaje = "Estimado(a) " + paciente + "le saludamos de Clinica Flores para recordarle que su proxima cita esta programada para el " + recordatorio.fecha + " a las " + recordatorio.hora;
                string sender = "C. Flores";
                string numero = "502" + recordatorio.telefono;
                var client = new RestClient("https://api.labsmobile.com/json/send");
                client.Authenticator = new HttpBasicAuthenticator("secret email", "secret");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\"message\":\"" + mensaje + "\", \"tpoa\":\"" + sender + "\",\"recipient\":[{\"msisdn\":\"" + numero + "\"}]}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                dynamic api = JObject.Parse(response.Content);
                var code = api.code;
                var message = api.message;

                await repo.LogMensaje(recordatorio, Convert.ToString(code.Value), Convert.ToString(message.Value) , response.StatusCode.ToString());

            } catch (Exception ex) {
                string error = ex.ToString();
                try {
                    await repo.LogMensaje(recordatorio, "-1", error, "-1");
                } catch (Exception exe) { 
                }
            }
            
            return 0;
        }
        public async Task<int> EnvioDeRecordatorios()
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
                            string texto = "Citas encontradas ";
                            Repositorios.Cita repo = new Repositorios.Cita(_conn, transaction);
                            List<Recordatorio> recordatorios = await repo.ObtenerCitasProgramadas();
                            foreach (Recordatorio recordatorio in recordatorios) {
                                // validamos telefono
                                if (recordatorio.telefono != null && recordatorio.telefono.Trim() != "" && recordatorio.telefono.Trim().Length == 8) {
                                    try {
                                        await EnviarMensajeTexto(recordatorio, repo); //solo esta linea se descomenta
                                    } catch (Exception ex) {

                                    }
                                }
                                //texto += "Cita: " + recordatorio.numCita + ", Paciente: " + recordatorio.idPaciente + ", Nombre: " + recordatorio.nombrePaciente + " ";
                                //texto += "Telefono: " + recordatorio.telefono + ", Contacto: " + recordatorio.contacto + ", Firma: " + recordatorio.firma + " ";
                                //texto += "Fecha: " + recordatorio.fecha + ", Hora: " + recordatorio.hora + ", Nota: " + recordatorio.notaCita + ", DiasAnticipacion:" + recordatorio.diasAntes + "\n";
                            }
                            texto += "" + recordatorios.Count;
                            transaction.Commit();
                            _conn.Close();
                            // ISendEmail email = new SendEmail();
                            // await email.SendTest(texto);
                            return 0;
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
        public async Task<ResponseServer> EnviarMSJ(MensajeTextoModelo msj)
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
                            Repositorios.Cita repo = new Repositorios.Cita(_conn, transaction);
                            if (msj.telefono != null && msj.telefono.Trim() != "" && msj.telefono.Trim().Length == 8)
                            {
                                try
                                {
                                    int respuesta = await EnviarMsj(msj.telefono, msj.texto, repo, msj.idPaciente, msj.contacto);
                                    response.status = "OK";
                                    response.mensaje = "Se envio el mensaje correctamente";
                                }
                                catch (Exception ex)
                                {
                                    response.mensaje = "Error en el servidor";
                                }
                            }
                            else {
                                response.mensaje = "Numero de telefono con formato invalido";
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
        public async Task<int> EnviarMsj(string telefono, string texto, Repositorios.Cita repo, string idPaciente, string contacto)
        {
            try
            {
                
                string mensaje = texto;
                string sender = "Clin.Flores";
                string numero = "502" + telefono;
                var client = new RestClient("https://api.labsmobile.com/json/send");
                client.Authenticator = new HttpBasicAuthenticator("secret", "secret");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\"message\":\"" + mensaje + "\", \"tpoa\":\"" + sender + "\",\"recipient\":[{\"msisdn\":\"" + numero + "\"}]}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                dynamic api = JObject.Parse(response.Content);
                var code = api.code;
                var message = api.message;

                await repo.LogMensajeManual(Convert.ToString(code.Value), Convert.ToString(message.Value), response.StatusCode.ToString(), telefono, idPaciente, contacto,texto);

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                try
                {
                    await repo.LogMensajeManual("-1", error, "-1", telefono, idPaciente, contacto, texto);
                }
                catch (Exception exe)
                {
                }
            }
            return 0;
        }
    }
}

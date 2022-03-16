using Core.Modelos.Entorno;
using Core.Repositorios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utils.ExcelReader
{
    public class ExcelReader
    {

        public static List<PacienteModelo> loadData(string filePath) {
            using (var reader = new StreamReader(filePath))
            {
                List<PacienteModelo> data = new List<PacienteModelo>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    PacienteModelo paciente = new PacienteModelo();
                    paciente.id_paciente = values[0];
                    paciente.id_usuario = "1";
                    paciente.nombre = values[1];
                    paciente.apellido = values[2];
                    paciente.cui = values[3];
                    paciente.genero = buildGenero(values[4]);
                    paciente.fecha_nacimiento = buildDate(values[5]);
                    paciente.direccion = values[6];
                    paciente.id_departamento = buildDepto(values[7]);
                    TelefonoModelo telefono1 = new TelefonoModelo();
                    telefono1.nota = values[8];
                    telefono1.telefono = values[9];
                    telefono1.telefono = telefono1.telefono.Replace("-", "");
                    paciente.telefonos = new List<TelefonoModelo>();
                    paciente.telefonos.Add(telefono1);

                    if (values[11].Length >= 0) {
                        TelefonoModelo telefono2 = new TelefonoModelo();
                        telefono2.nota = values[10];
                        telefono2.telefono = values[11];
                        telefono2.telefono = telefono2.telefono.Replace("-", "");
                        paciente.telefonos.Add(telefono2);
                    }

                    if (values[13].Length >= 0)
                    {
                        TelefonoModelo telefono2 = new TelefonoModelo();
                        telefono2.nota = values[12];
                        telefono2.telefono = values[13];
                        telefono2.telefono = telefono2.telefono.Replace("-", "");
                        paciente.telefonos.Add(telefono2);
                    }

                    paciente.nota_importante = values[14];
                    paciente.motivo_consulta = values[15];
                    paciente.historia_enf_actual = values[16];
                    paciente.antecedente = values[17];
                    paciente.alergia = values[18];
                    paciente.examen_fisico = values[19];
                    paciente.historial_previo = values[20];


                    paciente.fecha_primer_consulta = buildDate(values[21]);
                    paciente.fecha_ultima_consulta = buildDate(values[22]);

                    data.Add(paciente);
                }

                return data;
            }
        }

        private static string buildDate(string fecha) {
            if (fecha == null || fecha.Trim().Equals("")) return ""; 
            string[] strArrayOne = fecha.Split('/');
            return strArrayOne.Length == 3 ? strArrayOne[2] + "-" + strArrayOne[1] + "-" + strArrayOne[0] : "01-01-1990";
        }

        private static string buildGenero(string genero) {
            if (genero.ToUpper().Equals("MASCULINO"))
            {
                return "M";
            }
            else if (genero.ToUpper().Equals("FEMENINO"))
            {
                return "F";
            }
            else {
                return "0";
            }
        }

        private static string buildDepto(string depto) {
            switch (depto.ToUpper().Trim()) {
                case "ALTA VERAPAZ": return "1";
                case "BAJA VERAPAZ": return "2";
                case "CHIMALTENANGO": return "3";
                case "CHIQUIMULA": return "4";
                case "EL PROGRESO": return "5";
                case "ESCUINTLA": return "6";
                case "GUATEMALA": return "7";
                case "HUEHUETENANGO": return "8";
                case "IZABAL": return "9";
                case "JALAPA": return "10";
                case "JUTIAPA": return "11";
                case "PETÉN": return "12";
                case "QUETZALTENANGO": return "13";
                case "QUICHÉ": return "14";
                case "RETALHULEU": return "15";
                case "SACATEPÉQUEZ": return "16";
                case "SAN MARCOS": return "17";
                case "SANTA ROSA": return "18";
                case "SOLOLÁ": return "19";
                case "SUCHITEPÉQUEZ": return "20";
                case "TOTONICAPÁN": return "21";
                case "ZACAPA": return "22";
                case "AGUASCALIENTES": return "23";
                case "BAJA CALIFORNIA": return "24";
                case "BAJA CALIFORNIA SUR": return "25";
                case "CAMPECHE": return "26";
                case "CHIAPAS": return "27";
                case "CHIHUAHUA": return "28";
                case "COAHUILA": return "29";
                case "COLIMA": return "30";
                case "DISTRITO FEDERAL": return "31";
                case "DURANGO": return "32";
                case "ESTADO DE MÉXICO": return "33";
                case "GUANAJUATO": return "34";
                case "GUERRERO": return "35";
                case "HIDALGO": return "36";
                case "JALISCO": return "37";
                case "MICHOACÁN": return "38";
                case "MORELOS": return "39";
                case "NAYARIT": return "40";
                case "NUEVO LEÓN": return "41";
                case "OAXACA": return "42";
                case "PUEBLA": return "43";
                case "QUERÉTARO": return "44";
                case "QUINTANA ROO": return "45";
                case "SAN LUIS POTOSÍ": return "46";
                case "SINALOA": return "47";
                case "SONORA": return "48";
                case "TABASCO": return "49";
                case "TAMAULIPAS": return "50";
                case "TLAXCALA": return "51";
                case "VERACRUZ": return "52";
                case "YUCATÁN": return "53";
                case "ZACATECAS": return "54";
                default: return "0";
            }
            /*
 

             */
        }
    }
}

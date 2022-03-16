using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class ResponseServer
    {
        public ResponseServer() {
            this.datos = new List<string>();
        }
        public string status { get; set; }
        public string mensaje { get; set; }
        public List<string> datos { get; set; }
    }
    public class ResponseServerAuth
    {
        public string status { get; set; }
        public string mensaje { get; set; }
        public UsuarioModelo user { get; set; }
        public string token { get; set; }
    }
}

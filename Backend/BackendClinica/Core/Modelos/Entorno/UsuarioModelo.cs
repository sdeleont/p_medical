using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Modelos.Entorno
{
    public class UsuarioModelo
    {
        public string id_usuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string firma { get; set; }
        public string estado { get; set; }
        public string id_rol { get; set; }
        public string rol { get; set; }

    }
    public class UsuarioAuth {
        public string usuario { get; set; }
        public string pass { get; set; }
    }

    public class PasswordModelo {
        public string id_usuario { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}



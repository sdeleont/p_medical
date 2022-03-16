using Core.Modelos.Entorno;
using Core.Modelos.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    // aqui se declaran los metodos que se van a implementar en la clase siempre son de tipo Task
    public interface IUsuario
    {
        Task<List<UsuarioModelo>> ObtenerUsuario(string usuario);
        Task<List<UsuarioModelo>> ObtenerUsuarios();
        Task<ResponseServer> CrearUsuario(UsuarioModelo usuario);
        Task<ResponseServer> EliminarUsuario(string idUsuario);
        Task<ResponseServerAuth> AutenticarUsuario(UsuarioAuth usuario);
        Task<ResponseServerAuth> CambiarPassword(PasswordModelo password);
        Task<ResponseServerAuth> ResetearPassword(PasswordModelo password);
    }
}

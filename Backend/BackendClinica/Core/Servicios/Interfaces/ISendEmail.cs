using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios.Interfaces
{
    public interface ISendEmail
    {
        Task<int> SendTest(string mensaje);
    }
}

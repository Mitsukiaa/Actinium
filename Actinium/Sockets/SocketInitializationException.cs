using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actinium.Sockets
{
    public class SocketInitializationException : Exception
    {
        public SocketInitializationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}

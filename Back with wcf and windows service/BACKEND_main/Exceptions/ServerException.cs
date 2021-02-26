using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKEND_CONTACTS.Exceptions
{
    public class ServerException : Exception
    {
        public int Key { get; set; }

        public ServerException(string message, int key) : base(message)
        {
            Key = key;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKEND_CONTACTS.Exceptions
{
    public class IncorrectDataException : Exception
    {
        public int Key { get; set; }

        public IncorrectDataException(string message, int key) : base(message)
        {
            Key = key;
        }
    }
}

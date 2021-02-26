using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Util
{
    public class FileData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] File { get; set; }
    }
}
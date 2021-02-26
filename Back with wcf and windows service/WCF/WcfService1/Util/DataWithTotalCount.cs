using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.Model;

namespace WcfService1.Util
{
    public class DataWithTotalCount
    {
        public List<Contact> data { get; set; }
        public int total_count { get; set; }
    }
}
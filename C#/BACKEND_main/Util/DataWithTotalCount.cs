using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKEND_CONTACTS.Models
{
    public class DataWithTotalCount
    {
        public List<Contact> data { get; set; }
        public int total_count { get; set; }
    }
}

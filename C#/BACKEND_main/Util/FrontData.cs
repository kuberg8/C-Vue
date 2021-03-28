using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BACKEND_CONTACTS.Models
{
    public class FrontData
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Message { get; set; }
        public IFormFile Avatar { get; set; }
    }
}

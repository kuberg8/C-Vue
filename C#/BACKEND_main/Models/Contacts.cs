using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BACKEND_CONTACTS.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DocumentType { get; set; }
        public int Series { get; set; }
        public int PasportNumber { get; set; }
        public string IssuedBy { get; set; }
        public string DateOfIssue { get; set; }
        public long BirthCertificate { get; set; }
        public long DriveNumber { get; set; }
        public string Message { get; set; }
        public int FileId { get; set; }
    }
}

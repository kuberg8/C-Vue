using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Util
{
    public class BackData
    {
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
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileContnetnType { get; set; }
    }
}
using System;
using System.Linq;
using WcfService1.Model;
using WcfService1.Util;
using System.IO;
using System.Configuration;
using WcfService1.Exceptions;

namespace WcfService1
{
    public class Service1 : IContactService
    {
        private string filePathName;

        public void CreateContact(BackData data)
        {
            using (ContactContext db = new ContactContext())
            {
                if (data == null)
                {
                    throw new DataException();
                }

                var user = new Contact
                {
                    Name = data.Name,
                    LastName = data.LastName,
                    MiddleName = data.MiddleName,
                    DateOfBirthday = data.DateOfBirthday,
                    Email = data.Email,
                    Phone = data.Phone,
                    DocumentType = data.DocumentType,
                    Series = data.Series,
                    PasportNumber = data.PasportNumber,
                    IssuedBy = data.IssuedBy,
                    DateOfIssue = data.DateOfIssue,
                    BirthCertificate = data.BirthCertificate,
                    DriveNumber = data.DriveNumber,
                    Message = data.Message
                };

                    
                if (data.File != null)
                {
                    Guid uniqueName = Guid.NewGuid();
                    filePathName = ConfigurationManager.AppSettings["Files_store"] + @"\" + uniqueName + data.FileName;


                    using (var fileStream = new FileStream(filePathName, FileMode.Create))
                    {
                        fileStream.Write(data.File, 0, data.File.Length);
                    }



                    user.FileName = data.FileName;
                    user.FilePath = filePathName;
                    user.FileContnetnType = data.FileContnetnType;
                }
                    

                db.Contacts.Add(user);
                db.SaveChanges();
            }
        }

        public DataWithTotalCount GetContact(string SortName = "Name", bool Descending = false, int Page = 1, int PageSize = 20) {

            using (ContactContext db = new ContactContext())
            {
                IQueryable<Contact> users = db.Contacts;

                if(users == null)
                {
                    throw new DataException();
                }

                int total_count = users.Count();


                switch (SortName)
                {
                    case "Name":
                        users = Descending ? users.OrderByDescending(t => t.Name) : users.OrderBy(t => t.Name);
                        break;
                    case "LastName":
                        users = Descending ? users.OrderByDescending(t => t.LastName) : users.OrderBy(t => t.LastName);
                        break;
                    case "MiddleName":
                        users = Descending ? users.OrderByDescending(t => t.MiddleName) : users.OrderBy(t => t.MiddleName);
                        break;
                    case "Email":
                        users = Descending ? users.OrderByDescending(t => t.Email) : users.OrderBy(t => t.Email);
                        break;
                    case "Date":
                        users = Descending ? users.OrderByDescending(t => t.DateOfBirthday) : users.OrderBy(t => t.DateOfBirthday);
                        break;
                    default:
                        users = users.OrderBy(t => t.Id);
                        break;
                }


                var items = users.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                var response = new DataWithTotalCount { data = items, total_count = total_count };

                return response;
            }
        }

        public FileData Download(int fileId)
        {
            using (ContactContext db = new ContactContext())
            {
                var user = db.Contacts.FirstOrDefault(t => t.Id == fileId);

                if (user == null)
                {
                    throw new UserNotFoundException();
                } else if(user.FileName == null || user.FilePath == null)
                {
                    throw new UserFileNotFoundException();
                }

                using (var fileStream = new FileStream(user.FilePath, FileMode.Open))
                {
                    var memoryString = new MemoryStream();
                    fileStream.CopyTo(memoryString);
                    var fileBytes = memoryString.ToArray();

                    var File = new FileData
                    {
                        Name = user.FileName,
                        Type = user.FileContnetnType,
                        File = fileBytes
                    };
                    return File;
                }
            };
        }
    }
}
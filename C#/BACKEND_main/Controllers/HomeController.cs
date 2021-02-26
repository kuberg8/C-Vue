using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BACKEND_CONTACTS.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace BACKEND_CONTACTS.Controllers
{
    public class HomeController : Controller
    {
        ContactContext db;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ContactContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        [RequestSizeLimit(100000)]
        public async Task<StatusCodeResult> Post([FromForm] FrontData data)
        {
            try
            {
                if(data == null)
                {
                    throw new Exception("Неправильный запрос");
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

                if (data.Avatar != null && (data.Avatar.ContentType == "image/jpeg" || data.Avatar.ContentType == "image/png"))
                {
                    string fileType = data.Avatar.ContentType;
                    Guid filePathName = Guid.NewGuid();
                    string path = "/Files/" + filePathName + "." + fileType.Replace("image/", "");
                    
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await data.Avatar.CopyToAsync(fileStream);
                    }

                    var file = new File_info
                    {
                        Name = data.Avatar.FileName,
                        Path = path
                    };

                    db.Files.Add(file);
                    db.SaveChanges();
                    user.FileId = file.Id;
                } else if(data.Avatar != null)
                {
                    throw new Exception("Загрузите файл формата jpg или png");
                }

                db.Contacts.Add(user);
                db.SaveChanges();

                return StatusCode(200);
            } catch
            {
                throw;
            }
        }

        
        public VirtualFileResult Download(int fileId)
        {
            try
            {
                var file = db.Files.FirstOrDefault(t => t.Id == fileId);
                if(file == null)
                {
                    throw new Exception("нету файла с данным id");
                }
                return File("~" + file.Path, "application/octet-stream", file.Name);
            } catch
            {
                throw;
            }

        }


        public async Task<JsonResult> Index(string SortName = "Name", bool Descending = false, int Page = 1, int PageSize = 20)
        {
            IQueryable<Contact> users = db.Contacts;

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


            try 
            {
                var items = await users.Skip((Page - 1) * PageSize).Take(PageSize).ToListAsync();

                var response = new DataWithTotalCount { data = items, total_count = total_count};

                return Json(response);
            } catch
            {
                throw;
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                Contact user = await db.Contacts.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Contacts.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
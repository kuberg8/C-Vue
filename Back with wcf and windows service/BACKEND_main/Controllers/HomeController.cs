using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BACKEND_CONTACTS.Models;
using ServiceReference2;
using System.IO;
using BACKEND_CONTACTS.Exceptions;
using System.ServiceModel;

namespace BACKEND_CONTACTS.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [RequestSizeLimit(15000)]
        public async Task<StatusCodeResult> CreateContact([FromForm] FrontData data)
        {
            ContactServiceClient ms = new ContactServiceClient();

            BackData serviceContact;

            try
            {
                serviceContact = new BackData
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

                if (data.File != null && (data.File.ContentType == "image/jpeg" || data.File.ContentType == "image/png"))
                {
                    var memoryString = new MemoryStream();
                    data.File.CopyTo(memoryString);
                    var fileBytes = memoryString.ToArray();
                    serviceContact.File = fileBytes;
                    serviceContact.FileName = data.File.FileName;
                    serviceContact.FileContnetnType = data.File.ContentType;
                }
                else if (data.File != null)
                {
                    throw new IncorrectDataException("Неверный формат файла", 1);
                }
            }
            catch (NullReferenceException)
            {
                throw new IncorrectDataException("Неверно указанные имена полей в запросе", 1);
            }
            catch (InvalidCastException)
            {
                throw new IncorrectDataException("Неверно указаны типы данных в запросе", 1);
            }

            try
            {
                await ms.CreateContactAsync(serviceContact);
            }
            catch(EndpointNotFoundException)
            {
                throw new ServerException("Неверный адрес запроса", 1);
            }
            catch(TimeoutException)
            {
                throw new ServerException("Превышено время ожидания", 1);
            }
            catch(ProtocolException)
            {
                throw new ServerException("Не совпадают типы содержимого запроса или превышен максимальный размер", 1);
            }
            catch
            {
                throw new ServerException("Ошибка в WCF сервисе", 1);
            }
                
            return StatusCode(200);
        }

        
        public async Task<FileResult> Download(int fileId)
        {
            ContactServiceClient ms = new ContactServiceClient();

            try
            {           
                var response = await ms.DownloadAsync(fileId);
                return File(response.File, response.Type, response.Name);
            }
            catch (EndpointNotFoundException)
            {
                throw new ServerException("Неверный адрес запроса", 1);
            }
            catch (TimeoutException)
            {
                throw new ServerException("Превышено время ожидания", 1);
            }
            catch (ProtocolException)
            {
                throw new ServerException("Не совпадают типы содержимого запроса или превышен максимальный размер", 1);
            }
            catch
            {
                throw new ServerException("Ошибка в WCF сервисе", 1);
            }
        }
        

        public async Task<JsonResult> GetContacts(string sortName = "Name", bool descending = false, int page = 1, int pageSize = 20)
        {
            ContactServiceClient ms = new ContactServiceClient();

            try
            {
                return Json(await ms.GetContactAsync(sortName, descending, page, pageSize));
            }
            catch (EndpointNotFoundException)
            {
                throw new ServerException("Неверный адрес запроса", 1);
            }
            catch (TimeoutException)
            {
                throw new ServerException("Превышено время ожидания", 1);
            }
            catch (ProtocolException)
            {
                throw new ServerException("Не совпадают типы содержимого запроса или превышен максимальный размер", 1);
            }
            catch(Exception ex)
            {
                var a = ex;
                throw new ServerException("Ошибка в WCF сервисе", 1);
            }
        }
    }
}
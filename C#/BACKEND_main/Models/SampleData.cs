using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKEND_CONTACTS.Models
{
    public static class SampleData
    {
        public static void Initialize(ContactContext context)
        {
            if (!context.Contacts.Any())
            {
                context.Contacts.AddRange(
                    new Contact
                    {
                        Name = "Дорофей",
                        LastName = "Лагошин",
                        MiddleName = "Тарасович",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск.."

                    },
                    new Contact
                    {
                        Name = "Армен",
                        LastName = "Веселов",
                        MiddleName = "Германович",
                        DateOfBirthday = "2020-02-12",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Никита",
                        LastName = "Жуков",
                        MiddleName = "Олегович",
                        DateOfBirthday = "1997-08-04",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Амадей",
                        LastName = "Веденеев",
                        MiddleName = "Игоревич",
                        DateOfBirthday = "2005-16-07",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск..",
                        DateOfIssue = "2015-02-11",

                    },
                    new Contact
                    {
                        Name = "Ренольд",
                        LastName = "Анников",
                        MiddleName = "Мстиславович",
                        DateOfBirthday = "2008-04-09",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Камиль",
                        LastName = "Артёмов",
                        MiddleName = "Осипович",
                        DateOfBirthday = "2011-05-01",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Гевор",
                        LastName = "Панарин",
                        MiddleName = "Константинович",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск.."

                    },
                    new Contact
                    {
                        Name = "Израиль",
                        LastName = "Малюгин",
                        MiddleName = "Леонтьевич",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Тибор",
                        LastName = "Ельцов",
                        MiddleName = "Павлович",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Степан",
                        LastName = "Исмайлов",
                        MiddleName = "Борисович",
                        DateOfBirthday = "2016-02-11",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск..",

                    },
                    new Contact
                    {
                        Name = "Ижутин",
                        LastName = "Джереми",
                        MiddleName = "Филиппович",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Хабаров",
                        LastName = "Филипп",
                        MiddleName = "Аркадьевич",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Ждан ",
                        LastName = "Чазов",
                        MiddleName = "Онисимович",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск..",

                    },
                    new Contact
                    {
                        Name = "Бернар",
                        LastName = "Левкин",
                        MiddleName = "Львович",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Эраст",
                        LastName = "Шмелев",
                        MiddleName = "Натанович",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Ратмир",
                        LastName = "Каретников",
                        MiddleName = "Петрович",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск..",

                    },
                    new Contact
                    {
                        Name = "Потап",
                        LastName = "Алёхин",
                        MiddleName = "Витальевич",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Станислав",
                        LastName = "Енин",
                        MiddleName = "Акимович",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Ксанф",
                        LastName = "Лобов",
                        MiddleName = "Константинович",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск..",

                    },
                    new Contact
                    {
                        Name = "Артур",
                        LastName = "Жидков",
                        MiddleName = "Михайлович",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Прокофий",
                        LastName = "Веселов",
                        MiddleName = "Алексеевич",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    },
                    new Contact
                    {
                        Name = "Матвей",
                        LastName = "Котов",
                        MiddleName = "Витальевич",
                        Phone = "79292921911",
                        DocumentType = "Паспорт",
                        Series = 1111,
                        PasportNumber = 222222,
                        IssuedBy = "Челябинск.."

                    },
                    new Contact
                    {
                        Name = "Ефим",
                        LastName = "Пузанов",
                        MiddleName = "Петрович",
                        Phone = "79292921911",
                        DocumentType = "Водительское удостоверение",
                        DriveNumber = 21321312312

                    },
                    new Contact
                    {
                        Name = "Клаус",
                        LastName = "Теплов",
                        MiddleName = "Русланович",
                        Phone = "79292921911",
                        DocumentType = "Свидетельство о рождении",
                        BirthCertificate = 1121212121
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

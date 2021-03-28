﻿using System;
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

                    },
                    new Contact
                    {
                        Name = "Армен",
                        LastName = "Веселов",
                        MiddleName = "Германович",
                        DateOfBirthday = new DateTime(2015, 7, 20),

                    },
                    new Contact
                    {
                        Name = "Никита",
                        LastName = "Жуков",
                        MiddleName = "Олегович",
                        DateOfBirthday = new DateTime(2012, 1, 11),
                    },
                    new Contact
                    {
                        Name = "Амадей",
                        LastName = "Веденеев",
                        MiddleName = "Игоревич",
                        DateOfBirthday = new DateTime(1997, 4, 8),

                    },
                    new Contact
                    {
                        Name = "Ренольд",
                        LastName = "Анников",
                        MiddleName = "Мстиславович",
                        DateOfBirthday = new DateTime(2000, 5, 30),

                    },
                    new Contact
                    {
                        Name = "Камиль",
                        LastName = "Артёмов",
                        MiddleName = "Осипович",
                        DateOfBirthday = new DateTime(2021, 12, 1),
                    },
                    new Contact
                    {
                        Name = "Гевор",
                        LastName = "Панарин",
                        MiddleName = "Константинович",

                    },
                    new Contact
                    {
                        Name = "Израиль",
                        LastName = "Малюгин",
                        MiddleName = "Леонтьевич",

                    },
                    new Contact
                    {
                        Name = "Тибор",
                        LastName = "Ельцов",
                        MiddleName = "Павлович",
                    },
                    new Contact
                    {
                        Name = "Степан",
                        LastName = "Исмайлов",
                        MiddleName = "Борисович",
                        DateOfBirthday = new DateTime(2011, 9, 14),

                    },
                    new Contact
                    {
                        Name = "Ижутин",
                        LastName = "Джереми",
                        MiddleName = "Филиппович",

                    },
                    new Contact
                    {
                        Name = "Хабаров",
                        LastName = "Филипп",
                        MiddleName = "Аркадьевич",
                    },
                    new Contact
                    {
                        Name = "Ждан ",
                        LastName = "Чазов",
                        MiddleName = "Онисимович",
                        Phone = 9292921911

                    },
                    new Contact
                    {
                        Name = "Бернар",
                        LastName = "Левкин",
                        MiddleName = "Львович",
                        Phone = 9292921911,

                    },
                    new Contact
                    {
                        Name = "Эраст",
                        LastName = "Шмелев",
                        MiddleName = "Натанович",
                        Phone = 9292921911,
                    },
                    new Contact
                    {
                        Name = "Ратмир",
                        LastName = "Каретников",
                        MiddleName = "Петрович",
                        Phone = 9292921911,

                    },
                    new Contact
                    {
                        Name = "Потап",
                        LastName = "Алёхин",
                        MiddleName = "Витальевич",
                        Phone = 9292921911

                    },
                    new Contact
                    {
                        Name = "Станислав",
                        LastName = "Енин",
                        MiddleName = "Акимович",
                        Phone = 9292921911
                    },
                    new Contact
                    {
                        Name = "Ксанф",
                        LastName = "Лобов",
                        MiddleName = "Константинович",
                        Phone = 9292921911

                    },
                    new Contact
                    {
                        Name = "Артур",
                        LastName = "Жидков",
                        MiddleName = "Михайлович",
                        Phone = 9292921911,

                    },
                    new Contact
                    {
                        Name = "Прокофий",
                        LastName = "Веселов",
                        MiddleName = "Алексеевич",
                        Phone = 9292921911
                    },
                    new Contact
                    {
                        Name = "Матвей",
                        LastName = "Котов",
                        MiddleName = "Витальевич",
                        Phone = 9292921911,

                    },
                    new Contact
                    {
                        Name = "Ефим",
                        LastName = "Пузанов",
                        MiddleName = "Петрович",
                        Phone = 9292921911,

                    },
                    new Contact
                    {
                        Name = "Клаус",
                        LastName = "Теплов",
                        MiddleName = "Русланович",
                        Phone = 9292921911
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

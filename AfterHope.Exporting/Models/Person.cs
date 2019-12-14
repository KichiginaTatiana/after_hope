using System;
using CsvHelper.Configuration.Attributes;

namespace AfterHope.Exporting.Models
{
    public class Person
    {
        [Name("ФИО")]
        public string Name { get; set; }

        [Name("Дело")]
        public string Lawsuit { get; set; }

        [Name("Тип")]
        public string Type { get; set; }

        [Name("Адрес")]
        public string Address { get; set; }

        [Name("Информация")]
        public string Info { get; set; }

        [Name("День рождения")]
        public DateTime? Birthday { get; set; }

        [Name("Статус")]
        public string Status { get; set; }
    }
}
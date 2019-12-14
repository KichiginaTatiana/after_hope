using System;
using System.IO;
using System.Linq;
using AfterHope.Data;
using AfterHope.Data.Models;
using AutoMapper;
using CsvHelper;

namespace AfterHope.Exporting
{
    public class PersonListExporter : IPersonListExporter
    {
        private readonly IPersonRepository personRepository;

        public PersonListExporter(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
            Mapper.Initialize(cfg => { cfg.CreateMap<Person, Models.Person>(); });
        }

        public byte[] Export(string lawsuit)
        {
            var persons = (lawsuit == "all"
                ? personRepository.ReadAll()
                : personRepository.SelectByLawsuit(lawsuit)).Select(Mapper.Map<Models.Person>);

            var fileName = $"{Guid.NewGuid().ToString()}.csv";
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer))
                csv.WriteRecords(persons);

            return File.ReadAllBytes(fileName);
        }
    }
}
using System.IO;
using System.Linq;
using System.Text;
using AfterHope.Data;
using AfterHope.Data.Models;
using AfterHope.Exporting.Extensions;
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

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(persons);

                streamWriter.Flush();
                memoryStream.Position = 0;
                return memoryStream.ReadToEnd();
            }
        }
    }
}
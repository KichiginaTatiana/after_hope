using System;
using AfterHope.Data.Models;
using AfterHope.Data.Repositories;

namespace AfterHope.Data.Parsers
{
    public class PersonParser : IPersonParser
    {
        private readonly IPersonRepository personRepository;

        public PersonParser(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Person Parse(string data, string photoId)
        {
            var strings = data.Split("\n");
            if (strings.Length < 3)
                throw new ArgumentException();

            var id = ((uint) strings[0].GetHashCode()).ToString();
            var person = personRepository.Read(id);

            return new Person
            {
                PhotoId = photoId,
                Id = id,
                Name = strings[0],
                Lawsuit = strings[1],
                Address = strings[2],
                Birthday = strings.Length > 3 ? DateTime.Parse(strings[3]) : null as DateTime?,
                Info = strings.Length > 4 ? strings[4] : null,
                Type = strings.Length > 5 ? strings[5] : null,
                Status = strings.Length > 6 ? strings[6] : null,
                Created = person?.Created ?? DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
        }
    }
}
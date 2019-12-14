using System;
using System.Linq;
using AfterHope.Data.Models;
using AfterHope.Data.Repositories;

namespace AfterHope.Data.Parsers
{
    public class PosterParser : IPosterParser
    {
        private readonly IPersonRepository personRepository;

        public PosterParser(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Poster Parse(string data, string photoId)
        {
            var selectedByLawsuit = personRepository.SelectByLawsuit(data);
            if (selectedByLawsuit.Any())
                return new Poster
                {
                    FileId = photoId,
                    Lawsuit = data,
                    Id = (photoId + data).GetHashCode().ToString()
                };

            var selectedByName = personRepository.Select(data);
            if (selectedByName.Any())
                return new Poster
                {
                    FileId = photoId,
                    PersonId = selectedByName.First().Id,
                    Id = (photoId + data).GetHashCode().ToString()
                };

            throw new ArgumentException();
        }
    }
}
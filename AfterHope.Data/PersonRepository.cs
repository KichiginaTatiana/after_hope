using System.Collections.Generic;
using AfterHope.Data.Models;
using LiteDB;

namespace AfterHope.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LiteRepository repository;

        public PersonRepository(LiteRepository repository)
        {
            this.repository = repository;
            this.repository.Database.GetCollection<Person>().EnsureIndex(x => x.Id, true);
        }

        public void Write(Person person)
        {
            try
            {
                repository.Insert(person);
            }
            catch (LiteException)
            {
                repository.Update(person);
            }
        }

        public void Delete(string id) => repository.Delete<Person>(x => x.Id == id);

        public Person Read(string id) => new Person
        {
            Name = "Name 1",
            Id = "1"
        };
        //repository.FirstOrDefault<Person>(x => x.Id == id);

        public List<Person> Select(string query) => ReadAll();
        // repository.Fetch<Person>(x => x.Name.Contains(query));

        public List<Person> SelectByLawsuit(string lawsuit) => ReadAll();
        //repository.Fetch<Person>(x => x.Lawsuit.Contains(query));

        public List<Person> ReadAll() => new List<Person>()
        {
            new Person
            {
                Name = "Name 1",
                Id = "1"
            },
            new Person
            {
                Name = "Name 2",
                Id = "2"
            }
        };
        //repository.Fetch<Person>();
    }
}
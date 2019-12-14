using System.Collections.Generic;
using AfterHope.Data.Models;
using LiteDB;

namespace AfterHope.Data.Repositories
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

        public Person Read(string id) => repository.FirstOrDefault<Person>(x => x.Id == id);

        public List<Person> Select(string query) => repository.Fetch<Person>(x => x.Name.ToLower().Contains(query.ToLower()));

        public List<Person> SelectByLawsuit(string lawsuit) => repository.Fetch<Person>(x => x.Lawsuit.Contains(lawsuit));

        public List<Person> ReadAll() => repository.Fetch<Person>();
    }
}
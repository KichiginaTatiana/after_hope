using System.Collections.Generic;
using AfterHope.Data.Models;

namespace AfterHope.Data.Repositories
{
    public interface IPersonRepository
    {
        void Write(Person person);

        void Delete(string id);

        Person Read(string id);

        List<Person> Select(string query);

        List<Person> SelectByLawsuit(string lawsuit);

        List<Person> ReadAll();
    }
}
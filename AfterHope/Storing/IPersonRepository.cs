using System.Collections.Generic;
using AfterHope.Storing.Data;

namespace AfterHope.Storing
{
    public interface IPersonRepository
    {
        void Write(Person person);
        void Delete(string id);
        Person Read(string id);
        List<Person> Select(string query);
        List<Person> ReadAll();
    }
}
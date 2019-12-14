using System.Collections.Generic;
using AfterHope.Data.Models;

namespace AfterHope.Data.Repositories
{
    public interface IPosterRepository
    {
        void Write(Poster poster);
        void Delete(string id);
        Poster Read(string id);
        List<Poster> ReadByPersonId(string personId);
        List<Poster> SelectByLawsuit(string lawsuit);
        List<Poster> ReadAll();
    }
}
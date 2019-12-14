using System.Collections.Generic;
using AfterHope.Data.Models;
using LiteDB;

namespace AfterHope.Data.Repositories
{
    public class PosterRepository : IPosterRepository
    {
        private readonly LiteRepository repository;

        public PosterRepository(LiteRepository repository)
        {
            this.repository = repository;
            this.repository.Database.GetCollection<Poster>().EnsureIndex(x => x.Id, true);
        }

        public void Write(Poster poster)
        {
            try
            {
                repository.Insert(poster);
            }
            catch (LiteException)
            {
                repository.Update(poster);
            }
        }

        public void Delete(string id) => repository.Delete<Poster>(x => x.Id == id);

        public Poster Read(string id) => repository.FirstOrDefault<Poster>(x => x.Id == id);

        public List<Poster> ReadByPersonId(string personId) => repository.Fetch<Poster>(x => x.PersonId == personId);

        public List<Poster> SelectByLawsuit(string lawsuit) => repository.Fetch<Poster>(x => x.Lawsuit.Contains(lawsuit));

        public List<Poster> ReadAll() => repository.Fetch<Poster>();
    }
}
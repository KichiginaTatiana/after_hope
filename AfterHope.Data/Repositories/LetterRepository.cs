using AfterHope.Data.Models;
using LiteDB;

namespace AfterHope.Data.Repositories
{
    public class LetterRepository : ILetterRepository
    {
        private readonly LiteRepository repository;

        public LetterRepository(LiteRepository repository)
        {
            this.repository = repository;
            this.repository.Database.GetCollection<Letter>().EnsureIndex(x => x.Id, true);
        }

        public void Write(Letter letter)
        {
            try
            {
                repository.Insert(letter);
            }
            catch (LiteException)
            {
                repository.Update(letter);
            }
        }

        public Letter Read(string id) => repository.FirstOrDefault<Letter>(x => x.Id == id);
    }
}
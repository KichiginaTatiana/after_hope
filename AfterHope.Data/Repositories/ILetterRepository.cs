using AfterHope.Data.Models;

namespace AfterHope.Data.Repositories
{
    public interface ILetterRepository
    {
        void Write(Letter letter);
        Letter Read(string id);
    }
}
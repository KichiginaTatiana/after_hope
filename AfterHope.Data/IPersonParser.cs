using AfterHope.Data.Models;

namespace AfterHope.Data
{
    public interface IPersonParser
    {
        Person Parse(string data, string photoId);
    }
}
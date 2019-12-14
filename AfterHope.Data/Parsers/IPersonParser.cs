using AfterHope.Data.Models;

namespace AfterHope.Data.Parsers
{
    public interface IPersonParser
    {
        Person Parse(string data, string photoId);
    }
}
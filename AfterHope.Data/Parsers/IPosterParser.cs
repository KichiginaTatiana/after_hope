using AfterHope.Data.Models;

namespace AfterHope.Data.Parsers
{
    public interface IPosterParser
    {
        Poster Parse(string data, string photoId);
    }
}
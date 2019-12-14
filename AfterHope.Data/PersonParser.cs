using System;
using AfterHope.Data.Models;

namespace AfterHope.Data
{
    public class PersonParser : IPersonParser
    {
        public Person Parse(string data, string photoId)
        {
            var strings = data.Split("\n");
            if (strings.Length < 3)
                throw new ArgumentException();

            return new Person
            {
                PhotoId = photoId,
                Id = ((uint) strings[0].GetHashCode()).ToString(),
                Name = strings[0],
                Lawsuit = strings[1],
                Address = strings[2],
                Birthday = strings.Length > 3 ? DateTime.Parse(strings[3]) : null as DateTime?,
                Info = strings.Length > 4 ? strings[4] : null,
                Type = strings.Length > 5 ? strings[5] : null,
                Status = strings.Length > 6 ? strings[6] : null,
                Created = DateTime.UtcNow
            };
        }
    }
}
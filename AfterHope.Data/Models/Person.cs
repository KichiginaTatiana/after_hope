using System;

namespace AfterHope.Data.Models
{
    public class Person
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Lawsuit { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string Info { get; set; }

        public DateTime Birthday { get; set; }

        public string Status { get; set; }
    }
}
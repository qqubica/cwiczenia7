using System;
using System.Collections.Generic;

namespace cwiczenia7.Models.DTO
{
    public class GetTips
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Client> Clients { get; set; }

        public class Country
        {
            public string Name { get; set; }
        }
        public class Client
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}

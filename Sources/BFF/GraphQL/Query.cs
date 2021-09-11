using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Models;

namespace BFF.GraphQL
{
    public class Query
    {
        public User GetUser()
        {
            return new User
            {
                Name = "Serezhka",
                Age = 21,
            };
        }
    }
}

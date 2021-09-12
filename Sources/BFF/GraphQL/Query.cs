using Common.Models;

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

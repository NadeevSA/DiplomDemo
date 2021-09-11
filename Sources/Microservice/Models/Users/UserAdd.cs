using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Models.Users
{
    public class UserAdd : User
    {
        public DateTime CreatedAt { get; set; }
    }
}

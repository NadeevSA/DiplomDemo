using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microservice.Models.Users;

namespace Microservice.Providers
{
    public class UserProvider
    {
        private readonly IDbConnector _connector;
        public UserProvider(IDbConnector connector)
        {
            _connector = connector;
        }

        #region _addQuery

        private readonly string _addQuery = @"
insert into users (
                   name,
                   age) 
                   values (
                       :Name,
                       :Age
                   )";

        #endregion

        public void Add(UserAdd toAdd)
        {
            if (toAdd.CreatedAt == default)
            {
                throw new ArgumentException($"{nameof(toAdd)} must be defined");
            }

            using var connection = _connector.Connect();
            var parametrs = new DynamicParameters(
                new
                {
                    toAdd.Name,
                    toAdd.Age
                });
            connection.Execute(_addQuery, parametrs);
            connection.Close();
        }

    }
}

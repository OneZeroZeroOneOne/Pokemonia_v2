using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;
using Pokemonia.Dal.Queryes;
using Npgsql;
using Dapper;
using System.Data;
using System.Linq;

namespace Pokemonia.Dal.Extentions
{
    public static class UserModelExt
    {
        public static User GetUser(this IDbConnection dbConnection, string login)
        {
            string GetUserQuery = String.Format(QueryUser.GetUser, login);
            return dbConnection.Query<User>(GetUserQuery).FirstOrDefault();
        }
    }
}

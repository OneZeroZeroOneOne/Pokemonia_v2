using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Dal.Queryes
{
    public static class QueryUser
    {
        public static string GetUser = "select u.\"Id\", u.\"Name\", us.\"Login\", us.\"Password\" from \"User\" u join \"UserSecurity\" us on u.\"Id\" = us.\"UserId\" where us.\"Login\" = '{0}'";
    }
}

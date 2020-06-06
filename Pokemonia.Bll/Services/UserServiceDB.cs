using System;
using Pokemonia.Dal.Models;

namespace Pokemonia.Bll.Services
{
    public class UserServiceDB : BaseServiceDB
    {
        public User GetUser(string login)
        {
            User user = _dbWorker.GetUser(login);
            if(user == null)
            {
                throw new Exception();
            }
        }
    }
}

﻿using System;
using Pokemonia.Dal.Models;
using Pokemonia.Utils.Exceptions;

namespace Pokemonia.Bll.Services
{
    public class UserServiceDB : BaseServiceDB
    {
        public User GetUser(string login, string password)
        {
            User user = _dbWorker.GetUser(login);
            if(user == null)
            {
                throw new Exception();
            }
            if(user.Password != password)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.PasswordNotValid, "Password not valid");
            }
            return user;
        }
    }
}

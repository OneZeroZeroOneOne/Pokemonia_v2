using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal;
using Pokemonia.Dal.Models;

namespace Pokemonia.Bll.Services
{
    public class BaseServiceDB
    {
        protected DBWorker _dbWorker;
        public BaseServiceDB()
        {
            _dbWorker = new DBWorker();
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;

namespace Pokemonia.Bll.Services
{
    public class MapServiceDB : BaseServiceDB
    {
        public Map GetMap(int mapId)
        {
            Map map = _dbWorker.GetMap(mapId);
            return map;
        }
        public Dictionary<int, Map> GetAllMaps()
        {
            return _dbWorker.GetAllMaps();
        }
    }
}

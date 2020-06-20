using Pokemonia.Bll.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;
using Pokemonia.Dal.LogicModels;
using System.Threading;

namespace Pokemonia.MapEngine2D
{
    public class MapEngineGenerator
    {
        private MapServiceDB _mapServiceDB;
        private Dictionary<int, MapDataHolder> _collections;
        private Dictionary<int, Map> _maps = new Dictionary<int, Map>();
        public MapEngineGenerator()
        {
            _mapServiceDB = new MapServiceDB();
            _maps = _mapServiceDB.GetAllMaps();
            _collections = new Dictionary<int, MapDataHolder>();
        }

        public Dictionary<int, MapDataHolder> RunMapsEngine()
        {
            foreach(var map in _maps)
            {
                MapDataHolder mapDataHolder = new MapDataHolder()
                {
                    users = new List<User>(),
                    usersMoveCoordinates = new List<Coordinates<User>>(),
                    outInfoCurrentStateMap = new InfoCurrentStateMap(),
                    killMonsters = new List<Monster>(),
                    disconnectUser = new List<User>(),
                };
                _collections.Add(map.Value.Id, mapDataHolder);
                MapEngine mapEnjine = new MapEngine(map.Value, mapDataHolder);
                Thread th = new Thread(mapEnjine.Run);
                th.Start();
                break;
            }
            return _collections;
        }

        public Dictionary<int, MapDataHolder> GetCollections()
        {
            return _collections;
        }
    }
}

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
        private Dictionary<int, Map> _maps = new Dictionary<int, Map>();
        public MapEngineGenerator()
        {
            _mapServiceDB = new MapServiceDB();
            _maps = _mapServiceDB.GetAllMaps();
        }

        public Dictionary<int, MapDataHolder> RunMapsEngine()
        {
            Dictionary<int, MapDataHolder> collections = new Dictionary<int, MapDataHolder>();
            foreach(var map in _maps)
            {
                MapDataHolder mapDataHolder = new MapDataHolder()
                {
                    users = new BlockingCollection<User>(),
                    usersMoveCoordinates = new BlockingCollection<Coordinates<User>>(),
                    outInfoCurrentStateMap = new BlockingCollection<InfoCurrentStateMap>(),
                    killMonsters = new BlockingCollection<TemporaryObjectPokemon>(),
                };
                collections.Add(map.Value.Id, mapDataHolder);
                MapEngine mapEnjine = new MapEngine(map.Value, mapDataHolder.users, 
                                        mapDataHolder.usersMoveCoordinates, mapDataHolder.killMonsters,
                                        mapDataHolder.outInfoCurrentStateMap);
                Thread th = new Thread(mapEnjine.Run);
                th.Start();
                break;
            }
            return collections;
        }
    }
}

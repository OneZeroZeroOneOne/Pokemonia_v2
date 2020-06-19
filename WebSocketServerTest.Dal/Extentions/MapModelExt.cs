using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Pokemonia.Dal.Queryes;
using Npgsql;
using Dapper;
using Pokemonia.Dal.Models;

namespace Pokemonia.Dal.Extentions
{
    public static class MapModelExt
    {
        public static Map GetMap(this IDbConnection dbConnection, int mapId)
        {
            Map returnedMap = null;
            string GetMapQuery = String.Format(QueryMap.GetMap, mapId);
            dbConnection
                .Query<Map, MapDecoration, Map>(GetMapQuery, (map, dec) =>
                {
                    if (returnedMap == null)
                    {
                        returnedMap = map;
                        returnedMap.Id = mapId;
                        returnedMap.MapDecoration = new List<MapDecoration>();
                    }
                    returnedMap.MapDecoration.Add(dec);
                    return returnedMap;
                }, splitOn: "Id");
            Console.WriteLine(returnedMap);
            Console.WriteLine(returnedMap.MapDecoration);
            return returnedMap;
        }

        public static Dictionary<int, Map> GetAllMaps(this IDbConnection dbConnection)
        {
            Dictionary<int, Map> returnedMaps = new Dictionary<int, Map>();
            dbConnection
                .Query<Map, MapDecoration, Map>(QueryMap.GetAllMaps, (map, dec) =>
                {
                    Map mapEntry;
                    if (!returnedMaps.TryGetValue(map.Id, out mapEntry))
                    {
                        Console.WriteLine(map.Id);
                        Console.WriteLine(dec.Id);
                        mapEntry = map;
                        mapEntry.MapDecoration = new List<MapDecoration>();
                        returnedMaps.Add(mapEntry.Id, mapEntry);
                    };
                    mapEntry.MapDecoration.Add(dec);
                    return mapEntry;
                }, splitOn: "Id");

            return returnedMaps;
        }
    }
        
}

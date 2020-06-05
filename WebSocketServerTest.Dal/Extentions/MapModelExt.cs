using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
//using Pokemonia.Dal.Maps;
//using Pokemonia.Dal.Maps.MapsObjects;
using Pokemonia.Dal.Queryes;
using Npgsql;
using Dapper;
using System.Threading.Tasks;
using Pokemonia.Dal.Queryes;
using Pokemonia.Dal.Models;

namespace Pokemonia.Dal.Extentions
{
    public static class MapModelExt
    {
        async public static Task<Map> GetMap(this IDbConnection dbConnection, int mapId)
        {
            Map returnedMap = null;
            string GetMapQuery = String.Format(QueryMap.GetMap, mapId);
            await dbConnection
                .QueryAsync<Map, MapDecoration, Map>(GetMapQuery, (map, dec) =>
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
    }
        
}

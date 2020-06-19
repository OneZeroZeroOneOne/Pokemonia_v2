using Npgsql;
using Dapper;
using System;
using System.Data;
using Pokemonia.Dal.Models;
using Pokemonia.Dal.Queryes;
using Pokemonia.Dal.Extentions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pokemonia.Dal
{
    public class DBWorker
    {
        private string _connectionString = "Host=194.99.21.140;Database=pokemonia;Username=postgres;Password=werdwerd2012";
        public DBWorker()
        {

        }
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connectionString);
            }
        }
        public Map GetMap(int mapId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetMap(mapId);
            }
        }

        public Dictionary<int, Map> GetAllMaps()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetAllMaps();
            }
        }

        public User GetUser(string login)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetUser(login);
            }
        }




    }
}

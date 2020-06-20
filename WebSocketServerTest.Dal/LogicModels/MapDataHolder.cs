using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;

namespace Pokemonia.Dal.LogicModels
{
    public struct MapDataHolder
    {
        public List<Coordinates<User>> usersMoveCoordinates;
        public List<User> users;
        public InfoCurrentStateMap outInfoCurrentStateMap;
        public List<Monster> killMonsters;
        public List<User> disconnectUser;
    }
}

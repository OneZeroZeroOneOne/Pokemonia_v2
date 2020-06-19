using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;

namespace Pokemonia.Dal.LogicModels
{
    public struct MapDataHolder
    {
        public BlockingCollection<Coordinates<User>> usersMoveCoordinates;
        public BlockingCollection<User> users;
        public BlockingCollection<InfoCurrentStateMap> outInfoCurrentStateMap;
        public BlockingCollection<Monster> killMonsters;
    }
}

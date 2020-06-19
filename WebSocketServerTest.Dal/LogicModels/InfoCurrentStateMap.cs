using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.Models;

namespace Pokemonia.Dal.LogicModels
{
    public class InfoCurrentStateMap
    {
        private Map Map { get; set; }
        public Dictionary<long, User> Users { get; set; }
        public Dictionary<long, Coordinates<User>> UsersCoordinates { get; set; }
        public Dictionary<Guid, Monster> Monsters { get; set; }
        public Dictionary<Guid, Coordinates<Monster>> MonstersCoordinates { get; set; }
    }
}

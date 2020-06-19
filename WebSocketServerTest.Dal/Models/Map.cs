using System;
using System.Collections.Generic;

namespace Pokemonia.Dal.Models
{
    public partial class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string PicUrl { get; set; }
        public int MoveDistance { get; set; }
        public int MonstersQuantity { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public virtual List<MapDecoration> MapDecoration { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Pokemonia.Dal.Models
{
    public partial class Map
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public string PicUrl { get; set; }
        public virtual List<MapDecoration> MapDecoration { get; set; }
    }
}

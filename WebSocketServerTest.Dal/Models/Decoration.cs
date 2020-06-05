using System;
using System.Collections.Generic;

namespace Pokemonia.Dal.Models
{
    public partial class Decoration
    {
        public Decoration()
        {
            MapDecoration = new HashSet<MapDecoration>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public string PicUrl { get; set; }

        public virtual ICollection<MapDecoration> MapDecoration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Dal.Queryes
{
    public static class QueryMap
    {
        public static string GetMap = "select m.\"Name\", m.\"Height\", m.\"Width\", m.\"PicUrl\", m.\"MoveDistance\", m.\"MonstersQuantity\", md.\"Id\", md.\"DecorationId\", md.\"MapId\" from \"Map\" as m inner join \"MapDecoration\" as md on m.\"Id\" = md.\"MapId\" where m.\"Id\" = {0}";
        public static string GetAllMaps = "select m.\"Id\", m.\"Name\", m.\"Height\", m.\"Width\", m.\"PicUrl\", m.\"MoveDistance\", m.\"MonstersQuantity\", md.\"Id\", md.\"DecorationId\", md.\"MapId\" from \"Map\" as m inner join \"MapDecoration\" as md on m.\"Id\" = md.\"MapId\"";
    }
}

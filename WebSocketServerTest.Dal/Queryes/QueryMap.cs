using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Dal.Queryes
{
    public static class QueryMap
    {
        public static string GetMap = "select m.\"Name\", m.\"Height\", m.\"Width\", m.\"PicUrl\", md.\"Id\", md.\"DecorationId\", md.\"MapId\" from \"Map\" as m inner join \"MapDecoration\" as md on m.\"Id\" = md.\"MapId\" where m.\"Id\" = {}";
    }
}

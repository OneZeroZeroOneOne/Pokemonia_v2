using System;
using System.Collections.Generic;

namespace Pokemonia.Dal.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}

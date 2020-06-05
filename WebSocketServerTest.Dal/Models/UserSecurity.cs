using System;
using System.Collections.Generic;

namespace Pokemonia.Dal.Models
{
    public partial class UserSecurity
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}

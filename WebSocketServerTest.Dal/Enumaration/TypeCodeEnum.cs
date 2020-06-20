using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Dal.Enumaration
{
    public enum TypeCodeEnum : byte
    {
        LoginPlease = 0x12,
        LoginSucces = 0x13,
        LoginАttempt = 0x14,
        UserAddedOnMap = 0x15,
        DisconnectUserFromMap = 0x16,
        UserChangeMap = 0x17,
    }
}

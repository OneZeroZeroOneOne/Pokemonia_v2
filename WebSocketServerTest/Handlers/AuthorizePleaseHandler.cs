using Pokemonia.Dal.Models;
using Pokemonia.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.WebServer.Handlers
{
    public class AuthorizePleaseHandler : BaseHandler
    {
        public AuthorizePleaseHandler(UserConnection userConnection) : base(userConnection)
        {
        }
        public void SendAuthorizePlease()
        {
            _userConnection.Send(_byteMessageFactory.LoginPlease());
        }

        public void SendSuccesAuthorize()
        {
            _userConnection.Send(_byteMessageFactory.SuccesLogin());
        }
    }
}

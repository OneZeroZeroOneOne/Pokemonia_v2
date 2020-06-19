using Pokemonia.Dal.Models;
using Pokemonia.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.WebServer.Handlers
{
    public class AuthorizePleaseHandler : BaseHandler
    {
        public AuthorizePleaseHandler()
        {
        }
        public void SendAuthorizePlease(UserConnection userConnection)
        {
            userConnection.Send(_byteMessageFactory.LoginPlease());
        }

        public void SendSuccesAuthorize(UserConnection userConnection)
        {
            userConnection.Send(_byteMessageFactory.SuccesLogin());
        }


    }
}

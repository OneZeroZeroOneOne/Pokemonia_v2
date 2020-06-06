using Pokemonia.Dal.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Pokemonia.WebServer
{
    public class UserConnection
    {
        private Socket _header;
        private User _user;
        public UserConnection(Socket header)
        {
            _header = header;
        }

        public byte[] Receive()
        {
            byte[] bytes = new byte[4096];
            int bytesRec = _header.Receive(bytes);
            return bytes;
        }

        public void Send(byte[] bytes)
        {
            _header.Send(bytes);
        }

        public void SetUser(User user)
        {
            _user = user;
        }

        public User GetUser()
        {
            return _user;
        }

    }
}

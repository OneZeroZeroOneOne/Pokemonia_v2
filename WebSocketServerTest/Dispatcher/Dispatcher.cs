using Pokemonia.Dal.Enumaration;
using Pokemonia.Dal.Models;
using Pokemonia.Utils;
using System.Net.Sockets;

namespace Pokemonia.WebServer
{
    public class Dispatcher
    {
        private Socket _handler;
        private User _user;
        private TypeCodeEnum _typeCode;
        private ByteMessageFactory _byteMessageFactory;
        public Dispatcher(Socket handler)
        {
            _handler = handler;
            Run();
        }

        private void Run()
        {
            while (true)
            {
                if (_user == null)
                {
                    byte[] b = _byteMessageFactory.RegisterBytes();
                    _handler.Send(b);
                }
            }
            
        }

    }
}

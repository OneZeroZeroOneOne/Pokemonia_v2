using Pokemonia.Dal.Enumaration;
using Pokemonia.Dal.Models;
using Pokemonia.Utils;
using System.Net.Sockets;
using Pokemonia.WebServer.Handlers;
using System.Threading;
using System;

namespace Pokemonia.WebServer
{
    public class Dispatcher
    {
        private UserConnection _userConnection;
        private TypeCodeEnum _typeCode;
        private ByteMessageFactory _byteMessageFactory;
        private AuthorizePleaseHandler _authorizePleaseHandler;
        private BufferParser _bufferParser;
        public Dispatcher(UserConnection userConnection)
        {
            _bufferParser = new BufferParser();
            _userConnection = userConnection;
            _authorizePleaseHandler = new AuthorizePleaseHandler(_userConnection);
            Run();
        }

        private void Run()
        {
            while(_userConnection.GetUser() == null)
            {
                _authorizePleaseHandler.SendAuthorizePlease();
                byte[] bytes = _userConnection.Receive();
                _bufferParser.Set(bytes);
                if (_bufferParser.GetByte() == (byte)ContextCodeEnum.Login & _bufferParser.GetByte() == (byte)TypeCodeEnum.Login) 
                {
                    
                }
            }
            while (true)
            {
                
                
            }
            
        }

    }
}

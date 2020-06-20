using Pokemonia.Dal.Enumaration;
using Pokemonia.Dal.Models;
using Pokemonia.Utils;
using System.Net.Sockets;
using Pokemonia.WebServer.Handlers;
using System.Threading;
using System;
using Pokemonia.Bll.Services;

namespace Pokemonia.WebServer
{
    public class Dispatcher
    {
        private UserConnection _userConnection;
        private AuthorizeHandler _authorizePleaseHandler;
        private BufferParser _bufferParser;
        private UserServiceDB _userServiceDB;
        public Dispatcher(UserConnection userConnection)
        {
            _bufferParser = new BufferParser();
            _userConnection = userConnection;
            _authorizePleaseHandler = new AuthorizeHandler();
            _userServiceDB = new UserServiceDB();
        }

        public void Run()
        {
            while(_userConnection.GetUser() == null)
            {
                _authorizePleaseHandler.SendAuthorizePlease(_userConnection);
                byte[] bytes = _userConnection.Receive();
                _bufferParser.Set(bytes);
                byte context = _bufferParser.GetByte();
                byte type = _bufferParser.GetByte();
                if (context == (byte)ContextCodeEnum.Login & type == (byte)TypeCodeEnum.LoginАttempt) 
                {
                    string[] credentials = (_bufferParser.GetString()).Split(',');
                    _userConnection.SetUser(_userServiceDB.GetUser(credentials[0], credentials[1]));
                    _authorizePleaseHandler.SendSuccesAuthorize(_userConnection);
                }
            }
            while (true)
            {
                Console.WriteLine("WHILE AFTER AUTENTIFICATION");
            }
            
        }

    }
}

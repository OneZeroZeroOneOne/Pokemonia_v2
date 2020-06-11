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
        private TypeCodeEnum _typeCode;
        private ByteMessageFactory _byteMessageFactory;
        private AuthorizePleaseHandler _authorizePleaseHandler;
        private BufferParser _bufferParser;
        private UserServiceDB _userServiceDB;
        public Dispatcher(UserConnection userConnection)
        {
            _bufferParser = new BufferParser();
            _userConnection = userConnection;
            _authorizePleaseHandler = new AuthorizePleaseHandler(_userConnection);
            _userServiceDB = new UserServiceDB();
            Run();
        }

        private void Run()
        {
            while(_userConnection.GetUser() == null)
            {
                _authorizePleaseHandler.SendAuthorizePlease();
                byte[] bytes = _userConnection.Receive();
                _bufferParser.Set(bytes);
                byte b1 = _bufferParser.GetByte();
                byte b2 = _bufferParser.GetByte();
                string s = _bufferParser.GetString();
                Console.WriteLine($"Context {b1}");
                Console.WriteLine($"Context from  ContextCodeEnum.Login {(byte)ContextCodeEnum.Login}");
                Console.WriteLine($"type {b2}");
                Console.WriteLine($"type from  (byte)TypeCodeEnum.LoginАttempt {(byte)TypeCodeEnum.LoginАttempt}");
                Console.WriteLine($"text {s}");
                if (b1 == (byte)ContextCodeEnum.Login & b2 == (byte)TypeCodeEnum.LoginАttempt) 
                {
                    string[] credentials = (s).Split(',');
                    _userConnection.SetUser(_userServiceDB.GetUser(credentials[0], credentials[1]));
                    _authorizePleaseHandler.SendSuccesAuthorize();
                }
            }
            while (true)
            {
                Console.WriteLine("WHILE AFTER AUNTENTIFICATION");
            }
            
        }

    }
}

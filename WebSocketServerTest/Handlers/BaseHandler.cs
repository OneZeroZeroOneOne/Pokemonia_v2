using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Utils;

namespace Pokemonia.WebServer.Handlers
{
    public class BaseHandler
    {
        protected UserConnection _userConnection;
        protected BufferParser _bufferParser;
        protected ByteMessageFactory _byteMessageFactory;
        public BaseHandler(UserConnection userConnection)
        {
            _userConnection = userConnection;
            _bufferParser = new BufferParser();
            _byteMessageFactory = new ByteMessageFactory();

        }
    }
}

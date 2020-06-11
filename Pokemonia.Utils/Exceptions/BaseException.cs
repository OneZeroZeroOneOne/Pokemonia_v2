using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Utils.Exceptions
{

    public class BaseException : Exception
    {
        int Code;
        string ExMessage;
        public BaseException(int code,string message)
            : base(message)
        {
            Code = code;
            ExMessage = message;

        }

        public override string ToString()
        {
            return String.Format("code:{0}, message:{1}", Code, ExMessage);
        }

    }
}

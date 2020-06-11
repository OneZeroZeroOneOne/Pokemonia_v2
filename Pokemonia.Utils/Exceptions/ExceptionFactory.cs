using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Utils.Exceptions
{
    public static class ExceptionFactory
    {
        public static BaseException SoftException(ExceptionEnum code, string message, params object[] args)
        {
            message = string.Format(message, args);
            return new BaseException((int)code, message);
        }

        public static BaseException SoftException(ExceptionEnum code, string message)
        {
            return new BaseException((int)code, message);
        }
    }
}

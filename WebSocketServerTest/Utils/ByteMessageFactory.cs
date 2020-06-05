using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemonia.Dal.Enumaration;

namespace Pokemonia.WebServer.Utils
{
    public class ByteMessageFactory
    {
        public byte[] RegisterBytes()
        {
            var header = CreateHeader(ContextCodeEnum.RegistrationContext, TypeCodeEnum.Register);
            byte[] msg = Encoding.UTF8.GetBytes("Register please");
            byte[] newByte = ConcateBytes(header, msg, msg.Length+ header.Length+1);
            newByte[msg.Length + header.Length] = (byte)TypeCodeEnum.NullTerminator;
            return newByte;
        }

        public byte[] CreateHeader(ContextCodeEnum context, TypeCodeEnum tp)
        {
            byte[] data = new byte[2];
            data[0] = (byte)context;
            data[1] = (byte)tp;
            return data;
        }

        public byte[] ConcateBytes(byte[] bytes, byte[] anotherBytes, long length)
        {
            byte[] newByte = new byte[length];
            uint index = 0;
            for(int i = 0; i < bytes.Length; i++)
            {
                newByte[index++] = bytes[i];
            }
            for (int i = 0; i < anotherBytes.Length; i++)
            {
                newByte[index++] = anotherBytes[i];
            }
            return newByte;
        }
    }
}

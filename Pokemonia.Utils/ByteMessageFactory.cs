using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemonia.Dal.Enumaration;

namespace Pokemonia.Utils
{
    public class ByteMessageFactory
    {
        public byte[] LoginBytes()
        {
            var header = CreateHeader(ContextCodeEnum.Login, TypeCodeEnum.Login);
            byte[] msg = Encoding.UTF8.GetBytes("Register please");
            byte[] newByte = ConcateBytes(header, msg);
            newByte[msg.Length + header.Length] = 0x0;
            return newByte;
        }

        public byte[] CreateHeader(ContextCodeEnum context, TypeCodeEnum tp)
        {
            byte[] data = new byte[2];
            data[0] = (byte)context;
            data[1] = (byte)tp;
            return data;
        }

        public byte[] ConcateBytes(byte[] bytes, byte[] anotherBytes)
        {
            byte[] newByte = new byte[bytes.Length+anotherBytes.Length];
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

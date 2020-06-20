using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemonia.Dal.Enumaration;

namespace Pokemonia.Utils
{
    public class ByteMessageFactory
    {
        public byte[] LoginPlease()
        {
            var header = CreateHeader(ContextCodeEnum.Login, TypeCodeEnum.LoginPlease);
            byte[] newByte = AddString(header, "Register please");
            return newByte;
        }

        public byte[] SuccesLogin()
        {
            var header = CreateHeader(ContextCodeEnum.Login, TypeCodeEnum.LoginSucces);
            byte[] newByte = AddString(header, "login succes");
            return newByte;
        }

        public byte[] AddedUser()
        {
            var header = CreateHeader(ContextCodeEnum.User, TypeCodeEnum.UserAddedOnMap);
            byte[] newByte = AddString(header, "User added on map");
            return newByte;
        }

        public byte[] DisconnectUserFromMap(int mapId)
        {
            var header = CreateHeader(ContextCodeEnum.User, TypeCodeEnum.DisconnectUserFromMap);
            byte[] newByte = AddString(header, $"User was disconnect from map, Id - {mapId}");
            return newByte;
        }

        public byte[] ChangeMap(int mapId)
        {
            var header = CreateHeader(ContextCodeEnum.User, TypeCodeEnum.UserChangeMap);
            byte[] newByte = AddString(header, $"User changed map, Id - {mapId}");
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
            Console.WriteLine($"byte L {bytes.Length}") ;
            Console.WriteLine($"anotherBytes L {anotherBytes.Length}");
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
            Console.WriteLine($"newbyte L {newByte.Length}");
            return newByte;
        }

        public byte[] AddString(byte[] bytes, string str)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            byte[] newBytes = new byte[bytes.Length + strBytes.Length + 1];
            uint index = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                newBytes[index++] = bytes[i];
            }
            for (int i = 0; i < strBytes.Length; i++)
            {
                newBytes[index++] = strBytes[i];
            }
            newBytes[bytes.Length + strBytes.Length] = 0x0;
            return newBytes;
        }


    }
}

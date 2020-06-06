using Pokemonia.Dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.Utils
{
    public class BufferParser
    {
        public byte[] _bytes;
        public int _index;
        public void Set(byte[] bytes)
        {
            _bytes = bytes;
            _index = 0;
        }
        public int GetInt32()
        {
            _index += 4;
            return BitConverter.ToInt32(_bytes, _index - 4);
        }
        public string GetString()
        {
            List<byte> strList = new List<byte>();
            while (_bytes[_index] != 0x0)
            {
                Console.WriteLine(_bytes[_index]);
                strList.Add(_bytes[_index]);
                _index += 1;
            }
            _index += 1;
            return Encoding.UTF8.GetString(strList.ToArray());
        }

        public byte GetByte()
        {
            return _bytes[_index++];
        }

    }
}

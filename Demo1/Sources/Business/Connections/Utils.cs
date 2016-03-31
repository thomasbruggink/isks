using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Connections
{
    public class Utils
    {
        public static string ToHexString(byte[] data)
        {
            var stringBuilder = new StringBuilder();
            foreach (var b in data)
            {
                stringBuilder.Append(ByteToChar((byte)(b >> 4)));
                stringBuilder.Append(ByteToChar((byte)(b & 0x0F)));
            }
            return stringBuilder.ToString();
        }

        public static byte[] FromHexString(string data)
        {
            var output = new byte[data.Length / 2];
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = (byte)((CharToByte(data[i * 2]) << 4) + CharToByte(data[(i * 2) + 1]));
            }
            return output;
        }

        private static char ByteToChar(byte b)
        {
            switch (b)
            {
                case 10:
                    return 'A';
                case 11:
                    return 'B';
                case 12:
                    return 'C';
                case 13:
                    return 'D';
                case 14:
                    return 'E';
                case 15:
                    return 'F';
                default:
                    return (char)('0' + b);
            }
        }

        private static byte CharToByte(char c)
        {
            switch (c)
            {
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                default:
                    return (byte)(c - '0');
            }
        }
    }
}

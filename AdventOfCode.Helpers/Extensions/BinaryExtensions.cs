using System;
using System.Text;

namespace AdventOfCode.Helpers.Extensions;

public static class BinaryExtensions
{
    public static byte[] ToBytes(this string text) => Encoding.UTF8.GetBytes(text);
    public static string ToHexString(this byte[] bytes)
    {
        var hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }

    public static byte[] HexStringToBytes(this string hexString)
    {
        byte[] bytes = new byte[hexString.Length / 2];
        for (int i = 0; i < hexString.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        return bytes;
    }
}

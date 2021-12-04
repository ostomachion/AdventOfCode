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


    private static readonly char[] defaultChars = "0123456789abcdefghijklmnopqrstuvwxyz".ToArray();
    public static string ToBase(this int n, int b, char[]? chars = null) => ToBase((long)n, b, chars);
    public static string ToBase(this long n, int b, char[]? chars = null)
    {
        chars ??= defaultChars;
        if (b < 2 || b > chars.Length)
            throw new ArgumentException("Invalid base.", nameof(b));

        int i = 64;
        char[] buffer = new char[i];

        do
        {
            buffer[--i] = chars[n % b];
            n /= b;
        }
        while (n > 0);

        var result = new char[32 - i];
        Array.Copy(buffer, i, result, 0, 64 - i);

        return new string(result);
    }

    public static long FromBase(this string n, int b, char[]? chars = null)
    {
        chars ??= defaultChars;
        if (b < 2 || b > chars.Length)
            throw new ArgumentException("Invalid base.", nameof(b));

        var value = 0L;
        for (int i = n.Length - 1; i >= 0; i--)
        {
            var v = chars.IndexOf(n[i]);
            value += (long)Math.Pow(b, i) * v ?? throw new Exception("Could not parse string.");
        }

        return value;
    }

    public static string ToBinary(this int n) => Convert.ToString(n, 2);
    public static string ToBinary(this long n) => Convert.ToString(n, 2);
    public static long FromBinary(this string n) => Convert.ToInt64(n, 2);
}

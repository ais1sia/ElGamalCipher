using System.Numerics;
using System.Text;

namespace ElGamalCipher;


public static class Converter
{

    public static byte[] HexFormat(string str)
    {
        byte[] bytes = new byte[str.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
        }

        return bytes;
    }
    
    public static string BigIntegerToHex(BigInteger number)
    {
        byte[] bytes = number.ToByteArray();
        Array.Reverse(bytes); // Odwrócenie kolejności bajtów
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
    
    public static string BigIntegerArrayToHexString(BigInteger[] numbers)
    {
        string[] hexStrings = new string[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            hexStrings[i] = numbers[i].ToString("X");
        }
        return string.Join("\n", hexStrings);
    }
    
    public static BigInteger[] ByteArrayToBigIntegerArray(byte[] bytes)
    {
        int byteCount = bytes.Length;
        int bigIntegerCount = byteCount % 32 == 0 ? byteCount / 32 : (byteCount / 32) + 1;

        BigInteger[] bigIntegers = new BigInteger[bigIntegerCount];
        for (int i = 0; i < bigIntegerCount; i++)
        {
            byte[] chunk = new byte[Math.Min(32, byteCount - (i * 32))];
            Array.Copy(bytes, i * 32, chunk, 0, chunk.Length);
            Array.Reverse(chunk); // Ensure little-endian order
            bigIntegers[i] = new BigInteger(chunk);
        }

        return bigIntegers;
    }
    
    public static byte[] BigIntegerArrayToByteArray(BigInteger[] bigIntegers)
    {
        byte[] byteArray = new byte[bigIntegers.Length * sizeof(int)];
        for (int i = 0; i < bigIntegers.Length; i++)
        {
            byte[] tempArray = bigIntegers[i].ToByteArray();
            Array.Reverse(tempArray);
            Array.Copy(tempArray, 0, byteArray, i * sizeof(int), sizeof(int));
        }
        return byteArray;
    }
    public static byte[] StringToByteArray(string input)
    {
        // Convert the string to a byte array using ASCII encoding
        byte[] byteArray = Encoding.ASCII.GetBytes(input);
        return byteArray;
    }

    // Converts an Array of bytes to hex String
    // Checks if every hex value has length of 2
    // If not this element gets an extra 0xFF
    public static string ByteArrayToHex(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    // Converts 1 byte to String form with complement to a given number <n>
    // e.g. nr = 8: (byte) 10 => (String) "00001010"
    public static string ByteToBin(byte oneByte, int nr)
    {
        var s = new StringBuilder(Convert.ToString(oneByte, 2));
        var length = nr - s.Length;
        for (int i = 0; i < length; i++)
        {
            s.Insert(0, "0");
        }

        return s.ToString();
    }

    // Converts 1 String of byte value to int array of binary values, e.g "10101010" => int[8]
    public static int[] BinStringToIntArr(string b)
    {
        return b.Select(c => int.Parse(c.ToString())).ToArray();
    }

    // Converts byte array to int array of binary values, e.g. byte[8] => int[64]
    public static int[] ByteArrToIntArr(byte[] input)
    {
        var result = new int[input.Length * 8];
        for (int i = 0; i < input.Length; i++)
        {
            var b = ByteToBin(input[i], 8).ToCharArray();
            for (int j = 0; j < 8; j++)
            {
                result[(8 * i) + j] = int.Parse(b[j].ToString());
            }
        }

        return result;
    }
}

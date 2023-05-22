using System.Numerics;
using System.Text;

namespace ElGamalCipher;


public static class Converter
{
 
    public static string BigIntegerToHex(BigInteger number)
    {
        byte[] bytes = number.ToByteArray();
        Array.Reverse(bytes); // Odwrócenie kolejności bajtów
        return BitConverter.ToString(bytes).Replace("-", "");
    }
    
    public static byte[] BigIntegerToByteArray(BigInteger number)
    {
        byte[] byteArray = number.ToByteArray();

        // Handle negative BigInteger values
        if (number.Sign == -1)
        {
            // If the most significant byte is zero, it will be removed during ToByteArray()
            // Add a leading zero byte to preserve the sign of the value
            if (byteArray[byteArray.Length - 1] == 0x00)
            {
                Array.Resize(ref byteArray, byteArray.Length + 1);
            }
            // Invert all the bytes to restore the original negative value
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte)~byteArray[i];
            }
        }

        // Reverse the byte order to match little-endian encoding
        Array.Reverse(byteArray);

        return byteArray;
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
    
  /*  public static byte[] BigIntegerArrayToByteArray(BigInteger[] bigIntegers)
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
    */
  public static byte[] BigIntegerArrayToByteArray(BigInteger[] numbers)
  {
      byte[] result = numbers.SelectMany(n => n.ToByteArray()).ToArray();
      return result;
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

}

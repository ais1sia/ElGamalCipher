using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace ElGamalCipher;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Podaj wiadomość do zaszyfrowania:");
        string input = Console.ReadLine();
        KeyGenerator x = new KeyGenerator();
        KeyPair keyPair = x.GenerateKeys(64);
        BigInteger[] cypher = ElGamalAlgorithm.Encrypt(Encoding.UTF8.GetBytes(input), keyPair);
        Console.WriteLine("P: " + Converter.BigIntegerToHex(keyPair.P));
        Console.WriteLine("G: " + Converter.BigIntegerToHex(keyPair.G));
        Console.WriteLine("X: " + Converter.BigIntegerToHex(keyPair.X));
        Console.WriteLine("Y: " +Converter.BigIntegerToHex(keyPair.Y));


        Console.WriteLine("Zaszyfrowano:");
        Console.WriteLine(Converter.BigIntegerArrayToHexString(cypher));
        Console.WriteLine("Deszyfrowanie:");
        byte[] text = ElGamalAlgorithm.Decrypt(cypher, keyPair);
        string decrypted = Encoding.UTF8.GetString(text);
        Console.WriteLine(decrypted);
    }
}
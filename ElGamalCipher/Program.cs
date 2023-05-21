using System.Numerics;
using System.Text;

namespace ElGamalCipher;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Podaj wiadomość do zaszyfrowania:");
        Console.WriteLine("Podaj wiadomość:");
        string input = Console.ReadLine();
        KeyPair x = KeyGenerator.GenerateKeys(1024);
        BigInteger[] cypher = ElGamalAlgorithm.Encrypt(Encoding.UTF8.GetBytes(input), x);

        Console.WriteLine("Zaszyfrowano:");
        Console.WriteLine(Converter.BigIntegerArrayToHexString(cypher));
        Console.WriteLine("Deszyfrowanie:");
        byte[] text = ElGamalAlgorithm.Decrypt(cypher, x);
        string decrypted = Encoding.UTF8.GetString(text);
        Console.WriteLine(decrypted);
    }
}
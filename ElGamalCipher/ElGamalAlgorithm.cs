using System;
using System.Numerics;

namespace ElGamalCipher;

public class ElGamalAlgorithm
{
    public static BigInteger[] Encrypt(byte[] message, KeyPair keyPair)
    {
        BigInteger p = keyPair.P;
        BigInteger g = keyPair.G;
        BigInteger y = keyPair.Y;

        BigInteger[] encryptedMessage = new BigInteger[message.Length * 2];

        for (int i = 0; i < message.Length; i++)
        {
            BigInteger k = keyPair.X;
            BigInteger a = BigInteger.ModPow(g, k, p);
            BigInteger b = (BigInteger.ModPow(y, k, p) * message[i]) % p;

            encryptedMessage[2 * i] = a;
            encryptedMessage[2 * i + 1] = b;
        }

        return encryptedMessage;
    }
    
    public static byte[] Decrypt(BigInteger[] encryptedMessage, KeyPair keyPair)
    {
        BigInteger p = keyPair.P;
        BigInteger x = keyPair.X;

        byte[] decryptedMessage = new byte[encryptedMessage.Length / 2];

        for (int i = 0; i < decryptedMessage.Length; i++)
        {
            BigInteger a = encryptedMessage[2 * i];
            BigInteger b = encryptedMessage[2 * i + 1];

            BigInteger sharedSecret = ModInverse(BigInteger.ModPow(a, x, p), p);
            BigInteger decryptedByte = (b * sharedSecret) % p;

            decryptedMessage[i] = (byte)decryptedByte;
        }

        return decryptedMessage;
    }
    
    // Obliczanie odwrotności modularnej liczby a modulo n
    private static BigInteger ModInverse(BigInteger a, BigInteger n)
    {
        BigInteger t = 0;
        BigInteger newT = 1;
        BigInteger r = n;
        BigInteger newR = a;

        while (newR != 0)
        {
            BigInteger quotient = r / newR;

            BigInteger tempT = newT;
            newT = t - quotient * newT;
            t = tempT;

            BigInteger tempR = newR;
            newR = r - quotient * newR;
            r = tempR;
        }

        if (r > 1)
        {
            throw new ArithmeticException("Liczba a nie ma odwrotności modularnej modulo n!");
        }

        if (t < 0)
        {
            t += n;
        }

        return t;
    }

}
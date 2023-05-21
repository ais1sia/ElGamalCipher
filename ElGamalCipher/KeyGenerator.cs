using System;
using System.Numerics;

namespace ElGamalCipher;

public class KeyPair
{
    public BigInteger P { get; set; } // Liczba pierwsza p
    public BigInteger G { get; set; } // Pierwiastek pierwotny g
    public BigInteger Y { get; set; } // Wartość y
    public BigInteger X { get; set; } // Klucz prywatny x
}
public class KeyGenerator
{
    private static Random random = new Random();

    public static KeyPair GenerateKeys(int keySizeInBits)
    {
        KeyPair keyPair = new KeyPair();
        keyPair.P = GeneratePrimeNumber(keySizeInBits);

        while (true)
        {
            keyPair.G = GenerateRandomInRange(3, keyPair.P + 1);

            CalculatePrimitiveRoot(keyPair.P);
            keyPair.X = GenerateRandomInRange(2, keyPair.P - 1);
            keyPair.Y = BigInteger.ModPow(keyPair.G, keyPair.X, keyPair.P);
            return keyPair;
        }
    }

    public static BigInteger CalculatePrimitiveRoot(BigInteger p)
    {
        BigInteger phi = p - 1;
        BigInteger[] primeFactors = Factorize(phi);

        for (BigInteger g = 2; g < p; g++)
        {
            bool isPrimitiveRoot = true;

            foreach (BigInteger primeFactor in primeFactors)
            {
                BigInteger power = phi / primeFactor;
                BigInteger result = BigInteger.ModPow(g, power, p);

                if (result == 1)
                {
                    isPrimitiveRoot = false;
                    break;
                }
            }

            if (isPrimitiveRoot)
                return g;
        }

        throw new Exception("No primitive root found for the given prime number.");
    }

    public static BigInteger[] Factorize(BigInteger n)
    {
        List<BigInteger> factors = new List<BigInteger>();
        BigInteger[] primes = SieveOfEratosthenes(1000);

        for (int i = 0; i < primes.Length; i++)
        {
            if (n % primes[i] == 0)
            {
                factors.Add(primes[i]);
                n /= primes[i];
            }
        }

        if (n > 1)
            factors.Add(n);

        return factors.ToArray();
    }

    public static BigInteger[] SieveOfEratosthenes(int n)
    {
        List<BigInteger> primes = new List<BigInteger>();
        //int limit = 2147483647;

        // Utwórz tablicę o rozmiarze limit+1 i wypełnij ją wartościami true
        bool[] sieve = new bool[n+1];
        for (int i = 0; i <= n; i++)
        {
            sieve[i] = true;
        }

        // Wykonaj sito Eratostenesa
        for (BigInteger p = 2; p * p <= n; p++)
        {
            if (sieve[(int)p])
            {
                // Jeśli p jest oznaczone jako pierwsze, to oznacz wszystkie wielokrotności p jako złożone
                for (BigInteger i = p * p; i <= n; i += p)
                {
                    sieve[(int)i] = false;
                }
            }
        }

        // Dodaj liczby pierwsze do tablicy wynikowej
        for (BigInteger p = 2; p <= n; p++)
        {
            if (sieve[(int)p])
            {   
                primes.Add(p);
            }
        }

        return primes.ToArray();
    }

    private static BigInteger GeneratePrimeNumber(int bits)
    {
        BigInteger prime;

        do
        {
            byte[] bytes = new byte[bits / 8];
            random.NextBytes(bytes);
            prime = new BigInteger(bytes);
            prime = BigInteger.Abs(prime);
        } while (!IsPrime(prime));

        return prime;
    }

    private static bool IsPrime(BigInteger number)
    {
        if (number <= 1)
            return false;

        if (number == 2 || number == 3)
            return true;

        if (number % 2 == 0 || number % 3 == 0)
            return false;

        BigInteger i = 5;
        BigInteger sqrt = Sqrt(number);

        while (i <= sqrt)
        {   Console.WriteLine(i);
            if (number % i == 0 || number % (i + 2) == 0)
                return false;

            i += 6;
        }

        return true;
    }

    private static BigInteger Sqrt(BigInteger number)
    {
        BigInteger sqrt = BigInteger.One;
        BigInteger lastSqrt;

        do
        {
            lastSqrt = sqrt;
            sqrt = (number / sqrt + sqrt) / 2;
        }
        while (sqrt < lastSqrt);

        return lastSqrt;
    }
    
    private static BigInteger GenerateRandomNumber(int bits)
    {
        byte[] bytes = new byte[bits / 8];
        random.NextBytes(bytes);
        bytes[bytes.Length - 1] &= 0x7F; // Ensure the highest bit is not set for a positive number
        return new BigInteger(bytes);
    }
    
    private static BigInteger GenerateRandomInRange(BigInteger min, BigInteger max)
    {
        int maxBytes = Math.Max(min.ToByteArray().Length, max.ToByteArray().Length);
        BigInteger result;
        byte[] bytes = new byte[maxBytes];

        do
        {
            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; // Ensure the highest bit is not set for a positive number
            result = new BigInteger(bytes);
        } while (result < min || result >= max);

        return result;
    }
}

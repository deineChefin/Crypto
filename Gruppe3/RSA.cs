using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
namespace Gruppe3
{
    public class RSA
    {
        public enum Mode
        {
            ENCRYPT,
            DECRYPT
        }

        // Random element should only be generated once
        private static readonly Random random = new Random();
        public RSAKey Key { get; }

        public RSA(RSAKey key)
        {
            this.Key = key;
        }

        public void start(string pathInput, string pathOutput, Mode mode)
        {
            switch (mode)
            {
                case Mode.ENCRYPT:
                    this.encrypt(pathInput, pathOutput);
                    break;
                case Mode.DECRYPT:
                    this.decrypt(pathInput, pathOutput);
                    break;
            }
        }

        private void encrypt(string pathInput, string pathOutput)
        {
            byte[] chunks = this.readPlaintext(pathInput);
            List<byte> chiffre = new List<byte>();
            int paddingLength = this.Key.NPubKey.ToByteArray().Length;
            for (var i = 0; i < chunks.Length; i++)
            {
                BigInteger m = new BigInteger(new byte[] { chunks[i] });
                BigInteger c = this.encrypt(m);
                chiffre.AddRange(this.oneIntegerToBytes(c, paddingLength));
            }
            this.writeBase64(pathOutput, chiffre.ToArray());
        }

        private void decrypt(string pathInput, string pathOutput)
        {
            byte[] chiffre = this.readBase64(pathInput);
            int paddingLength = this.Key.NPubKey.ToByteArray().Length;
            BigInteger[] encrypted = this.allBytesToAllIntegers(chiffre, paddingLength);
            List<byte> decrypted = new List<byte>();

            for (var i = 0; i < chiffre.Length; i++)
            {
                BigInteger m = this.decrypt(chiffre[i]);
                byte[] mBytes = m.ToByteArray();
                decrypted.Add(mBytes[0]);
            }
            this.writePlaintext(pathOutput, decrypted.ToArray());
        }

        public static RSAKey generateRSAKey()
        {
            // to be sure p and q are not the same with different ranges (incl. values)
            int q = RSA.getRandomPrimenumber(3, 10);
            int p = RSA.getRandomPrimenumber(11, 25);
            int N = p * q;
            int phiN = (p - 1) * (q - 1); // eulers phi function 
            int e = 0;

            do
            {
                // generates a number e between 2 and phiN - 1
                e = random.Next(2, phiN);
            } while (gcd(e, phiN) != 1);

            int d = RSA.modInverse(e, phiN);
            RSAKey key = new RSAKey(new BigInteger(e), new BigInteger(d), new BigInteger(N));
            return key;
        }

        /**
            return a prime number in the given boundries 
        */
        public static int getRandomPrimenumber(int lower = 0, int upper = 300)
        {
            int prime = 0;
            do
            {
                // lower is incl, upper is excl
                prime = RSA.random.Next(lower, upper + 1);
            } while (!RSA.isPrime(prime));

            return prime;
        }

        /**
            check if a number is a prime number
        */
        public static bool isPrime(int n)
        {
            // n must be positive 
            if (n <= 1)
            {
                return false;
            }

            // Check from 2 to n-1
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /**
            greatest common dividor 
        */
        public static int gcd(int i, int j)
        {
            while (j != 0)
            {
                int oldJ = j;
                j = i % oldJ; // (Math.Abs(i * j) + i) % j;
                i = oldJ;
            }
            return i;
        }

        /**
            private key = d = (1 % phiN) / e
            extended gcd
            calculates the greatest common divisor g of two numbers a and b 
            and additionally the coefficients u and v of a representation of g as an integer linear combination
            a = e, n = phiN 
        */
        public static int modInverse(int a, int n)
        {
            int n0 = n;
            int a0 = a;
            int y = 0, x = 1;

            if (n == 1)
                return 0;

            while (a > 1)
            {
                // q is quotient
                int q = a / n;
                int t = n;

                // phiN is remainder now, process, same as Euclid's algo
                n = a % n;
                a = t;
                t = y;

                // Update x and y
                y = x - q * y;
                x = t;
            }

            // Make x positive, adding phiN to make the result positive does not falsify the result, 
            // since the result is in the residual class ring mod phiN
            if (x < 0)
            {
                x += n0;
            }

            return x;
        }

        public byte[] readPlaintext(string path)
        {
            string allText = File.ReadAllText(path);
            return Encoding.ASCII.GetBytes(allText);
        }

        public byte[] readBase64(string path)
        {
            string allText = File.ReadAllText(path);
            return Convert.FromBase64String(allText);
        }

        public void writePlaintext(string path, byte[] bytes)
        {
            // per default the file is created in the current directory 
            string plaintext = Encoding.ASCII.GetString(bytes);
            File.WriteAllText(path, plaintext);
        }

        public void writeBase64(string path, byte[] bytes)
        {
            File.WriteAllText(path, Convert.ToBase64String(bytes));
        }

        private byte[] oneIntegerToBytes(BigInteger i, int paddingLength)
        {
            /*
                Number:
                0xABCDEF - 2 bytes

                In Memory:
                Big Endian: 0xAB 0xCD 0xEF
                Little Endian: 0xEF 0xCD 0xAB
            */
            List<byte> bytes = new List<byte>(i.ToByteArray(isBigEndian: true));
            while (bytes.Count < paddingLength)
            {
                bytes.Insert(0, 0);
            }
            return bytes.ToArray();
        }

        private BigInteger[] allBytesToAllIntegers(byte[] bytes, int paddingLength)
        {
            if (bytes.Length % paddingLength != 0)
            {
                System.Console.WriteLine(bytes.Length % paddingLength != 0);
                throw new Exception("Invalid input length");
            }

            List<BigInteger> allIntegers = new List<BigInteger>();
            List<byte> bytesHelper = new List<byte>(bytes);

            for (int i = 0; i < bytesHelper.Count; i += paddingLength)
            {
                BigInteger newInteger = new BigInteger(bytesHelper.GetRange(i, paddingLength).ToArray(), isBigEndian: true);
                allIntegers.Add(newInteger);
            }
            return allIntegers.ToArray();
        }

        /**
           c = m^e (mod N)
		   x**y mod |m|
        */
        public BigInteger encrypt(BigInteger input)
        {
            // Performs modulus division on a number raised to the power of another number
            return BigInteger.ModPow(input, this.Key.EPubKey, this.Key.NPubKey);
        }

        /** 
           m = c^d (mod N)
        */
        public BigInteger decrypt(BigInteger chiffre)
        {
            // Performs modulus division on a number raised to the power of another number.
            return BigInteger.ModPow(chiffre, this.Key.PrivateKey, this.Key.NPubKey);
        }
    }
}

using System.IO;
using System;
using System.Numerics;
namespace Gruppe3
{
    public class RSA
    {
        private Random random = new Random();

        public RSAKey Key {get;}

        public RSA() {
            this.Key = this.genarateRSAKey();
        }

        public void print()
        {
            Console.WriteLine(" RSA");
            this.Key.print();
            this.convertFileToChunks();
            // ent- oder verschlüsseln 
        }

        public RSAKey genarateRSAKey()
        {
            // fixed params
            int q = 13; 
            int p = 47; 
            int N = p * q; 
            int phiN = (p-1)*(q-1);
            int e;
            do {
                // generates a number e between 2 and phiN - 1 
                e = random.Next(2, phiN);
            }
            while (gcd(e, phiN) > 1);
         
            int k;
            do {
                // generates a number k between 2 and phiN - 1 
                k = random.Next(2, phiN);
            }
            while (gcd(e, k) != 1);          

            int d = this.modInverse(99, 78);
            
            return new RSAKey(e, d, N);
        }
           
        /**
            greatest common dividor 
        */
        public int gcd(int i, int j) {
            while (j != 0) {
                int oldJ = j; 
                j = i % j; 
                i = oldJ;
            }

            return i;
        }
        
        /**
            extended gcd
            calculates the greatest common divisor g of two numbers a and b 
            and additionally the coefficients u and v of a representation of g as an integer linear combination
        */
        public int[] extgcd(int a, int b) {
            // values defined by the algorithm 
            int u = 1; 
            int v = 0; 
            int s = 0; 
            int t = 1;

            while (b != 0) {
                int q = a / b;
                int tempB = a - q * b;
                a = b;
                b = tempB;
                int tempS = u - q * s;
                u = s;
                s = tempS;
                int tempT = v - q * t;
                v = t;
                t = tempT;
                   
            }

            return new int[]{a, u, v};
        }

        /**
            modinverse calculates the multiplicative inverse element a-1 of an element a in a group of integers k* 
        */
        public int modInverse(int a, int k) {
            // g, u, v 
            int[] extgcd = this.extgcd(a, k);
            return extgcd[1] % k; 
        }
        
        public string getFile() {    
            string path = "C:\\Users\\carin\\OneDrive\\Desktop\\test.txt";
            return path;
        }

        public void convertChunksToFile() {

        }

        public void convertFileToChunks() {
            byte[] allBytes = File.ReadAllBytes(this.getFile());
            
            foreach (var chunk in allBytes)
            {
                int encChunk = this.encrypt(chunk);
                int decChunk = this.decrypt(encChunk);
                System.Console.WriteLine("Chunk: {0} Enc: {1} Dec: {2}", chunk, encChunk, decChunk);
            }
        }

        /**
           c = m^e (mod N )
        */
        public int encrypt(int input)
        {   
            return (int)BigInteger.ModPow(input, this.Key.ePubKey, this.Key.NPubKey);
        }

        /**
           m = c^d (mod N)
        */
        public int decrypt(int chiffre)
        {
            return (int)BigInteger.ModPow(chiffre, this.Key.privateKey, this.Key.NPubKey);
        }

        public void sign()
        {
            // s = m^d (mod N)
            // quasi wie einen Tag dazu geben 
        }

        public void verify()
        {
            // m = s^e (mod N)
        }
    }
}

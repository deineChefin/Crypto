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
            int p = 11; 
            int N = p * q; 
            int phiN = (p-1)*(q-1);
            int e = 0;
            
            while(gcd(e, phiN) != 1){
               // generates a number e between 2 and phiN - 1 
               e = random.Next(2, phiN);
            }

            int d = modInverse(e, phiN);   

            return new RSAKey(e, d, N);
        }
           
        /**
            greatest common dividor 
        */
        public int gcd(int i, int j) {
            while (j != 0) {
                int oldJ = j; 
                j =  (Math.Abs(i * j) + i) % j;
                i = oldJ;
            }

            return i;
        }

    	/**
            extended gcd
            calculates the greatest common divisor g of two numbers a and b 
            and additionally the coefficients u and v of a representation of g as an integer linear combination
        */
        public int modInverse(int a, int m) {
			int m0 = m;
        	int y = 0, x = 1;
 
        	if (m == 1)
            	return 0;
 
        	while (a > 1) {
            	// q is quotient
            	int q = a / m;
 
            	int t = m;
 
            	// m is remainder now, process
            	// same as Euclid's algo
            	m = a % m;
            	a = t;
            	t = y;
 
            	// Update x and y
            	y = x - q * y;
            	x = t;
        	}
 
        	// Make x positive
        	if (x < 0)
            	x += m0;
 
        	return x;
        }

        public string getFile() {    
            string path = "C:\\Users\\carin\\OneDrive\\Desktop\\test.txt";
            return path;
        }

        public void convertChunksToFile(byte[] chiffre) {
            byte[] encrypted = new byte[chiffre.Length]; 
            for (var i = 0; i < chiffre.Length; i++)
            {
                encrypted[i] = (byte)this.decrypt(chiffre[i]);
            }

            File.WriteAllBytes(this.getFile()+"-enc", encrypted);
        }

        public void convertFileToChunks() {
            byte[] allBytes = File.ReadAllBytes(this.getFile());
            
            foreach (byte chunk in allBytes)
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
        {    // % isn't implemented properly (https://github.com/dotnet/docs/issues/4827, https://stackoverflow.com/questions/2691025/mathematical-modulus-in-c-sharp)
            double pow = Math.Pow(input, this.Key.ePubKey);
            double enc = (Math.Abs(pow * this.Key.NPubKey) + pow) % this.Key.NPubKey;
            return (int)enc;
        }

        /**
           m = c^d (mod N)
        */
        public int decrypt(int chiffre)
        {
            double pow = Math.Pow(chiffre, this.Key.privateKey);
            double dec = (Math.Abs(pow * this.Key.NPubKey) + pow) % this.Key.NPubKey; 
            return (int)dec;
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

using System.IO;
using System;
using System.Numerics;
namespace Gruppe3
{
    /* Neben dem Verschlüssln von Files auch eine Möglichkeit 
    1. Inhalt der Datei mit symmetrischen Algorithmus verschlüsseln
    2. Schlüssel der symmetrischen Verschlüsselung Asymmetrisch verschlüsseln
    3. In die erste Zeile oder letzte Zeile der Datei den verschlüsselten Schlüssel packen.
    */
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
            byte[] chiffre = this.convertFileToChunks();
            this.convertChunksToFile(chiffre);
            // ent- oder verschlüsseln auswählen
        }

        public RSAKey genarateRSAKey()
        {
            // fixed params
            int q = 23; 
            int p = 19; 
            // key must be greater than message --> a byte is max. 255 for our test 
            int N = p * q; 
            int phiN = (p-1)*(q-1);
            int e = 0;
            
			do {
                // generates a number e between 2 and phiN - 1
                e = random.Next(2, phiN);
            }
            while (!(gcd(e, phiN) == 1));

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
            // file richtig auswählen 
            string path = "C:\\Users\\carin\\OneDrive\\Desktop\\test.txt";
            return path;
        }

        public void convertChunksToFile(byte[] chiffre) {
            // Files öffnen testen 
            byte[] encrypted = new byte[chiffre.Length]; 
            for (var i = 0; i < chiffre.Length; i++)
            {
                encrypted[i] = (byte)this.decrypt(chiffre[i]);
            }

            File.WriteAllBytes(this.getFile()+"-enc", encrypted);
        }

        public byte[] convertFileToChunks() {
            byte[] allBytes = File.ReadAllBytes(this.getFile());             
            // System.Console.WriteLine("Chunk: {0} Enc: {1} Dec: {2}", 7, this.encrypt(7), this.decrypt(this.encrypt(7)));
            byte[] chiffre = new byte[allBytes.Length]; 
            for (var i = 0; i < allBytes.Length; i++)
            {
                BigInteger encChunk = this.encrypt(allBytes[i]);
                // chiffre[i] = (byte)this.encrypt(allBytes[i]);
                // BigInteger decChunk = this.decrypt(encChunk);
                System.Console.WriteLine("Chunk: {0} Enc: {1}", allBytes[i], encChunk);
            }

            return chiffre;
        }

        /**
           c = m^e (mod N )
		   x**y mod |m|
        */
        public BigInteger encrypt(int input)
        {   
			BigInteger mToPow = new BigInteger(input);
			BigInteger exp = new BigInteger(this.Key.ePubKey);
            BigInteger modN = new BigInteger(this.Key.NPubKey);
            BigInteger modRes = BigInteger.ModPow(mToPow, exp, modN);

            return modRes;
        }
        /** 
           m = c^d (mod N)
        */
        public BigInteger decrypt(BigInteger chiffre)
        {
			BigInteger cToPow = chiffre;
            BigInteger exp = new BigInteger(this.Key.privateKey);
            BigInteger modN = new BigInteger(this.Key.NPubKey);
            BigInteger modRes = BigInteger.ModPow(cToPow, exp, modN);

            return modRes;
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

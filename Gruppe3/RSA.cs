using System.Collections.Generic;
using System.IO;
using System;
using System.Numerics;
namespace Gruppe3
{
    /* Neben dem Verschlüssln von Files auch eine Möglichkeit 
    1. Inhalt der Datei mit symmetrischen Algorithmus verschlüsseln
    2. Schlüssel der symmetrischen Verschlüsselung Asymmetrisch verschlüsseln
    3. In die erste Zeile oder letzte Zeile der Datei den verschlüsselten Schlüssel packen.
        extgcg | modinvers 
        https://www.hsg-kl.de/faecher/inf/krypto/rsa/modinv/index.php 
        https://www.mathe-online.at/materialien/Franz.Embacher/files/RSA/Euklid.html 
    */
    public class RSA
    {
        private Random random = new Random();
        public RSAKey Key {get;}


        public RSA() {
            this.Key = this.genarateRSAKey();
            this.print();
            this.start();                       
        }

        public void print()
        {
            Console.WriteLine(" RSA");
            this.Key.print();
        }

        /**
            default mode is true for encryption 
            only pressing d will lead to decryption 
        */
        public bool getMode() {
            System.Console.WriteLine("Press d for decyrption - everything else will be encryption");
            ConsoleKey input = Console.ReadKey().Key; 
            if (input == ConsoleKey.D) {
                return false; 
            }
            
            return true;
        }

        public void start () {
            if (this.getMode()) {
                System.Console.WriteLine("encrypt");
                byte[] chunks = this.convertFileToChunks();            
                byte[] chiffre = new byte[chunks.Length]; 
                for (var i = 0; i < chunks.Length; i++)
                {
                    // chiffre[i] = (byte) this.encrypt(chunks[i]);
                    System.Console.WriteLine("Chunk: {0} Enc: {1}", chunks[i], this.encrypt(chunks[i]));
                }
            } else {
                System.Console.WriteLine("decrypt");
                byte[] chiffre = this.convertFileToChunks();
                byte[] encrypted = new byte[chiffre.Length]; 
                for (var i = 0; i < chiffre.Length; i++)
                {
                    encrypted[i] = (byte)this.decrypt(chiffre[i]);
                }

                string path = this.convertChunksToFile(chiffre);
            } 
        }

        public RSAKey genarateRSAKey()
        {
            // fixed params
            int q = 997; 
            int p = 929; 
            // key must be greater than message --> a byte is max. 255 for our test 
            int N = p * q; 
            int phiN = (p-1)*(q-1);
            int e = 0;
            
			do {
                // generates a number e between 2 and phiN - 1
                e = random.Next(2, phiN);
            }
            while (gcd(e, phiN) != 1);

            int d = this.modInverse(e, phiN);   

            return new RSAKey(e, (int)d, N);
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
            private key = d = (1 % phiN) / e
            extended gcd
            calculates the greatest common divisor g of two numbers a and b 
            and additionally the coefficients u and v of a representation of g as an integer linear combination
        */
        public int modInverse(int e, int phiN) {
			int phiN0 = phiN;
            int e0 = e; 
        	int y = 0, x = 1;
 
        	if (phiN == 1)
            	return 0;
 
        	while (e > 1 && phiN != 0) {
            	// q is quotient
            	int q = e / phiN;
            	int t = phiN;
 
            	// phiN is remainder now, process
            	// same as Euclid's algo
            	phiN = e % phiN;
            	e = t;
            	t = y;
 
            	// Update x and y
            	y = x - q * y;
            	x = t;
        	}
 
        	// Make x positive, adding phiN to make the result positive does not falsify the result, 
            // since the result is in the residual class ring mod phiN
        	if (x < 0) {
                x += phiN0;
            }

        	return x;
        }

        /**
            if the default path (for testing) doesn't exit 
            check if the file exists 
        */
        public string getFilepath() {    
            string path = "C:\\Users\\Carina\\Desktop\\test.txt"; // Standrechner 
            // string path = "C:\\Users\\carin\\OneDrive\\Desktop\\test.txt"; // Laptop 
            while (!File.Exists(path)){
                System.Console.WriteLine("Please enter the file path: ");
                path = Console.ReadLine();
                
                if (path.StartsWith('.')) {
                    path = Path.GetRelativePath(Directory.GetCurrentDirectory(), path);
                }

                if (File.Exists(path)) {
                    return path;
                }
            } 

            return path;
        }

        public string convertChunksToFile(byte[] allBytes) {
            // Files öffnen testen
            string path = this.getFilepath()+"-enc";
            File.WriteAllBytes(path, allBytes);
            return path; 
        }

        public byte[] convertFileToChunks() {
            string path = this.getFilepath();
            byte[] allBytes = File.ReadAllBytes(path);             
            return allBytes;
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

        /**
            TODO 
        */
        public void sign()
        {
            // s = m^d (mod N)
            // quasi wie einen Tag dazu geben 
        }

        /**
            TODO 
        */
        public void verify()
        {
            // m = s^e (mod N)
        }
    }
}

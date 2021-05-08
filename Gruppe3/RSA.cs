using System;
using System.IO;
using System.Numerics;
using System.Text;
namespace Gruppe3
{
    public class RSA
    {
        // Random element should only be generated once
        private readonly Random random = new Random();
        public RSAKey Key {get;}
        public bool Mode {get; set; }

        public RSA() {
            this.Key = this.genarateRSAKey();
            this.Mode = this.getMode();                     
            this.print();
        }

        public void print()
        {
            this.Key.print();
            Console.WriteLine("Mode is enc {0} | dec {1}", this.Mode, !this.Mode);
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
            if (this.Mode) { // encrypt 
                char[] chunks = this.convertTextFileToChunks();            
                BigInteger[] chiffre = new BigInteger[chunks.Length]; 
                for (var i = 0; i < chunks.Length; i++)
                {
                    chiffre[i] = this.encrypt(chunks[i]);
                }

                string path = this.convertChunksToTextFile(chiffre);
            } else { // decrypt
                char[] chiffre = this.convertTextFileToChunks();
                BigInteger[] encrypted = new BigInteger[chiffre.Length]; 
                for (var i = 0; i < chiffre.Length; i++)
                {
                    encrypted[i] = this.decrypt(chiffre[i]);
                }

                string path = this.convertChunksToTextFile(encrypted);
            } 
        }

        public RSAKey genarateRSAKey()
        {
            // to be sure p and q are not the same with different ranges (incl. values)
            int q = this.getRandomPrimenumber(300, 599); 
            int p = this.getRandomPrimenumber(600, 899); 
            int N = p * q; 
            int phiN = (p-1)*(q-1); // eulers phi function 
            int e = 0;
            
			do {
                // generates a number e between 2 and phiN - 1
                e = random.Next(2, phiN);
            } while (gcd(e, phiN) != 1);

            int d = this.modInverse(e, phiN);   

            return new RSAKey(e, (int)d, N);
        }
           

        /**
            return a prime number in the given boundries 
        */ 
        public int getRandomPrimenumber(int lower = 0, int upper = 300) {
            int prime = 0;
            do { 
                // lower is incl, upper is excl
                prime = this.random.Next(lower, upper + 1); 
            } while (!this.isPrime(prime)); 

            return prime; 
        }   

        /**
            check if a number is a prime number
        */
        public bool isPrime(int n)
        {
            // n must be positive 
            if (n <= 1) {
                return false;
            }
            
            // Check from 2 to n-1
            for (int i = 2; i < n; i++) {
                if (n % i == 0) {
                    return false;
                }
            } 
    
            return true;
    }

        /**
            greatest common dividor 
        */
        public int gcd(int i, int j) {
            while (j != 0) {
                int oldJ = j; 
                j =  i % oldJ; // (Math.Abs(i * j) + i) % j;
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
        public int modInverse(int a, int n) {
			int n0 = n;
            int a0 = a; 
        	int y = 0, x = 1;
 
        	if (n == 1)
            	return 0;
 
        	while (a > 1) {
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
        	if (x < 0) {
                x += n0;
            }

        	return x;
        }

        /**
            if the default path (for testing) doesn't exit 
            check if the file exists 
        */
        public string getFilepath() {    
            // string path = "C:\\Users\\Carina\\Desktop\\test.txt"; // Standrechner 
            string path = "C:\\Users\\carin\\OneDrive\\Desktop\\test.txt-enc"; // Laptop 
            while (!File.Exists(path)) {
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

        public string convertChunksToTextFile(BigInteger[] chunks) {
            // Files öffnen testen
            string allLines = String.Empty;  
            for (var i = 0; i < chunks.Length; i++)
            {   
                
                allLines += chunks[i].ToString(); 
            }

            string path = this.Mode ? "gruppe3-enc.txt" : "gruppe3-dec.txt";
            // per default the file is created in the current directory 
            File.WriteAllText(path, allLines, Encoding.ASCII);
            return path; 
        }

        public char[] convertTextFileToChunks() {
            string allText = File.ReadAllText(this.getFilepath());
            string chunks = String.Empty;     
            for (var j = 0; j < allText.Length; j++)
            {
                chunks += allText[j];
            }    
 
            return chunks.ToCharArray();
        }

        /**
           c = m^e (mod N)
		   x**y mod |m|
        */
        public BigInteger encrypt(BigInteger input)
        {   
			BigInteger mToPow = input;
			BigInteger exp = new BigInteger(this.Key.EPubKey);
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
            BigInteger exp = new BigInteger(this.Key.PrivateKey);
            BigInteger modN = new BigInteger(this.Key.NPubKey);
            BigInteger modRes = BigInteger.ModPow(cToPow, exp, modN);

            return modRes;
        }

        /**
            TODO 
            like encrypt 
        */
        public void sign()
        {
            // s = m^d (mod N)
            // adding Tag 
        }

        /**
            TODO 
            like decrypt 
        */
        public void verify()
        {
            // m = s^e (mod N)
        }
    }
}

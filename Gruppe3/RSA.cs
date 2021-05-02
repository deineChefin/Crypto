using System.IO;
using System;
namespace Gruppe3
{
    public class RSA
    {
        private Random random = new Random();
        // Welches Files 
        // private FileStream fileStream = File.Open("C:\\Users\\carin\\OneDrive\\Desktop\\test.txt", FileMode.OpenOrCreate);
        // ent- oder verschlüsseln 
        public void print()
        {
            Console.WriteLine("1) asymmetrische Verschlüsselung (RSA)");
            RSAKey key = this.genarateRSAKey();
            key.print();
        }

        public void parGen()
        {
            // für zyklische GRuppe wichtig 
            // einigen auf parametern 

            // man braucht d und (e,N)
            // p und q brauchen wir nicht mehr --> muss man wegschmeißen 
            // so kennt man die Faktorisierung nicht mehr erechnen /erkennen 
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
                // generates a number between 2 and phiN - 1 
                e = random.Next(2, phiN);
            }
            while (gcd(e, phiN) > 1);
        
            // transform the formula e*d = 1 (mod phiN)
            int d = (1 + phiN) / e;
            
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
        
        public void hash(string input)
        {
            // schon von der anderen Aufgabe verwenden 
            MD5 md5 = new MD5();
            // Wo brauche ich das im RSA? 
        }

        public void encrypt(string enc, string sender)
        {
            // c = m^e (mod N )
        }

        public void decrypt(string dec, string receiver)
        {
            // m = c^d (mod N)
        }

        // Für unterschiedliche Aufgaben, unterschiedliche Schlüssel 
        // sonst würde man ein verschlüsselte Nachricht anstatt zu signieren, entschlüsseln! 
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
using System.IO;

namespace Gruppe3
{
    public class RSA
    {
        // Welches Files 
        private FileStream file = FileStream.Open("C:\\Users\\carin\\OneDrive\\Desktop\\test.txt", FileMode.OpenOrCreate);
        // ent- oder verschlüsseln 
        public void print()
        {
            Console.WriteLine("1) asymmetrische Verschlüsselung (RSA)");
        }

        public void parGen()
        {
            // für zyklische GRuppe wichtig 
            // einigen auf parametern 

            // man braucht d und (e,N)
            // p und q brauchen wir nicht mehr --> muss man wegschmeißen 
            // so kennt man die Faktorisierung nicht mehr erechnen /erkennen 
        }

        public void keyGen()
        {
            int q = 13; 
            int p = 47; 
            int N = p * q; 
            int phiN = (p-1)*(q-1);
            Random random = new Math.Random();
            do {
                // generates a number between 2 and phiN - 1 
                int e = random.next(2, phiN);
            }
            while (gcd(e, phiN) != 1);
            
            do {
                // generates a number between 2 and phiN - 1 
                int d = random.next(2, phiN);
            } while (e * d != (1 % phiN));

            RSAKey key = new RSAKey(e, d, N);
        }

        // greatest common divior 
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
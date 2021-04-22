using System;

namespace Gruppe3
{
    /*
    Links: 
        https://studyflix.de/informatik/rsa-verschlusselung-1608 
        https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa?view=net-5.0 

        https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsacryptoserviceprovider?view=net-5.0

        Pseudocode.. 
        https://www.educative.io/edpresso/what-is-the-rsa-algorithm 
        https://eli.thegreenplace.net/2019/rsa-theory-and-implementation/ 
        https://www.geeksforgeeks.org/rsa-algorithm-cryptography/
        http://koclab.cs.ucsb.edu/teaching/cren/project/2018/Adamczyk+Magnussen.pdf 
        http://www.crypto-uni.lu/jscoron/cours/mics3crypto/m3.pdf
        https://scialert.net/fulltext/?doi=jas.2006.482.510 
        https://de.wikipedia.org/wiki/RSA-Kryptosystem 

        Stackoverflow.. 
        https://stackoverflow.com/questions/7809490/rsa-elgamal-pseudocode

        Für Primzahlen.. 
        https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test 
        
        Für Padding.. 
        https://en.wikipedia.org/wiki/Optimal_asymmetric_encryption_padding 

        Tipps: 
         - Zum Berechnen vom Multiplikativem Inversem nimmt man den erweiterten euklidicschen Algorithmus
         - Wertebereiche genau definieren 
    */

    /* Steps (wie notiert) vergleichen mit seinen Unterlagen 
        1. Schlüssel berechnen 
        2. RSA verschlüsseln 
        3. RSA entschlüsseln 
    */

    /*
        - Wie viel dürfen wir ein schränken und was muss gehen? 
        - Welche Art von Input ist zu erwarten? 
    */

    /*
    RSA implementation in linux kernel
    https://github.com/torvalds/linux/blob/master/crypto/rsa.c
    */

    public class RSA
    {
        /**
            - LEHRBUCH RSA (nicht sicher, aber programmierbar)
            - message muss kleiner als der Schlüssel! 
            - wenn länger dann muss man das aufteilen -> hybride Lösungen 
            - 
        */
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
            // schon von der anderen Aufgabe verwenden 
            KeyGen keygenerator = new KeyGen();
            // private und public key ermitteln 

            // Ablauf in den Folien beschrieben 
        }

        public void hashen(string input)
        {
            // schon von der anderen Aufgabe verwenden 
            MD5 md5 = new MD5();
            // Wo brauche ich das im RSA? 
        }

        public void encrypt(string enc, string sender)
        {
            // c = m^e (mod N )
        }

        public void decrypt(string dec, string empfaenger)
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
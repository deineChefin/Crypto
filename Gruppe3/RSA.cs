using System;

namespace Gruppe3
{
    /*
    Links: 

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
    */

    public class RSA
    {
        public void print()
        {
            Console.WriteLine("1) asymmetrische Verschlüsselung (RSA)");          
        }

        public void keyGen(){
            // schon von der anderen Aufgabe verwenden 
            KeyGen keygenerator = new KeyGen();
        }

        public void hashen(string input){
            // schon von der anderen Aufgabe verwenden 
            MD5 md5 = new MD5();
        }

        public void encrypt()
        {

        }

        public void decrypt()
        {

        }
    }
}
using System;
using System.Threading;
using System.IO;

namespace Gruppe3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RSAKey key = RSA.generateRSAKey(); 
            // RSAKey key = new RSAKey(79, 127, 201);
            Console.WriteLine(key);
            RSA rsa = new RSA(key);
            string pathInput = @"C:\Users\carin\OneDrive\Desktop\laRSA.txt";
            string pathOutput = @"C:\temp\Crypto\Gruppe3\gruppe3-enc.txt";

            while (true)
            {
                Console.WriteLine(@"
Gruppe 3 (Aschauer, Fraißl, Stadler, Sutrich)
=============================================
1) asymmetrische Verschlüsselung (RSA)
2) asymmetrische Entschlüsselung (RSA)
3) symmetrische Verschlüsselung (AES)
4) symmetrische Entschlüsselung (AES)
5) MD5 Hash

Auswahl: ");

                switch (Console.ReadKey(true).Key) 
                {
                    case ConsoleKey.D1:
                        // pathInput = @"C:\Users\carin\OneDrive\Desktop\test.txt"; 
                        // pathOutput = @"C:\temp\Crypto\Gruppe3\gruppe3-enc.txt"; 
                        pathInput = Program.getFilepath("zu verschlüssende Datei");
                        pathOutput = Program.getFilepath("verschlüsselte Datei"); 
                        rsa.start(pathInput, pathOutput, RSA.Mode.ENCRYPT);
                        break;
                    case ConsoleKey.D2:
                        // pathInput = @"C:\temp\Crypto\Gruppe3\gruppe3-enc.txt"; 
                        // pathOutput = @"C:\temp\Crypto\Gruppe3\gruppe3-dec.txt";  
                        pathInput = Program.getFilepath("zu entschlüssende Datei");
                        pathOutput = Program.getFilepath("entschlüsselte Datei"); 
                        // System.Console.WriteLine(key);
                        rsa.start(pathInput, pathOutput, RSA.Mode.DECRYPT);
                        break;
                    case ConsoleKey.D3:
                        AES aesEnc = new AES();
                        Console.WriteLine("\n" + "Dateipfad eingeben, der mit AES verschlüsselt werden soll: ");
                        string fileToEncrypt = Console.ReadLine();
                        string fileEncrypted = Path.ChangeExtension(fileToEncrypt, ".enc");
                        aesEnc.EncryptFile(fileToEncrypt, fileEncrypted, "TESTTESTTESTTESTTESTTESTTESTTEST", "0123456789ABCDEF");  // 32-bit key and 16-bit IV
                        Console.WriteLine("\n" + "Ihre mit AES verschlüsselte Datei wurde in " + fileEncrypted + " gespeichert.");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D4:
                        AES aesDec = new AES();
                        Console.WriteLine("\n" + "Dateipfad eingeben, der mit AES entschlüsselt werden soll: ");
                        string fileToDecrypt = Console.ReadLine();
                        string fileDecrypted = Path.ChangeExtension(fileToDecrypt, ".dec");
                        aesDec.DecryptFile(fileToDecrypt, fileDecrypted, "TESTTESTTESTTESTTESTTESTTESTTEST", "0123456789ABCDEF");  // 32-bit key and 16-bit IV
                        Console.WriteLine("\n" + "Ihre mit AES entschlüsselte Datei wurde in " + fileDecrypted + " gespeichert.");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D5:
                        MD5 md5 = new MD5();
                        Console.WriteLine("\n" + "Dateipfad angeben, von der eine MD5-Checksumme gemacht werden soll: ");
                        string input = Console.ReadLine();
                        md5.hash_file(input);
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Auf Wiedersehen!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n Esc zum Beenden.");
                    break;
                }
            }
        }

        /**
            if the default path (for testing) doesn't exit 
            check if the file exists 
        */
        private static string getFilepath(string goal)
        {
            string path = String.Empty;
            while (!File.Exists(path))
            {
                System.Console.WriteLine("Pfad für {0} eingeben: ", goal);
                path = Console.ReadLine();

                if (File.Exists(path))
                {
                    return path;
                } else {
                    System.Console.WriteLine("Die angegebene Datei existiert nicht.");
                }
            }
            return path;
        }
    }
}
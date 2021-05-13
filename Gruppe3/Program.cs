using System.Globalization;
using System.Net.Mime;
using System.Collections.Specialized;
using System;
using System.IO;
using System.Threading;

namespace Gruppe3
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine(@"
Ihre ehrenwerte Gruppe (5-2) stellt Ihnen dieses Beispiel kostenlos für 30 Tage zur Verfügung.
======================================
1) asymmetrische Verschlüsselung (RSA)
2) MD5 Hash
3) symmetrische Verschlüsselung (AES)
4) symmetrische Entschlüsselung (AES)

Ihre hochachtungsvolle Auswahl:
");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        RSA rsa = new RSA();
                        rsa.start();
                        break;
                    case ConsoleKey.D2:
                        MD5 md5 = new MD5();
                        md5.print();
                        Console.WriteLine("\n" + "Text eingeben, der mit MD5-Algorithmus gehasht werden soll: ");
                        string input = Console.ReadLine();
                        md5.HashMD5(input);
                        break;
                    case ConsoleKey.D3:
                        AES aesEnc = new AES();
                        Console.WriteLine("\n" + "Dateipfad eingeben, der mit AES verschlüsselt werden soll: ");
                        string fileToEncrypt = Console.ReadLine();  // TODO: exception handling
                        string fileEncrypted = Path.ChangeExtension(fileToEncrypt, ".enc");
                        aesEnc.EncryptFile(fileToEncrypt, fileEncrypted, "23432343234323432343234323432343", "1234123412341234");  // 32-bit key and 16-bit IV
                        Console.WriteLine("\n" + "Ihre mit AES verschlüsselte Datei wurde in " + fileEncrypted + " gespeichert.");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D4:
                        AES aesDec = new AES();
                        Console.WriteLine("\n" + "Dateipfad eingeben, der mit AES entschlüsselt werden soll: ");
                        string fileToDecrypt = Console.ReadLine();  // TODO: exception handling
                        string fileDecrypted = Path.ChangeExtension(fileToDecrypt, ".dec");
                        aesDec.DecryptFile(fileToDecrypt, fileDecrypted, "23432343234323432343234323432343", "1234123412341234");  // 32-bit key and 16-bit IV
                        Console.WriteLine("\n" + "Ihre mit AES entschlüsselte Datei wurde in " + fileDecrypted + " gespeichert.");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D5:
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
    }
}

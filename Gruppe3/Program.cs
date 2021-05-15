﻿using System;
using System.Threading;
using System.IO;

namespace Gruppe3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RSAKey key = RSA.generateRSAKey(); 
            Console.WriteLine(key);
            RSA rsa = new RSA(key);
            string pathInput = @"C:\Users\carin\OneDrive\Desktop\laRSA.txt";
            string pathOutput = @"C:\temp\Crypto\Gruppe3\gruppe3-enc.txt";

            while (true)
            {
                Console.WriteLine(@"
Gruppe 3 (Aschauer, Fraißl, Stadler, Sutrich)
===============================================
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
                        pathInput = Program.getFilepath("input");
                        pathOutput = Program.getFilepath("output"); 
                        rsa.start(pathInput, pathOutput, RSA.Mode.ENCRYPT);
                        break;
                    case ConsoleKey.D2:
                        // pathInput = @"C:\temp\Crypto\Gruppe3\gruppe3-enc.txt"; 
                        // pathOutput = @"C:\temp\Crypto\Gruppe3\gruppe3-dec.txt";  
                        pathInput = Program.getFilepath("input");
                        pathOutput = Program.getFilepath("output"); 
                        // System.Console.WriteLine(key);
                        rsa.start(pathInput, pathOutput, RSA.Mode.DECRYPT);
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
                        MD5 md5 = new MD5();
                        md5.print();
                        Console.WriteLine("\n" + "Text eingeben, der mit MD5-Algorithmus gehasht werden soll: ");
                        string input = Console.ReadLine();
                        md5.HashMD5(input);
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
                System.Console.WriteLine("Please enter the complete file path for {0}: ", goal);
                path = Console.ReadLine();

                if (File.Exists(path))
                {
                    return path;
                }
            }
            return path;
        }
    }
}
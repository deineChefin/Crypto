using System.Globalization;
using System.Net.Mime;
using System.Collections.Specialized;
using System;
using System.IO;

namespace Gruppe3
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n*** GRUPPE 3 *** \n1) asymmetrische Verschlüsselung (RSA) \n2) MD5 Hash\n3) Key generieren \n4) symmetrische Verschlüsselung (AES)\n\nAuswahl:" + "\n");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        RSA rsa = new RSA();
                        rsa.start();
                        break;
                    case ConsoleKey.D2:
                        MD5 md5 = new MD5();
                        md5.print();
                        Console.WriteLine("\n" + "Enter text to be hashed with MD5 algorithm: ");
                        string input = Console.ReadLine();
                        md5.HashMD5(input);
                        break;
                    case ConsoleKey.D3:
                        KeyGen password = new KeyGen();
                        string key = password.Generate();
                        Console.WriteLine(key);
                        break;
                    case ConsoleKey.D4:
                        AES aes = new AES();
                        Console.WriteLine("\n" + "Enter text that needs to be encrypted..");  
                        string data = Console.ReadLine();  
                        aes.encryptAesManaged(data);
                        // TODO: take any file as input and generate encrypted byte array
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("\nAuf Wiedersehen!");
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

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
                Console.WriteLine("*** GRUPPE 3 *** \n 1) asymmetrische Verschlüsselung (RSA) \n2) MD5 Hash\n3) Key generieren \n4) symmetrische Verschlüsselung (?)\n\nAuswahl:");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        RSA rsa = new RSA();
                        rsa.print();
                        break;
                    case ConsoleKey.D2:
                        System.Console.WriteLine("\nMD5 Hash");
                        break;
                    case ConsoleKey.D3:
                        System.Console.WriteLine("\nKey generieren");
                        break;
                    case ConsoleKey.D4:
                        System.Console.WriteLine("\nsymmetrische Verschlüsselung (?)");
                        break;
                    case ConsoleKey.Escape:
                        System.Console.WriteLine("\nAuf Wiedersehen!");
                        Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine("\nEsc zum Beenden.");
                        break;
                }
            }
        }
    }
}

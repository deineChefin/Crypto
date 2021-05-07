using System;
using System.IO;  
using System.Security.Cryptography;

namespace Gruppe3
{
    public class AES
    {
        // https://www.c-sharpcorner.com/article/aes-encryption-in-c-sharp/

        /*
        AES implementation in linux kernel
        https://github.com/torvalds/linux/blob/master/crypto/aes_generic.c
        */
        
        public void encryptAesManaged(string raw) {
            try {  
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    
                using(AesManaged aes = new AesManaged()) {  
                    // Encrypt string    
                    byte[] encrypted = encrypt(raw, aes.Key, aes.IV);  
                    // Print encrypted string    
                    Console.WriteLine("Encrypted data: ");
                    Console.WriteLine(System.Text.Encoding.UTF8.GetString(encrypted));
                    // Decrypt the bytes to a string.    
                    string decrypted = decrypt(encrypted, aes.Key, aes.IV);  
                    // Print decrypted string. It should be same as raw data      
                    Console.WriteLine("Decrypted data: ");
                    Console.WriteLine(decrypted);
                }  
            } catch (Exception exp) {  
                Console.WriteLine(exp.Message);  
            }  
            
            Console.WriteLine("\n" + "Press any key to resume to the menu!");
            Console.ReadKey();  
        }  

        static byte[] encrypt(string plainText, byte[] Key, byte[] IV) {  
            byte[] encrypted;  
            // Create a new AesManaged.    
            using(AesManaged aes = new AesManaged()) {  
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);  
                // Create MemoryStream    
                using(MemoryStream ms = new MemoryStream()) {  
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {  
                        // Create StreamWriter and write data to a stream    
                        using(StreamWriter sw = new StreamWriter(cs))  
                        sw.Write(plainText);  
                        encrypted = ms.ToArray();  
                    }  
                }  
            }  
            // Return encrypted data    
            return encrypted;  
        }  

        static string decrypt(byte[] cipherText, byte[] Key, byte[] IV) {  
            string plaintext = null;  
            // Create AesManaged    
            using(AesManaged aes = new AesManaged()) {  
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);  
                // Create the streams used for decryption.    
                using(MemoryStream ms = new MemoryStream(cipherText)) {  
                    // Create crypto stream    
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {  
                        // Read crypto stream    
                        using(StreamReader reader = new StreamReader(cs))  
                        plaintext = reader.ReadToEnd();  
                    }  
                }  
            }  

            return plaintext;  
        }     

    }
}
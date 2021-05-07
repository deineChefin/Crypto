using System;
using System.Collections.Generic;
using System.Linq;

namespace Gruppe3
{

    //https://www.codeproject.com/Articles/264077/Keygen-for-application
    //https://www.c-sharpcorner.com/forums/how-to-generate-unique-producat-key-for-every-pc-for-c-sharp

    public class KeyGen
    {
        //public void genKey(string input){ }
        private readonly string lowercase = "abcdefghijklmnopqrstuvwxyz";
        private readonly string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly string numbers = "1234567890";
        private readonly string symbols = "!@#$%^&_-+=<,>.?/";

        private Random random = new System.Random();

        /// <summary>
        /// Method to generate password
        /// </summary>
        /// <param name="length">The length of the key represented by an integer</param>
        /// <param name="hasLowercase">Boolean that represents if the string contains lowercase letters</param>
        /// <param name="hasUppercase">Boolean that represents if the string contains uppercase letters</param>
        /// <param name="hasNumbers">Boolean that represents if the string contains numbers</param>
        /// <param name="hasSymbols">Boolean that represents if the string contains symbols</param>
        public string Generate(int length = 16, bool hasLowercase = true, bool hasUppercase = true, bool hasNumbers = true, bool hasSymbols = true)
        {
            string validCharacters = "";
            char[] password = new char[length];
            if (hasLowercase)
            {
                validCharacters += lowercase;
            }
            if (hasUppercase)
            {
                validCharacters += uppercase;
            }
            if (hasNumbers)
            {
                validCharacters += numbers;
            }
            if (hasSymbols)
            {
                validCharacters += symbols;
            }

            if (validCharacters is not "")
            {
                for (int i = 0; i < password.Length; i++)
                {
                    password[i] = validCharacters[random.Next(validCharacters.Length)];
                }
            }
            else
            {
                for (int i = 0; i < password.Length; i++)
                {
                    password[i] = ' ';
                }
            }
            return String.Concat(password);
        }

    }


}
using System;
using System.IO;
using System.Security.Cryptography;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymetricEncrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the RSA algorithm class  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            // Get the public keyy   
            string publicKey = rsa.ToXmlString(false); // false to get the public key   
            string privateKey = rsa.ToXmlString(true); // true to get the private key   
            Console.WriteLine(publicKey);

            // Call the encryptText method   
            byte[] Encrypt = EncryptText(publicKey, "Hello from C# Corner", "encryptedData.dat");

            // Call the decryptData method and print the result on the screen   
            Console.WriteLine("Decrypted message: {0}", DecryptData(privateKey, Encrypt));

            Console.ReadKey();

        }

        // Create a method to encrypt a text and save it to a specific file using a RSA algorithm public key   
        static byte[] EncryptText(string publicKey, string text, string fileName)
        {
            // Convert the text to an array of bytes   
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(text);

            // Create a byte array to store the encrypted data in it   
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Set the rsa pulic key   
                rsa.FromXmlString(publicKey);

                // Encrypt the data and store it in the encyptedData Array   
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }
            // Save the encypted data array into a file   
            File.WriteAllBytes(fileName, encryptedData);


            Console.WriteLine("Data has been encrypted\n");
            string ret = string.Empty;
            for(int i = 0; i<encryptedData.Length;i++)
            {
                Console.Write(Convert.ToChar(encryptedData[i]));
                ret += Convert.ToChar(encryptedData[i]);
            }
            Console.WriteLine();
            Console.WriteLine(ret);
            return encryptedData;
        }

        // Method to decrypt the data withing a specific file using a RSA algorithm private key   
        static string DecryptData(string privateKey, byte[] dataToDecrypt)
        {
            // read the encrypted bytes from the file   
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            //byte[] dataToDecrypt = byteConverter.GetBytes(text);
            //byte[] dataToDecrypt = File.ReadAllBytes(fileName);

            // Create an array to store the decrypted data in it   
            byte[] decryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Set the private key of the algorithm   
                rsa.FromXmlString(privateKey);
                decryptedData = rsa.Decrypt(dataToDecrypt, false);
            }

            // Get the string value from the decryptedData byte array   
            byteConverter = new UnicodeEncoding();
            return byteConverter.GetString(decryptedData);
        }
    }
}

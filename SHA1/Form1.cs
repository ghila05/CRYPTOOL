﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography; //libreria importata
using System.IO;// libreria cript-encript

namespace SHA1
{

   

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            textBox2.Text = generaHash(s);
        }
        public string generaHash(string input) // FUNZIONE GENERA HASH CON SHA1
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(Encoding.UTF8.GetBytes(input)); //sfrutta il computer per creare l'hash 
            byte[] array = sh.Hash; // crea array di byte 
            StringBuilder sb = new StringBuilder();


            foreach (byte b in array)//costrutto foreach, prende ogni byte 
            {
                sb.Append(b.ToString("x2"));//x2 converte i caratteri in minuscolo 
            }


            return sb.ToString();

        }




        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------



        private void button5_Click(object sender, EventArgs e)
        {
            RijndaelManaged myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey(); // passa la chiave 
            myRijndael.GenerateIV(); //passa l'array 

            String original = textBox3.Text; // stringa presa in input 

            // conversione della stinga in array di byte 
            byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

            string en = ("");
            for (int i = 0; i < encrypted.Length; i++)
            {
                en += encrypted[i];

            }
            textBox3.Text = (en);
            textBox4.Text = textBox3.Text;


            // decripta l'array di byte e lo converte uin stringa 
            string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);



            // DA CHIEDERE: I DISPLAY CHECK SERVONO NEL PROGRAMMA GRAFICO??
            //Display the original data and the decrypted data.
            Console.WriteLine("Original:   {0}", original);
            Console.WriteLine("Round Trip: {0}", roundtrip);

        }
        private void button4_Click(object sender, EventArgs e) 
        {
           
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            
        }

      

        private void button3_Click_1(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                Clipboard.SetText(textBox2.Text); //copia il testo negli appunti
            }
            else
            {
                textBox2.Text = ("nessun parametro da copiare");
            }

            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       
    }
}

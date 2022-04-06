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

        byte[] Key;
        byte[] IV;


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

            if (comboBox1.Text == "𝕊ℍ𝔸𝟙")
            {
                textBox2.Text = sha1(s);
            }
            else if (comboBox1.Text == "𝕊ℍ𝔸𝟚𝟝𝟞")
            {
                textBox2.Text = sha256(s);
            }
            else if (comboBox1.Text == "𝕄𝔻𝟝")
            {
                textBox2.Text = MD5Hash(s);
            }

        }
        public string sha1(string input) // FUNZIONE GENERA HASH CON SHA1
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
        static string sha256(string randomString) //FUNZIONE GENERA HASH CON SHA256
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }




        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------



        private void button5_Click(object sender, EventArgs e)//button crypt
        {
            RijndaelManaged myRijndael; myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey(); // passa la chiave 
            myRijndael.GenerateIV(); //passa l'array 
            Key=myRijndael.Key;
            IV = myRijndael.IV;

            String original = textBox3.Text; // stringa presa in input 

            // conversione della stinga in array di byte 
            byte[] encrypted = EncryptStringToBytes(original, Key, IV);

            string en = ("");
            for (int i = 0; i < encrypted.Length; i++)
            {
                en += encrypted[i];

            }

            
            textBox4.Text = (en);

            textBox4.Text = (en);
            



            // decripta l'array di byte e lo converte una stringa 
            string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

            


            textBox5.Text=(roundtrip);



            // DA CHIEDERE: I DISPLAY CHECK SERVONO NEL PROGRAMMA GRAFICO??
            //Display the original data and the decrypted data.
            Console.WriteLine("Original:   {0}", original);
            Console.WriteLine("Round Trip: {0}", roundtrip);

        }


        private void button4_Click(object sender, EventArgs e) //button decrypt  LAVORI IN CORSO !!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            RijndaelManaged myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey(); // passa la chiave 
            myRijndael.GenerateIV(); //passa l'array 

            string original = textBox5.Text;
            byte[] fileContent = Encoding.Unicode.GetBytes(original);


            string fine = DecryptStringFromBytes(fileContent, myRijndael.Key, myRijndael.IV);
            textBox6.Text=(fine);


          
            string author = textBox5.Text;
           
            byte[] bytes = Encoding.ASCII.GetBytes(author); //converti stringa in array di byte 
            string decry = DecryptStringFromBytes(bytes, myRijndael.Key, myRijndael.IV);
           // decrypt(textBox5.Text, fileContent, )

        }

        public static string decrypt(string msg, byte[] Key, byte[] IV)
        {

            byte[] bytes = Encoding.ASCII.GetBytes(msg); //converti stringa in array di byte 
            string decry = DecryptStringFromBytes(bytes, Key, IV);
            return decry;
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV) //funzione di Encrypt
        {
            // Check  
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

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV) //funzione di Decrypt 
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                Clipboard.SetText(textBox2.Text); //copia il testo negli appunti
            }
            else
            {
                textBox2.Text = ("nessun parametro da copiare");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox4.Text != "")
            {
                Clipboard.SetText(textBox4.Text); //copia il testo negli appunti
            }
            else
            {
                textBox4.Text = ("nessun parametro da copiare");
            }

        }

        private void label6_Click(object sender, EventArgs e)

        {

        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)

        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
          
            if (textBox6.Text != "")
            {
                Clipboard.SetText(textBox6.Text); //copia il testo negli appunti
            }
            else
            {
                textBox6.Text = ("nessun parametro da copiare");
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                Clipboard.SetText(textBox4.Text); //copia il testo negli appunti
            }
            else
            {
                textBox4.Text = ("nessun parametro da copiare");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}

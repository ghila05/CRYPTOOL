using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography; //libreria importata

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
        public string generaHash(string input)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(Encoding.UTF8.GetBytes(input));
            byte[] array = sh.Hash;
            StringBuilder sb = new StringBuilder();


            foreach (byte b in array)//costrutto foreach, prende ogni byte 
            {
                sb.Append(b.ToString("x2"));//x2 converte i caratteri in minuscolo 
            }


            return sb.ToString();

        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; // Developed by EMRECAN BALTA

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        public OleDbConnection Baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        public string tc, parola;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        bool Admin = false;
        bool Doktor = false;
        bool Kullanici = false;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void AdminGiris()
        {
         // if (textBox1.Text == "Administrator" && textBox2.Text == "1234")
            if (textBox1.Text == "123456789" && textBox2.Text == "1234")
                {
                Form5 F5 = new Form5();
                F5.Show();
                this.Hide();
                Admin = true;
                Kullanici = true;
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void KullaniciGiris()
        {
            try
            {
                Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc= '" + textBox1.Text + "' AND Parola= '" + textBox2.Text + "'",Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    Form2 F2 = new Form2(textBox1.Text, textBox2.Text);
                    F2.Show();
                    this.Hide();
                    Kullanici = true;
                }
                else
                {
                    Baglan.Close();
                    MessageBox.Show("Kullanıcı numaranız veya şifreniz yanlış lütfen tekrar deneyin.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Hata)
            {
                Baglan.Close();
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void DoktorGiris()
        {
            Doktor = false;
            Admin = false;
            try
            {
                Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Tc= '" + textBox1.Text + "' AND Parola='" + textBox2.Text + "'",Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    Form3 F3 = new Form3(textBox1.Text, textBox2.Text);
                    F3.Show();
                    this.Hide();
                    Doktor = true;
                    Kullanici = true;
                }
                Baglan.Close();
            }
            catch (Exception Hata)
            {
                Baglan.Close();
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            AdminGiris();
            if (Admin == false)
            {
                DoktorGiris();
            }
            if (Admin == false && Doktor == false)
            {
                KullaniciGiris();
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Baglan.Close();
            Application.Exit();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 F4 = new Form4();
            F4.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Wonxen");
        }
    }
}


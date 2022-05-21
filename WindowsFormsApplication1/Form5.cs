using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        Form4 F4 = new Form4();
        Form8 F8 = new Form8();
        Form9 F9 = new Form9();
        Form10 F10 = new Form10();
        Form11 F11 = new Form11();
        Form12 F12 = new Form12();
        Form13 F13 = new Form13();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut; 
        OleDbDataReader Oku; 
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = "Giriş Yapıldı";
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button7_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button8_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            F4.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button10_Click(object sender, EventArgs e)
        {
            F8.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button9_Click(object sender, EventArgs e)
        {
            F8.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button11_Click(object sender, EventArgs e)
        {
            F1.Baglan.Open();
            Komut = new OleDbCommand("SELECT * FROM Hesaplar", F1.Baglan);
            Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            MessageBox.Show("Hastane sistemizde şuan " + Sayi + " adet üye kayıtlı üye bulunmaktadır!", "ADMIN PANEL");
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button14_Click(object sender, EventArgs e)
        {
            F1.Baglan.Open();
            Komut = new OleDbCommand("SELECT * FROM Doktorlar", F1.Baglan);
            Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            MessageBox.Show("Hastane sistemizde şuan " + Sayi + " adet doktor kayıtlı bulunmaktadır!","ADMIN PANEL");
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button19_Click(object sender, EventArgs e)
        {
            F1.Baglan.Open();
            Komut = new OleDbCommand("SELECT * FROM Klinik", F1.Baglan);
            Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            MessageBox.Show("Hastane sistemizde şuan " + Sayi + " adet kayıtlı klinik bulunmaktadır!", "ADMIN PANEL");
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button12_Click(object sender, EventArgs e)
        {
            F10.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button18_Click(object sender, EventArgs e)
        {
            F9.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button13_Click(object sender, EventArgs e)
        {
            F9.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button15_Click(object sender, EventArgs e)
        {
            F11.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button17_Click(object sender, EventArgs e)
        {
            F12.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button16_Click(object sender, EventArgs e)
        {
            F13.ShowDialog();
        }
    }
}

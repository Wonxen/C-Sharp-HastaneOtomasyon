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
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string tc, string sifre)
        {
            InitializeComponent();
            Tc = tc;
            Sifre = sifre;
        }
        string Tc, Sifre;
        Form1 F1 = new Form1();
        public void Bilgi()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Tc=@Tc AND Sifre=@Sifre", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", Tc);
                Komut.Parameters.AddWithValue("@Sifre", Sifre);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label1.Text = "TC " + Oku["Tc"].ToString();
                    label2.Text = "Ad Soyad " + Oku["AdiSoyadi"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }
        private void Randevu()
        {
            try
            {
                F1.Baglan.Open();

                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e){ toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString() + " / " + DateTime.Now.ToShortDateString(); }

        private void Form3_Load(object sender, EventArgs e)
        {
            Bilgi();
            timer1.Start();
            this.CenterToScreen();
        }
    }
}

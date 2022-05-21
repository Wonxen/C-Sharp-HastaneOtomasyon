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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string tc, string parola)
        {
            InitializeComponent();
            TcKimlik = tc;
            Parola = parola;
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        string TcKimlik = "";
        string Parola = "";

        DateTime Date = DateTime.Now;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut; 
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Tc='" + TcKimlik + "'",F1.Baglan);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label5.Text = Oku["Tc"].ToString();
                    label6.Text = Oku["AdiSoyadi"].ToString();
                    label7.Text = Oku["Klinikid"].ToString();
                    label8.Text = "Giriş Yapıldı.";
                }
                F1.Baglan.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            RandevuSorgu();
            timer1.Start();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void GecmisZaman()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].BackColor = Color.White;
            }
            string Bugun = Date.ToShortDateString();
            string Simdi = Date.ToShortTimeString();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (string.Compare(Bugun, listView1.Items[i].SubItems[3].Text) == 0)
                {
                    if (string.Compare(Simdi, listView1.Items[i].SubItems[4].Text) == 1 || string.Compare(Simdi, listView1.Items[i].SubItems[4].Text) == 0)
                    {
                        listView1.Items[i].BackColor = Color.Red;
                    }
                }

                if (string.Compare(Bugun, listView1.Items[i].SubItems[3].Text) == 1)
                {
                    listView1.Items[i].BackColor = Color.Red;
                }
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].BackColor != Color.Red)
                {
                    listView1.Items[i].BackColor = Color.LawnGreen;
                }
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void RandevuSorgu()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Randevu WHERE DoktorAdi='" + label6.Text + "'",F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    listView1.Items.Add(Oku["Tc"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Tarih"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Saat"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Komut = new OleDbCommand("SELECT * FROM Doktorlar", F1.Baglan);
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            RandevuSorgu();
        }
    }
}

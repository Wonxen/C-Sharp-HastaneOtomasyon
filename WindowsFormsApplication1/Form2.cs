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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string tc, string sifre)
        {
            InitializeComponent();
            Tc = tc;
            Sifre = sifre;
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        string Tc, Sifre;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        DateTime Date = DateTime.Now;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void Bilgi()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc=@Tc", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", Tc);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label2.Text = F1.textBox1.Text;
                    label3.Text = "Adı Soyadı:" + " " + Oku["Adi"].ToString().ToUpper() + " " + Oku["Soyadi"].ToString().ToUpper();
                    label4.Text = "Doğum Tarihi:" + " " + Oku["DogumTarihi"].ToString();
                    label5.Text = "Doğum Yeri:" + " " + Oku["DogumYeri"].ToString().ToUpper();
                    label6.Text = "Cinsiyet:" + " " + Oku["Cinsiyeti"].ToString().ToUpper();
                    pictureBox1.ImageLocation = Oku["ProfilResim"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
                F1.Baglan.Close();
            }
        }
        public void YaklasanRandevular()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT TOP 3 * FROM Randevular WHERE Tc=@Tc ORDER BY Randevuid DESC", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", label2.Text);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                int i = 0;
                while (Oku.Read())
                {
                    i++;
                    listView1.Items.Add(i.ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["KlinikAdi"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["DoktorAdi"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Tarih"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Saat"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Randevuid"].ToString());
                    label7.Text = "Sistemde yaklaşan " + i.ToString() +  " randevu mevcut.";
                }
                F1.Baglan.Close();
                Zaman();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        private void ToplamRandevular()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Randevular WHERE Tc=@Tc ORDER BY Randevuid DESC", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", label2.Text);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                int i = 0;
                while (Oku.Read())
                {
                    i++;
                    listView2.Items.Add(i.ToString());
                    listView2.Items[i - 1].SubItems.Add(Oku["KlinikAdi"].ToString());
                    listView2.Items[i - 1].SubItems.Add(Oku["DoktorAdi"].ToString());
                    listView2.Items[i - 1].SubItems.Add(Oku["Tarih"].ToString());
                    listView2.Items[i - 1].SubItems.Add(Oku["Saat"].ToString());
                    listView2.Items[i - 1].SubItems.Add(Oku["Randevuid"].ToString());
                    label8.Text = "Sistemde toplam " + i.ToString() + " randevu bilgisi mevcut.";
                }
                F1.Baglan.Close();
                Zaman();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        private void Zaman()
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
                    listView1.Items[i].BackColor = Color.DodgerBlue;
                }
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void timer1_Tick(object sender, EventArgs e) { toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString() + " / " + DateTime.Now.ToShortDateString(); }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            Form5 F5 = new Form5(label2.Text);
            F5.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 F7 = new Form7(label2.Text, Sifre);
            F7.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 F8 = new Form8(label2.Text);
            F8.ShowDialog();
        }

        private void Form2_Leave(object sender, EventArgs e)
        {
            F1.Baglan.Close();
            Application.Exit();
        }

        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form2_Load(object sender, EventArgs e)
        {
            Bilgi();
            YaklasanRandevular();
            ToplamRandevular();
            timer1.Start();
            this.CenterToScreen();
        }
    }
}

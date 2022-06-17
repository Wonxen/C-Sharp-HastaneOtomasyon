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
        public Form5(string tc)
        {
            InitializeComponent();
            Tc = tc;
        }
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        string Tc = "", Klinik = "";
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        Form1 F1 = new Form1();
        Form2 F2 = new Form2();
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        DateTime Date = DateTime.Now;
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        bool Saat = true;
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        private void Klinikler()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Klinikler", F1.Baglan);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    comboBox1.Items.Add(Oku["KlinikAdi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void IdBul()
        {
            F1.Baglan.Open();
            try
            {
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Klinikler WHERE KlinikAdi='" + comboBox1.Text + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    Klinik = Oku["Klinikid"].ToString();
                }
                F1.Baglan.Close();
                Doktorlar();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void Doktorlar()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Klinikid=@id", F1.Baglan);
                Komut.Parameters.AddWithValue("@id", Klinik);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    comboBox2.Items.Add(Oku["AdiSoyadi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void Bugunmu()
        {
            foreach (Control index in panel1.Controls)
            {
                if (index.Text != "Saat Seçiniz")
                {
                    index.BackColor = Color.Yellow;
                }
            }
            DateTime Tarihcik = DateTime.Now;
            if (Tarihcik.ToShortDateString() == comboBox3.Text)
            {
                int Dakika = Tarihcik.Minute;
                int Saat = Tarihcik.Hour;
                foreach (Control index in panel1.Controls)
                {
                    if (index.Text != "Saat Seçiniz")
                    {
                        if (int.Parse(index.Text.Substring(0, 2)) < Saat)
                        {
                            index.BackColor = Color.Red;
                            index.Cursor = Cursors.Default;
                        }
                        if (int.Parse(index.Text.Substring(0, 2)) == Saat)
                        {
                            if (int.Parse(index.Text.Substring(3)) <= Dakika)
                            {
                                index.BackColor = Color.Red;
                                index.Cursor = Cursors.Default;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control index in panel1.Controls)
                {
                    if (index.Text != "Saat Seçiniz")
                    {
                        index.BackColor = Color.LawnGreen;
                        index.Cursor = Cursors.Hand;
                    }
                }
            }
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Randevular WHERE DoktorAdi=@Ad AND Tarih=@Tarih", F1.Baglan);
                Komut.Parameters.AddWithValue("@Ad", comboBox2.Text);
                Komut.Parameters.AddWithValue("@Tarih", comboBox3.Text);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    foreach (Control index in panel1.Controls)
                    {
                        if (Oku["Saat"].ToString() == index.Text)
                        {
                            index.BackColor = Color.Red;
                            index.Cursor = Cursors.Default;
                        }
                    }
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void Tarih()
        {
            int i = 0;
            while (comboBox3.Items.Count != 5)
            {
                if (Date.AddDays(i).DayOfWeek.ToString() != "Saturday" && Date.AddDays(i).DayOfWeek.ToString() != "Sunday")
                {
                    comboBox3.Items.Add(Date.AddDays(i).ToShortDateString());
                }
                i++;
            }
        }
        private void Button(object sender, EventArgs e)
        {
            Bugunmu();
            Saat = false;
            foreach (Control Saatler in panel1.Controls)
            {
                if (Saatler.Text == sender.ToString().Substring(34))
                {
                    if (Saatler.BackColor != Color.Red)
                    {
                        F1.Baglan.Open();
                        OleDbCommand Komut = new OleDbCommand("SELECT * FROM Randevular where Tc=@Tc AND Tarih=@Tarih", F1.Baglan);
                        Komut.Parameters.AddWithValue("@Tc", Tc);
                        Komut.Parameters.AddWithValue("@Tarih", comboBox3.Text);
                        OleDbDataReader Oku = Komut.ExecuteReader();
                        while (Oku.Read())
                        {
                            if (Saatler.Text == Oku["Saat"].ToString())
                            {
                                Saat = true;
                                MessageBox.Show("Hata: Çoklu seçim. Lütfen aynı gün ve saate bir adet randevu alınız!", "Hastane");
                                F1.Baglan.Close();
                                break;
                            }

                        }
                        F1.Baglan.Close();
                    }
                    if (Saatler.BackColor != Color.Red)
                    {
                        if (Saat == false)
                        {
                            F1.Baglan.Close();
                            DialogResult D = MessageBox.Show(comboBox3.Text + " Tarihinde " + comboBox1.Text + " Kliniğindeki " + comboBox2.Text + " Adlı Doktora " + sender.ToString().Substring(34) + " Saatine Randevu Alınıyor Onaylıyor musunuz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (D == DialogResult.Yes)
                            {
                                try
                                {
                                    F1.Baglan.Open();
                                    OleDbCommand Komut = new OleDbCommand("INSERT INTO Randevular (Tc,KlinikAdi,DoktorAdi,Tarih,Saat) VALUES (@Tc,@KlinikAdi,@DoktorAdi,@Tarih,@Saat)", F1.Baglan);
                                    Komut.Parameters.AddWithValue("@Tc", Tc);
                                    Komut.Parameters.AddWithValue("@KlinikAdi", comboBox1.Text);
                                    Komut.Parameters.AddWithValue("@DoktorAdi", comboBox2.Text);
                                    Komut.Parameters.AddWithValue("@Tarih", comboBox3.Text);
                                    Komut.Parameters.AddWithValue("@Saat", Saatler.Text);
                                    Komut.ExecuteNonQuery();
                                    MessageBox.Show("Randevunuz başarıyla sisteme kaydedilmiştir.\nSistemsel olarak birazdan sekme kapatılacaktır.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    F1.Baglan.Close();
                                    comboBox1.Select();
                                    IdBul();
                                    F2.listView1.Items.Clear();
                                    F2.YaklasanRandevular();
                                    IslemTamam();
                                }
                                catch (Exception Hata)
                                {
                                    MessageBox.Show(Hata.Message);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void IslemTamam()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            panel1.Visible = false;
            this.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            panel1.Visible = false;
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            IdBul();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Select();
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            Button(sender, e);
            Tarih();
            comboBox3.Visible = true;
            panel1.Visible = false;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Klinikler();
            panel1.Visible = true;
            Bugunmu();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form5_Load(object sender, EventArgs e)
        {
            Klinikler();
            this.CenterToScreen();
            foreach (Control index in panel1.Controls)
            {
                if (index.Text != "Saat Seçiniz")
                {
                    if (index.BackColor != Color.Red)
                    {
                        index.Click += new EventHandler(Button);
                    }
                }
            }
        }
    }
}

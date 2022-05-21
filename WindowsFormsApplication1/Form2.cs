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
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        public Form2(string tc, string parola)
        {
            InitializeComponent();
            TcKimlik = tc;
            Parola = parola;
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        string TcKimlik = "";
        string Parola = "";
        string klinikid = "";
        bool Saat = true;
        DateTime Date = DateTime.Now;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void KullaniciBilgi()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc='" + TcKimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label7.Text = Oku["Tc"].ToString();
                    label8.Text = "Adı Soyadı:" + " " + Oku["Adi"].ToString() + " " + Oku["Soyadi"].ToString();
                    label10.Text = "Doğum Tarihi:" + " " + Oku["DogumTarihi"].ToString();
                    label11.Text = "Doğum Yeri:" + " " + Oku["DogumYeri"].ToString();
                    label12.Text = "Cinsiyet:" + " " + Oku["Cinsiyeti"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void KlinikleriGoster()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Klinik", F1.Baglan);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    comboBox1.Items.Add(Oku["KlinikAdi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void KlinikidBul()
        {
            F1.Baglan.Open();
            try
            {
                Komut = new OleDbCommand("SELECT * FROM Klinik WHERE KlinikAdi='" + comboBox1.Text + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    klinikid = Oku["Klinikid"].ToString();
                }
                F1.Baglan.Close();
                DoktorBilgiGoster();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void DoktorBilgiGoster()
        {
            F1.Baglan.Open();
            try
            {
                Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Klinikid=@id", F1.Baglan);
                Komut.Parameters.AddWithValue("@id", klinikid);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();

                while (Oku.Read())
                {
                    comboBox2.Items.Add(Oku["AdiSoyadi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void TarihBugunmu()
        {
            foreach (Control ss in panel2.Controls)
            {
                if (ss.Text != "Randevu Saatini Seçin")
                {
                    ss.BackColor = Color.Yellow;
                }
            }
            DateTime Tarihcik = DateTime.Now;
            if (Tarihcik.ToShortDateString() == comboBox3.Text)
            {
                int Dakika = Tarihcik.Minute;
                int Saat = Tarihcik.Hour;
                foreach (Control x in panel2.Controls)
                {
                    if (x.Text != "Randevu Saatini Seçin")
                    {
                        if (int.Parse(x.Text.Substring(0, 2)) < Saat)
                        {
                            x.BackColor = Color.Red;
                            x.Cursor = Cursors.Default;
                        }
                        if (int.Parse(x.Text.Substring(0, 2)) == Saat)
                        {
                            if (int.Parse(x.Text.Substring(3)) <= Dakika)
                            {
                                x.BackColor = Color.Red;
                                x.Cursor = Cursors.Default;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control x in panel2.Controls)
                {
                    if (x.Text != "Randevu Saatini Seçin")
                    {
                        x.BackColor = Color.LawnGreen;
                        x.Cursor = Cursors.Hand;
                    }
                }
            }
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Randevu WHERE DoktorAdi='" + comboBox2.Text + "' AND Tarih='" + comboBox3.Text + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    foreach (Control x in panel2.Controls)
                    {
                        if (Oku["Saat"].ToString() == x.Text)
                        {
                            x.BackColor = Color.Red;
                            x.Cursor = Cursors.Default;
                        }
                    }
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void LabelTiklama(object sender, EventArgs e)
        {
            TarihBugunmu();
            Saat = false;
            foreach (Control Saatler in panel2.Controls)
            {
                if (Saatler.Text == sender.ToString().Substring(34))
                {
                    if (Saatler.BackColor != Color.Red)
                    {
                        F1.Baglan.Open();
                        Komut = new OleDbCommand("SELECT * FROM Randevu where Tc='" + TcKimlik + "' AND Tarih='" + comboBox3.Text + "'", F1.Baglan);
                        Komut.ExecuteNonQuery();
                        Oku = Komut.ExecuteReader();
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

                                    Komut = new OleDbCommand("INSERT INTO Randevu (Tc,KlinikAdi,DoktorAdi,Tarih,Saat) VALUES (@Tc,@KlinikAdi,@DoktorAdi,@Tarih,@Saat)", F1.Baglan);
                                    Komut.Parameters.AddWithValue("@Tc", TcKimlik);
                                    Komut.Parameters.AddWithValue("@KlinikAdi", comboBox1.Text);
                                    Komut.Parameters.AddWithValue("@DoktorAdi", comboBox2.Text);
                                    Komut.Parameters.AddWithValue("@Tarih", comboBox3.Text);
                                    Komut.Parameters.AddWithValue("@Saat", Saatler.Text);
                                    Komut.ExecuteNonQuery();
                                    MessageBox.Show("Randevu alma işlemi başarıyla gerçekleştirilmiştir.","Hastane");
                                    F1.Baglan.Close();
                                    RandevuAlindi();
                                    listView1.Items.Clear();
                                    AlinanRandevular();
                                    comboBox1.Select();
                                    KlinikidBul();
                                }
                                catch (Exception Hata)
                                {
                                    MessageBox.Show(Hata.ToString());
                                }
                            }
                            break;
                        }
                    }
                }
            }
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
                    listView1.Items[i].BackColor = Color.DodgerBlue;
                }
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void TarihBilgileriGoster()
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
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void RandevuAlindi()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            panel2.Visible = false;
            FormKucult();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void AlinanRandevular()
        {
            listView1.Items.Clear();
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Randevu WHERE Tc='" + TcKimlik + "' ORDER BY Randevuid DESC", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
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
                }
                F1.Baglan.Close();
                GecmisZaman();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void FormGenislet() { this.Width = 880; this.Height = 354; }
        private void FormKucult() { this.Width = 644; this.Height = 354; }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            FormKucult();
            KullaniciBilgi();
            AlinanRandevular();
            KlinikleriGoster();
            foreach (Control X in panel2.Controls)
            {
                if (X.Text != "Randevu Saatini Seçin")
                {
                    if (X.BackColor != Color.Red)
                    {
                        X.Click += new EventHandler(LabelTiklama);
                    }
                }
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form2_Leave(object sender, EventArgs e)
        {
            Application.Exit();
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            Form6 F6 = new Form6(label7.Text,Parola);
            F6.ShowDialog();

        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            FormGenislet();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button3_Click(object sender, EventArgs e)
        {
            Form7 F7 = new Form7(label7.Text);
            F7.ShowDialog();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            panel2.Visible = false;
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            KlinikidBul();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Select();
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            LabelTiklama(sender, e);
            TarihBilgileriGoster();
            comboBox3.Visible = true;
            panel2.Visible = false;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            KlinikleriGoster();
            panel2.Visible = true;
            TarihBugunmu();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            AlinanRandevular();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void label15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Wonxen");
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            //
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox3.Visible = false;
            // 
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            comboBox2.Visible = false;
            //
            comboBox1.Text = "";
            // 
            FormKucult();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Oturumunuzu kapatıp giriş ekranına dönmek istediğinize emin misiniz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (D == DialogResult.Yes)
            {
                this.Hide();
                F1.Show();
                F1.Baglan.Close();
            }
            else
            {
                MessageBox.Show("Oturum kapatma işlemi iptal edildi!","Hastane");
            }
        }
    }
}


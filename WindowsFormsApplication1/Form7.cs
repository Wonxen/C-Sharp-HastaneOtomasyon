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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public Form7(string tc, string sifre)
        {
            InitializeComponent();
            Tc = tc;
            Sifre = sifre;
        }
        string Tc = "", Sifre = "", Resim;
        Form1 F1 = new Form1();
        Form2 F2 = new Form2();
        private void Bilgiler()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc=@Tc", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", Tc);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox1.Text = Oku["Tc"].ToString();
                    textBox2.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Oku["Adi"].ToString());
                    textBox3.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Oku["Soyadi"].ToString());
                    comboBox1.Text = Oku["Cinsiyeti"].ToString();
                    textBox4.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Oku["DogumYeri"].ToString());
                    dateTimePicker1.Text = Oku["DogumTarihi"].ToString();
                    textBox5.Text = Oku["ePosta"].ToString();
                    textBox8.Text = Oku["TelefonNo"].ToString();
                    pictureBox1.ImageLocation = Oku["ProfilResim"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            F1.Baglan.Close();
        }

        private void Form7_Leave(object sender, EventArgs e)
        {
            this.Close();
            F1.Baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == Sifre)
                {
                    if(textBox7.Text != textBox9.Text)
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Şifre bilgisi doğrulanmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        F1.Baglan.Open();
                        OleDbCommand Komut = new OleDbCommand("UPDATE Hesaplar SET Adi=@Ad,Soyadi=@Soyad,Cinsiyeti=@Cinsiyet,DogumYeri=@DogumYer,DogumTarihi=@DogumTarih,ePosta=@Posta,TelefonNo=@Telefon,ProfilResim=@Resim,Sifre=@Sifre WHERE Tc='" + Tc + "'", F1.Baglan);
                        Komut.Parameters.AddWithValue("@Ad", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox2.Text));
                        Komut.Parameters.AddWithValue("@Soyad", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox3.Text));
                        Komut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                        Komut.Parameters.AddWithValue("@DogumYer", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox4.Text));
                        Komut.Parameters.AddWithValue("@DogumTarih", dateTimePicker1.Text);
                        Komut.Parameters.AddWithValue("@Posta", textBox5.Text);
                        Komut.Parameters.AddWithValue("@Telefon", textBox8.Text);
                        Komut.Parameters.AddWithValue("@Resim", Resim);
                        Komut.Parameters.AddWithValue("@Sifre", textBox7.Text);
                        Komut.ExecuteNonQuery();
                        F1.Baglan.Close();
                        MessageBox.Show("Sayın " + textBox2.Text + " " + textBox3.Text + " bilgileriniz başarıyla güncellenmiştir iyi günler dileriz.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Şifre Hatalı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.ToString());
            }
        }
        private void label10_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dosya = new OpenFileDialog();
            Dosya.Title = "Resim Aç";
            Dosya.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tüm Dosyalar (*.)|*.*";
            Dosya.ShowDialog();
            Resim = Dosya.FileName;
            pictureBox1.ImageLocation = Resim;
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            Bilgiler();
            this.CenterToScreen();
        }
    }
}

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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        Form1 F1 = new Form1();
        string Resim;
        private void Kontrol()
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                bool Kayit = false;
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar", F1.Baglan);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    if (Oku["Tc"].ToString() == textBox1.Text)
                    {
                        MessageBox.Show("Kimlik bilgisi sistemimizde zaten kayıtlı.\nGiriş sayfasındaki şifremi unuttum kısmını ziyaret ediniz.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Kayit = true;
                        this.Close();
                        break;
                    }
                }
                if (Kayit == false)
                {
                    if (textBox1.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Kimlik bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox2.Text == "" && textBox3.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Ad veya Soyad bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox4.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Doğum yeri bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox5.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("ePosta bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox6.Text == "" && textBox7.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Şifre bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox8.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Telefon bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (comboBox1.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Cinsiyet bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (dateTimePicker1.Text == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Doğum tarihi bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Resim == "")
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Profil resmi bilgisi doldurulmadı.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox6.Text != textBox7.Text)
                    {
                        F1.Baglan.Close();
                        MessageBox.Show("Şifreler Eşleşmiyor.", "Hatane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Komut = new OleDbCommand("INSERT INTO Hesaplar(Tc,Adi,Soyadi,Cinsiyeti,DogumYeri,DogumTarihi,ePosta,TelefonNo,ProfilResim,Sifre) VALUES (@Tc,@Adi,@Soyadi,@Cinsiyeti,@DogumYeri,@DogumTarihi,@ePosta,@TelefonNo,@ProfilResim,@Sifre)", F1.Baglan);
                        Komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                        Komut.Parameters.AddWithValue("@Adi", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox2.Text));
                        Komut.Parameters.AddWithValue("@Soyadi", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox3.Text));
                        Komut.Parameters.AddWithValue("@Cinsiyeti", comboBox1.Text);
                        Komut.Parameters.AddWithValue("@DogumYeri", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox4.Text));
                        Komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Text);
                        Komut.Parameters.AddWithValue("@ePosta", textBox5.Text);
                        Komut.Parameters.AddWithValue("@TelefonNo", textBox8.Text);
                        Komut.Parameters.AddWithValue("@ProfilResim", Resim);
                        Komut.Parameters.AddWithValue("Sifre", textBox7.Text);
                        Komut.ExecuteNonQuery();
                        MessageBox.Show("Sayın " + textBox2.Text + " " + textBox3.Text + " kaydınız başarıyla oluşturulmuştur giriş ekranına yönlendiriliyorsunuz.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        F1.Baglan.Close();
                        this.Close();
                    }
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
                this.Close();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox6.PasswordChar = '\0';
                textBox7.PasswordChar = '\0';
            }
            else
            {
                textBox6.PasswordChar = '*';
                textBox7.PasswordChar = '*';
            }
        }
    }
}

